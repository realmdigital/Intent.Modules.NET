﻿using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Exceptions;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;

namespace Intent.Modules.Application.Shared;

internal static class DomainKeyExtensions
{
    // This is duplicated in Intent.Modules.Application.MediatR.CRUD
    // Once we go through the Intent 4.1 we will need to upgrade and reconcile.
    public static ClassModel GetNestedCompositionalOwner(this ClassModel entity)
    {
        var aggregateRootClass = entity.AssociatedClasses
            .Where(p => p.TypeReference?.Element?.AsClassModel()?.IsAggregateRoot() == true &&
                        p.IsSourceEnd() && !p.IsCollection && !p.IsNullable)
            .Select(s => s.Class)
            .Distinct()
            .ToList();
        if (aggregateRootClass.Count > 1)
        {
            throw new ElementException(entity.InternalElement, $"{entity.Name} has multiple owners ({string.Join(",", aggregateRootClass.Select(a => a.Name))}). Owned entities can only have 1 owner.");
        }
        return aggregateRootClass.SingleOrDefault();
    }

    // This is duplicated in Intent.Modules.Application.MediatR.CRUD
    // Once we go through the Intent 4.1 we will need to upgrade and reconcile.
    public static EntityIdAttribute GetEntityPkAttribute(this ClassModel entity, ISoftwareFactoryExecutionContext executionContext)
    {
        return GetEntityPkAttributes(entity, executionContext).FirstOrDefault();
    }

    // This is duplicated in Intent.Modules.Application.MediatR.CRUD
    // Once we go through the Intent 4.1 we will need to upgrade and reconcile.
    public static IList<EntityIdAttribute> GetEntityPkAttributes(this ClassModel entity, ISoftwareFactoryExecutionContext executionContext)
    {
        while (entity != null)
        {
            var primaryKeys = entity.Attributes.Where(x => x.IsPrimaryKey()).ToArray();
            if (!primaryKeys.Any())
            {
                entity = entity.ParentClass;
                continue;
            }

            return primaryKeys
                .Select(attribute => new EntityIdAttribute(attribute.Name, GetKeyTypeName(attribute.Type), attribute))
                .ToList();
        }

        // Implicit Key:
        return new List<EntityIdAttribute>
        {
            new("Id", GetDefaultSurrogateKeyType(executionContext), null)
        };
    }

    // This is duplicated in Intent.Modules.Application.MediatR.CRUD
    // Once we go through the Intent 4.1 we will need to upgrade and reconcile.
    public static IList<EntityIdAttribute> GetEntityFkAttributes(
        this ClassModel composite,
        ClassModel owner,
        ISoftwareFactoryExecutionContext executionContext)
    {
        var fkAttributes = new List<AttributeModel>(0);

        while (composite != null)
        {
            var fkAttributesAndAssociations = composite.Attributes
                .Where(x => x.IsForeignKey())
                .Select(x => new
                {
                    Attribute = x,
                    Association = x.GetForeignKeyAssociation()
                })
                .ToArray();

            // Modern method of matching by the association on the FK:
            fkAttributes = fkAttributesAndAssociations
                .Where(x => owner.AssociationEnds().Any(y => x.Association.Id == y.Id))
                .Select(x => x.Attribute)
                .ToList();
            if (fkAttributes.Any())
            {
                break;
            }

            // Fallback to legacy method of matching by name:
            fkAttributes = fkAttributesAndAssociations
                .Where(x =>
                    x.Association == null &&
                    x.Attribute.Name.Contains(owner.Name, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Attribute)
                .ToList();
            if (fkAttributes.Any())
            {
                break;
            }

            composite = composite.ParentClass;
        }

        return fkAttributes
            .Select(x => new EntityIdAttribute(
                IdName: x.Name,
                Type: GetKeyTypeName(x.TypeReference),
                Attribute: x))
            .ToList();
    }
    
    // This is duplicated in Intent.Modules.Application.MediatR.CRUD
    // Once we go through the Intent 4.1 we will need to upgrade and reconcile.
    public static EntityNestedCompositionalIdAttribute GetNestedCompositionalOwnerIdAttribute(this ClassModel entity, ClassModel owner, ISoftwareFactoryExecutionContext executionContext)
    {
        return GetNestedCompositionalOwnerIdAttributes(entity, owner, executionContext).FirstOrDefault();
    }

    // This is duplicated in Intent.Modules.Application.MediatR.CRUD
    // Once we go through the Intent 4.1 we will need to upgrade and reconcile.
    public static IList<EntityNestedCompositionalIdAttribute> GetNestedCompositionalOwnerIdAttributes(this ClassModel entity, ClassModel owner, ISoftwareFactoryExecutionContext executionContext)
    {
        var curEntity = entity;
        while (curEntity is not null)
        {
            var foreignKeys = curEntity.Attributes.Where(x => x.IsForeignKey()).ToArray();
            if (!foreignKeys.Any())
            {
                curEntity = curEntity.ParentClass;
                continue;
            }

            return foreignKeys
                .Where(attr =>
                {
                    if (!attr.IsForeignKey())
                    {
                        return false;
                    }

                    var fkAssociation = attr.GetForeignKeyAssociation();

                    // Backward compatible lookup method
                    if (fkAssociation == null)
                    {
                        return attr.Name.Contains(owner.Name, StringComparison.OrdinalIgnoreCase);
                    }

                    return owner.AssociationEnds().Any(p => p.Id == fkAssociation.Id);
                })
                .Select(attribute => new EntityNestedCompositionalIdAttribute(attribute.Name, GetKeyTypeName(attribute.Type), attribute))
                .ToList();
        }
        
        // Nested Entity without Aggregate FK
        var aggregateOwner = entity.GetNestedCompositionalOwner();
        if (aggregateOwner is not null)
        {
            var pks = aggregateOwner.Attributes.Where(attr => attr.IsPrimaryKey()).ToArray();
            if (pks.Any())
            {
                return pks
                    .Select(attr => new EntityNestedCompositionalIdAttribute($"{owner.Name.ToPascalCase()}{attr.Name.ToPascalCase()}", GetKeyTypeName(attr.Type), attr))
                    .ToList();
            }
        }

        // Implicit Key:
        return new List<EntityNestedCompositionalIdAttribute>
        {
            new($"{owner.Name}Id", GetDefaultSurrogateKeyType(executionContext), null)
        };
    }

    // This is duplicated in Intent.Modules.Application.MediatR.CRUD
    // Once we go through the Intent 4.1 we will need to upgrade and reconcile.
    public static DTOFieldModel GetEntityIdField(this ICollection<DTOFieldModel> fields, ClassModel entity, ISoftwareFactoryExecutionContext context)
    {
        return GetEntityIdFields(fields, entity, context).FirstOrDefault();
    }

    // This is duplicated in Intent.Modules.Application.MediatR.CRUD
    // Once we go through the Intent 4.1 we will need to upgrade and reconcile.
    public static List<DTOFieldModel> GetEntityIdFields(this ICollection<DTOFieldModel> fields, ClassModel entity, ISoftwareFactoryExecutionContext context)
    {
        var primaryKeys = entity.GetEntityPkAttributes(context);

        return primaryKeys
            .Select(GetField)
            .Where(x => x != null)
            .ToList();

        DTOFieldModel GetField(EntityIdAttribute pk)
        {
            return fields
                .Where(field =>
                {
                    if (field.Mapping?.ElementId != null)
                    {
                        return field.Mapping.ElementId == pk.Attribute?.Id;
                    }

                    // Fallback to matching by name
                    return string.Equals(pk.IdName, field.Name, StringComparison.OrdinalIgnoreCase);
                })
                // We give priority to mapped elements and fallback to matches by name
                .MinBy(x => x.Mapping?.ElementId != null);
        }
    }
    
    public static string GetAttributeAndFieldComparison(
        this IList<DTOFieldModel> idFields,
        string domainVarName,
        string dtoVarName,
        bool isEqualComparison)
    {
        string comparisonSymbols = isEqualComparison ? "==" : "!=";
        if (idFields.Count == 1)
        {
            return $"{domainVarName}.{idFields.First().Name.ToPascalCase()} {comparisonSymbols} {dtoVarName}.{idFields.First().Name.ToPascalCase()}";
        }

        return $"{string.Join(" && ", idFields.Select(idField => $"{domainVarName}.{idField.Name.ToPascalCase()} {comparisonSymbols} {dtoVarName}.{idField.Name.ToPascalCase()}"))}";
    }
    
    // This is duplicated in Intent.Modules.Application.MediatR.CRUD
    // Once we go through the Intent 4.1 we will need to upgrade and reconcile.
    private static string GetKeyTypeName(ITypeReference typeReference)
    {
        return typeReference switch
        {
            _ when typeReference.HasIntType() => "int",
            _ when typeReference.HasLongType() => "long",
            _ when typeReference.HasGuidType() => "System.Guid",
            _ => typeReference.Element.Name
        };
    }

    // This is duplicated in Intent.Modules.Application.MediatR.CRUD
    // Once we go through the Intent 4.1 we will need to upgrade and reconcile.
    private static string GetDefaultSurrogateKeyType(ISoftwareFactoryExecutionContext executionContext)
    {
        //var settingType = executionContext.Settings.GetDatabaseSettings()?.KeyType().Value ?? "guid";
        var settingType = "guid"; // GCB - we want to deprecate this asap.
        switch (settingType)
        {
            case "guid":
                return "System.Guid";
            case "int":
                return "int";
            case "long":
                return "long";
            default:
                return settingType;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Intent.Exceptions;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Modelers.Services.DomainInteractions.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.Builder;
using Intent.Modules.Common.CSharp.Mapping;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeResolution;
using Intent.Modules.Common.Types.Api;
using Intent.Modules.Constants;
using Intent.Templates;

namespace Intent.Modules.Application.DomainInteractions;

public class DomainInteractionsManager
{
    private readonly ICSharpFileBuilderTemplate _template;
    private readonly CSharpClassMappingManager _csharpMapping;

    public DomainInteractionsManager(ICSharpFileBuilderTemplate template, CSharpClassMappingManager csharpMapping)
    {
        _template = template;
        _csharpMapping = csharpMapping;
    }

    public Dictionary<string, EntityDetails> TrackedEntities { get; set; } = new();

    public List<CSharpStatement> QueryEntity(ClassModel foundEntity, IAssociationEnd associationEnd)
    {
        var queryMapping = associationEnd.Mappings.GetQueryEntityMapping();
        if (queryMapping == null)
        {
            throw new ElementException(associationEnd, "Query Entity Mapping has not been specified.");
        }

        var entityVariableName = associationEnd.Name;

        _csharpMapping.SetFromReplacement(foundEntity, entityVariableName);
        _csharpMapping.SetFromReplacement(associationEnd, entityVariableName);
        _csharpMapping.SetToReplacement(foundEntity, entityVariableName);
        _csharpMapping.SetToReplacement(associationEnd, entityVariableName);

        if (!TryInjectRepositoryForEntity(foundEntity, out var dataAccess))
        {
            TryInjectDbContext(foundEntity, out dataAccess);
        }

        var statements = new List<CSharpStatement>();
        if (queryMapping.MappedEnds.Any() && queryMapping.MappedEnds.All(x => x.TargetElement.AsAttributeModel()?.IsPrimaryKey() == true)
            && foundEntity.Attributes.Count(x => x.IsPrimaryKey()) == queryMapping.MappedEnds.Count)
        {
            var idFields = queryMapping.MappedEnds
                .OrderBy(x => ((IElement)x.TargetElement).Order)
                .Select(x => new PrimaryKeyFilterMapping(
                    _csharpMapping.GenerateSourceStatementForMapping(queryMapping, x),
                    x.TargetElement.AsAttributeModel().Name.ToPropertyName(),
                    x))
                .ToList();

            if (associationEnd.TypeReference.IsCollection && idFields.All(x => x.Mapping.SourceElement.TypeReference.IsCollection))
            {
                statements.Add(new CSharpAssignmentStatement($"var {entityVariableName}", dataAccess.FindByIdsAsync(idFields)));
            }
            else
            {
                statements.Add(new CSharpAssignmentStatement($"var {entityVariableName}", dataAccess.FindByIdAsync(idFields)));
            }
        }
        else
        {
            var queryFields = queryMapping.MappedEnds
                .Select(x => x.IsOneToOne()
                    ? $"x.{x.TargetElement.Name} == {_csharpMapping.GenerateSourceStatementForMapping(queryMapping, x)}"
                    : $"x.{x.TargetElement.Name}.{_csharpMapping.GenerateSourceStatementForMapping(queryMapping, x)}")
                .ToList();


            var expression = queryFields.Any() ? $"x => {string.Join(" && ", queryFields)}" : "";

            if (associationEnd.TypeReference.IsCollection)
            {
                statements.Add(new CSharpAssignmentStatement($"var {entityVariableName}", dataAccess.FindAllAsync(expression)));
            }
            else if (TryGetPaginationValues(associationEnd, _csharpMapping, out var pageNo, out var pageSize))
            {
                statements.Add(new CSharpAssignmentStatement($"var {entityVariableName}", dataAccess.FindAllAsync(expression, pageNo, pageSize)));
            }
            else
            {
                if (!queryFields.Any())
                {
                    throw new ElementException(associationEnd, "No query fields have been mapped for this Query Entity Action, which signifies a single return value.");
                }
                statements.Add(new CSharpAssignmentStatement($"var {entityVariableName}", dataAccess.FindAsync(expression)));
            }
        }

        if (!associationEnd.TypeReference.IsNullable && !associationEnd.TypeReference.IsCollection && !IsResultPaginated(associationEnd.OtherEnd().TypeReference.Element.TypeReference))
        {
            var queryFields = queryMapping.MappedEnds
                .Select(x => new CSharpStatement($"{{{_csharpMapping.GenerateSourceStatementForMapping(queryMapping, x)}}}"))
                .ToList();
            statements.Add(CreateIfNullThrowNotFoundStatement(
                template: _template,
                variable: entityVariableName,
                message: $"Could not find {foundEntity.Name.ToPascalCase()} '{queryFields.AsSingleOrTuple()}'"));

        }
        TrackedEntities.Add(associationEnd.Id, new EntityDetails(foundEntity, entityVariableName, dataAccess, false, associationEnd.TypeReference.IsCollection));

        return statements;
    }

    public CSharpStatement CreateIfNullThrowNotFoundStatement(
        ICSharpTemplate template,
        string variable,
        string message)
    {
        var ifStatement = new CSharpIfStatement($"{variable} is null");
        ifStatement.SeparatedFromPrevious(false);
        ifStatement.AddStatement($@"throw new {template.GetNotFoundExceptionName()}($""{message}"");");

        return ifStatement;
    }

    public bool TryInjectRepositoryForEntity(ClassModel foundEntity, out IDataAccessProvider dataAccessProvider)
    {
        if (!_template.TryGetTypeName(TemplateRoles.Repository.Interface.Entity, foundEntity, out var repositoryInterface))
        {
            dataAccessProvider = null;
            return false;
        }
        var repositoryName = repositoryInterface[1..].ToCamelCase();
        var repositoryFieldName = default(string);

        var ctor = _template.CSharpFile.Classes.First().Constructors.First();
        if (ctor.Parameters.All(x => x.Type != repositoryInterface))
        {
            ctor.AddParameter(repositoryInterface, repositoryName.ToParameterName(),
                param => param.IntroduceReadonlyField(field => repositoryFieldName = field.Name));
        }
        else
        {
            repositoryFieldName = ctor.Parameters.First(x => x.Type == repositoryInterface).Name.ToPrivateMemberName();
        }

        dataAccessProvider = new RepositoryDataAccessProvider(repositoryFieldName);
        return true;
    }

    public bool TryInjectDbContext(ClassModel entity, out IDataAccessProvider dataAccessProvider)
    {
        if (!_template.TryGetTypeName(TemplateRoles.Application.Common.DbContextInterface, out var dbContextInterface))
        {
            dataAccessProvider = null;
            return false;
        }
        var dbContext = "dbContext";
        var dbContextField = default(string);

        var ctor = _template.CSharpFile.Classes.First().Constructors.First();
        if (ctor.Parameters.All(x => x.Type != dbContextInterface))
        {
            ctor.AddParameter(dbContextInterface, dbContext.ToParameterName(),
                param => param.IntroduceReadonlyField(field => dbContextField = field.Name));
        }
        else
        {
            dbContextField = ctor.Parameters.First(x => x.Type == dbContextInterface).Name.ToPrivateMemberName();
        }

        dataAccessProvider = new DbContextDataAccessProvider(dbContextField, entity, _template);
        return true;
    }


    private void InjectAutoMapper(out string fieldName)
    {
        var temp = default(string);
        var ctor = _template.CSharpFile.Classes.First().Constructors.First();
        if (ctor.Parameters.All(x => x.Type != _template.UseType("AutoMapper.IMapper")))
        {
            ctor.AddParameter(_template.UseType("AutoMapper.IMapper"), "mapper",
                param => param.IntroduceReadonlyField(field => temp = field.Name));
            fieldName = temp;
        }
        else
        {
            fieldName = ctor.Parameters.First(x => x.Type == _template.UseType("AutoMapper.IMapper")).Name.ToPrivateMemberName();
        }
    }

    public IEnumerable<CSharpStatement> GetReturnStatements(ITypeReference returnType)
    {
        if (returnType.Element == null)
        {
            throw new Exception("No return type specified");
        }
        var statements = new List<CSharpStatement>();
        var entitiesReturningPk = TrackedEntities.Values
            .Where(x => x.Model.GetTypesInHierarchy().SelectMany(c => c.Attributes).Count(a => a.IsPrimaryKey() && a.TypeReference.Element.Id == returnType.Element.Id) == 1)
            .ToList();
        foreach (var entity in entitiesReturningPk.Where(x => x.IsNew).GroupBy(x => x.Model.Id).Select(x => x.First()))
        {
            statements.Add($"await {entity.DataAccessProvider.SaveChangesAsync()}");
        }

        if (returnType.Element.AsDTOModel()?.IsMapped == true && _template.TryGetTypeName("Application.Contract.Dto", returnType.Element, out var returnDto))
        {
            var entityDetails = TrackedEntities.Values.First(x => x.Model.Id == returnType.Element.AsDTOModel().Mapping.ElementId);
            InjectAutoMapper(out var autoMapperFieldName);
            statements.Add($"return {entityDetails.VariableName}.MapTo{returnDto}{(returnType.IsCollection ? "List" : "")}({autoMapperFieldName});");
        }
        else if (IsResultPaginated(returnType) && returnType.GenericTypeParameters.FirstOrDefault()?.Element.AsDTOModel()?.IsMapped == true && _template.TryGetTypeName("Application.Contract.Dto", returnType.GenericTypeParameters.First().Element, out returnDto))
        {
            var entityDetails = TrackedEntities.Values.First(x => x.Model.Id == returnType.GenericTypeParameters.First().Element.AsDTOModel().Mapping.ElementId);
            InjectAutoMapper(out var autoMapperFieldName);
            statements.Add($"return {entityDetails.VariableName}.MapToPagedResult(x => x.MapTo{returnDto}({autoMapperFieldName}));");
        }
        else if (returnType.Element.IsTypeDefinitionModel() && entitiesReturningPk.Count == 1)
        {
            var entityDetails = entitiesReturningPk.Single();
            var entity = entityDetails.Model;
            statements.Add($"return {entityDetails.VariableName}.{entity.GetTypesInHierarchy().SelectMany(x => x.Attributes).FirstOrDefault(x => x.IsPrimaryKey())?.Name.ToPascalCase() ?? "Id"};");
        }
        else
        {
            statements.Add(string.Empty);
            statements.Add("throw new NotImplementedException(\"Implement return type mapping...\");");
        }

        return statements;
    }

    public IEnumerable<CSharpStatement> CreateEntity(CreateEntityActionTargetEndModel createAction)
    {
        var entity = createAction.Element.AsClassModel() ?? createAction.Element.AsClassConstructorModel().ParentClass;

        if (!TryInjectRepositoryForEntity(entity, out var dataAccess))
        {
            TryInjectDbContext(entity, out dataAccess);
        }

        TrackedEntities.Add(createAction.Id, new EntityDetails(entity, createAction.Name, dataAccess, true));

        var entityVariableName = createAction.Name;
        var statements = new List<CSharpStatement>();

        var mapping = createAction.Mappings.SingleOrDefault();
        if (mapping != null)
        {
            statements.Add(new CSharpAssignmentStatement($"var {entityVariableName}", _csharpMapping.GenerateCreationStatement(mapping)).WithSemicolon());
        }
        else
        {
            statements.Add(new CSharpAssignmentStatement($"var {entityVariableName}", $"new {entity.Name}();"));
        }

        _csharpMapping.SetFromReplacement(createAction.InternalAssociationEnd, entityVariableName);
        _csharpMapping.SetFromReplacement(entity, entityVariableName);
        _csharpMapping.SetToReplacement(createAction.InternalAssociationEnd, entityVariableName);
        _csharpMapping.SetToReplacement(entity, entityVariableName);

        foreach (var actions in createAction.ProcessingActions)
        {
            statements.Add(string.Empty);
            statements.AddRange(_csharpMapping.GenerateUpdateStatements(actions.InternalElement.Mappings.Single()));
            statements.Add(string.Empty);
        }
        return statements;
    }

    public IEnumerable<CSharpStatement> UpdateEntity(UpdateEntityActionTargetEndModel updateAction)
    {
        var entityDetails = TrackedEntities[updateAction.Id];
        var entity = entityDetails.Model;
        var updateMapping = updateAction.Mappings.GetUpdateEntityMapping();

        var statements = new List<CSharpStatement>();

        if (entityDetails.IsCollection)
        {
            _csharpMapping.SetToReplacement(entity, entityDetails.VariableName.Singularize());
            if (updateMapping != null)
            {
                statements.Add(new CSharpForEachStatement(entityDetails.VariableName.Singularize(), entityDetails.VariableName)
                    .AddStatements(_csharpMapping.GenerateUpdateStatements(updateMapping)));
            }

            if (RepositoryRequiresExplicitUpdate(entity))
            {
                statements.Add(entityDetails.DataAccessProvider.Update(entityDetails.VariableName.Singularize())
                    .SeparatedFromPrevious());
            }
        }
        else
        {
            if (updateMapping != null)
            {
                statements.AddRange(_csharpMapping.GenerateUpdateStatements(updateMapping));
            }

            if (RepositoryRequiresExplicitUpdate(entity))
            {
                statements.Add(entityDetails.DataAccessProvider.Update(entityDetails.VariableName)
                    .SeparatedFromPrevious());
            }
        }

        foreach (var actions in updateAction.ProcessingActions)
        {
            statements.Add(string.Empty);
            statements.AddRange(_csharpMapping.GenerateUpdateStatements(actions.InternalElement.Mappings.Single()));
            statements.Add(string.Empty);
        }

        return statements;
    }

    public IEnumerable<CSharpStatement> DeleteEntity(DeleteEntityActionTargetEndModel deleteAction)
    {
        var entityDetails = TrackedEntities[deleteAction.Id];
        var statements = new List<CSharpStatement>();
        if (entityDetails.IsCollection)
        {
            statements.Add(new CSharpForEachStatement(entityDetails.VariableName.Singularize(), entityDetails.VariableName)
                .AddStatement(entityDetails.DataAccessProvider.Remove(entityDetails.VariableName.Singularize()))
                    .SeparatedFromPrevious());
        }
        else
        {
            statements.Add(entityDetails.DataAccessProvider.Remove(entityDetails.VariableName)
                .SeparatedFromPrevious());
        }
        return statements;
    }

    private bool RepositoryRequiresExplicitUpdate(IMetadataModel forEntity)
    {
        return _template.TryGetTemplate<ICSharpFileBuilderTemplate>(
                   TemplateRoles.Repository.Interface.Entity,
                   forEntity,
                   out var repositoryInterfaceTemplate) &&
               repositoryInterfaceTemplate.CSharpFile.Interfaces[0].TryGetMetadata<bool>("requires-explicit-update", out var requiresUpdate) &&
               requiresUpdate;
    }


    private bool TryGetPaginationValues(IAssociationEnd associationEnd, CSharpClassMappingManager mappingManager, out string pageNo, out string pageSize)
    {
        var handler = (IElement)associationEnd.OtherEnd().TypeReference.Element;
        var returnsPagedResult = IsResultPaginated(handler.TypeReference);

        var pageIndexVar = handler.ChildElements.SingleOrDefault(IsPageIndexParam)?.Name;
        var accessVariable = mappingManager.GetFromReplacement(handler);
        pageNo = $"{(accessVariable != null ? $"{accessVariable}." : "")}{handler.ChildElements.SingleOrDefault(IsPageNumberParam)?.Name ?? $"{pageIndexVar} + 1"}";
        pageSize = $"{(accessVariable != null ? $"{accessVariable}." : "")}{handler.ChildElements.SingleOrDefault(IsPageSizeParam)?.Name}";

        return returnsPagedResult;
    }

    private bool IsResultPaginated(ITypeReference returnType)
    {
        return returnType.Element?.Name == "PagedResult";
    }

    private bool IsPageNumberParam(IElement param)
    {
        if (param.TypeReference.Element.Name != "int")
        {
            return false;
        }

        switch (param.Name.ToLower())
        {
            case "page":
            case "pageno":
            case "pagenum":
            case "pagenumber":
                return true;
            default:
                break;
        }

        return false;
    }

    private bool IsPageIndexParam(IElement param)
    {
        if (param.TypeReference.Element.Name != "int")
        {
            return false;
        }

        switch (param.Name.ToLower())
        {
            case "pageindex":
                return true;
            default:
                return false;
        }
    }

    private bool IsPageSizeParam(IElement param)
    {
        if (param.TypeReference.Element.Name != "int")
        {
            return false;
        }

        switch (param.Name.ToLower())
        {
            case "size":
            case "pagesize":
                return true;
            default:
                break;
        }

        return false;
    }
}



public record EntityDetails(ClassModel Model, string VariableName, IDataAccessProvider DataAccessProvider, bool IsNew, bool IsCollection = false);

public static class MappingExtensions
{
    public static IElementToElementMapping GetQueryEntityMapping(this IEnumerable<IElementToElementMapping> mappings)
    {
        return mappings.SingleOrDefault(x => x.Type == "Query Entity Mapping");
    }

    public static IElementToElementMapping GetUpdateEntityMapping(this IEnumerable<IElementToElementMapping> mappings)
    {
        return mappings.SingleOrDefault(x => x.Type == "Update Entity Mapping");
    }
}

internal static class AttributeModelExtensions
{
    public static bool IsPrimaryKey(this AttributeModel attribute)
    {
        return attribute.HasStereotype("Primary Key");
    }

    public static bool IsForeignKey(this AttributeModel attribute)
    {
        return attribute.HasStereotype("Foreign Key");
    }

    public static AssociationTargetEndModel GetForeignKeyAssociation(this AttributeModel attribute)
    {
        return attribute.GetStereotype("Foreign Key")?.GetProperty<IElement>("Association")?.AsAssociationTargetEndModel();
    }

    public static string AsSingleOrTuple(this IEnumerable<CSharpStatement> idFields)
    {
        if (idFields.Count() <= 1)
            return $"{idFields.Single()}";
        return $"({string.Join(", ", idFields.Select(idField => $"{idField}"))})";
    }
}
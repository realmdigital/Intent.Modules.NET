﻿using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modules.Common.CSharp.Builder;
using Intent.Modules.Common.Templates;

namespace Intent.Modules.Application.MediatR.CRUD.Mapping;

public abstract class CSharpMappingBase : ICSharpMapping
{
    public ICanBeReferencedType Model { get; }
    public IList<ICSharpMapping> Children { get; }
    public IElementToElementMappingConnection Mapping { get; set; }

    protected CSharpMappingBase(ICanBeReferencedType model, IElementToElementMappingConnection mapping, IList<ICSharpMapping> children)
    {
        Model = model;
        Mapping = mapping;
        Children = children;
    }

    public abstract IEnumerable<CSharpStatement> GetMappingStatement(IDictionary<ICanBeReferencedType, string> fromReplacements, IDictionary<ICanBeReferencedType, string> toReplacements);

    protected string GetFromPath(IDictionary<ICanBeReferencedType, string> replacements)
    {
        return GetPath(Mapping.FromPath, replacements);
    }

    protected string GetToPath(IDictionary<ICanBeReferencedType, string> replacements)
    {
        return GetPath(Mapping.ToPath, replacements);
    }

    protected string GetPath(IList<IElementMappingPathTarget> mappingPath, IDictionary<ICanBeReferencedType, string> replacements)
    {
        var result = "";
        foreach (var mappingPathTarget in mappingPath)
        {
            if (replacements.ContainsKey(mappingPathTarget.Element))
            {
                result = replacements[mappingPathTarget.Element] ?? ""; // if map to null, ignore
            }
            else
            {
                result += $"{(result.Length > 0 ? "." : "")}{mappingPathTarget.Element.Name.ToPascalCase()}";
                if (mappingPathTarget.Element.TypeReference.IsNullable && mappingPath.Last() != mappingPathTarget)
                {
                    result += "?";
                }
            }
        }
        return result;
    }
}
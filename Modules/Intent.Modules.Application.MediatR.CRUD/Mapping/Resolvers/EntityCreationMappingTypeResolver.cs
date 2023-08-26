﻿using Intent.Modules.Common.CSharp.Templates;

namespace Intent.Modules.Application.MediatR.CRUD.Mapping.Resolvers;

public class EntityCreationMappingTypeResolver : IMappingTypeResolver
{
    private readonly ICSharpFileBuilderTemplate _sourceTemplate;

    public EntityCreationMappingTypeResolver(ICSharpFileBuilderTemplate sourceTemplate)
    {
        _sourceTemplate = sourceTemplate;
    }

    public ICSharpMapping ResolveMappings(MappingModel mappingModel)
    {
        if (mappingModel.MappingTypeId != "48ebf732-c6d9-4589-810d-d2a4f05a6c4d")
        {
            return null;
        }

        var model = mappingModel.Model;
        if (model.SpecializationType == "Class" || model.TypeReference?.Element?.SpecializationType == "Class")
        {
            return new ObjectInitializationMapping(mappingModel, _sourceTemplate);
        }
        if (model.SpecializationType == "Class Constructor")
        {
            return new ImplicitConstructorMapping(mappingModel, _sourceTemplate);
        }
        if (model.SpecializationType == "Operation")
        {
            return new MethodInvocationMapping(mappingModel, _sourceTemplate);
        }

        return null;
    }
}
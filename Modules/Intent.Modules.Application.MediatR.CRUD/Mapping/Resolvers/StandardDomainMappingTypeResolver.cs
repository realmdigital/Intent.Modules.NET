﻿using System.Linq;
using Intent.Modelers.Services.CQRS.Api;
using Intent.Modules.Common.CSharp.Mapping;
using Intent.Modules.Common.CSharp.Templates;

namespace Intent.Modules.Application.MediatR.CRUD.Mapping.Resolvers;

public class StandardDomainMappingTypeResolver : IMappingTypeResolver
{
    private readonly ICSharpFileBuilderTemplate _template;
    private readonly CommandModel _command;

    public StandardDomainMappingTypeResolver(ICSharpFileBuilderTemplate template, CommandModel command)
    {
        _template = template;
        _command = command;
    }

    public ICSharpMapping ResolveMappings(MappingModel mappingModel)
    {
        var model = mappingModel.Model;

        if (mappingModel.Mapping?.SourceElement?.TypeReference?.IsCollection == true && mappingModel.Model.SpecializationType == "Operation")
        {
            return new ForLoopMethodInvocationMapping(mappingModel, _template);
        }

        if (model.SpecializationType == "Class Constructor")
        {
            return new ConstructorMapping(mappingModel, _template);
        }
        if (model.SpecializationType == "Operation")
        {
            return new MethodInvocationMapping(mappingModel, _template);
        }

        if (mappingModel.Model.SpecializationType == "Class")
        {
            return new MapChildrenMapping(mappingModel, _template);
        }

        if (mappingModel.Model.SpecializationType == "Create Entity Action Target End")
        {
            return new MapChildrenMapping(mappingModel, _template);
        }

        if (mappingModel.Model.SpecializationType == "Update Entity Action Target End")
        {
            return new MapChildrenMapping(mappingModel, _template);
        }


        return null;
    }
}
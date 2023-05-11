using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.Services.CQRS.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.HotChocolate.GraphQL.Templates.QueryResolver;

namespace Intent.Modules.HotChocolate.GraphQL.Dispatch.MediatR.ImplicitResolvers;

public class QueryGraphQLResolverModel : IGraphQLResolverModel
{
    public QueryGraphQLResolverModel(QueryModel query)
    {
        Name = query.Name.RemoveSuffix("Query");
        TypeReference = query.TypeReference;
        Parameters = query.Properties.Select(x => new GraphQLParameterModel(
            name: x.Name.ToCamelCase(),
            typeReference: x.TypeReference,
            mappedElement: x.InternalElement,
            mappedPath: new[] { x.Name }));
        MappedElement = query.InternalElement;
    }
    public string Name { get; }
    public ITypeReference TypeReference { get; }
    public IEnumerable<IGraphQLParameterModel> Parameters { get; }
    public IElement MappedElement { get; }
}

public class CommandGraphQLResolverModel : IGraphQLResolverModel
{
    public CommandGraphQLResolverModel(CommandModel command)
    {
        Name = command.Name.RemoveSuffix("Command");
        TypeReference = command.TypeReference;
        Parameters = Array.Empty<IGraphQLParameterModel>();
        MappedElement = command.InternalElement;
    }
    public string Name { get; }
    public ITypeReference TypeReference { get; }
    public IEnumerable<IGraphQLParameterModel> Parameters { get; }
    public IElement MappedElement { get; }
}
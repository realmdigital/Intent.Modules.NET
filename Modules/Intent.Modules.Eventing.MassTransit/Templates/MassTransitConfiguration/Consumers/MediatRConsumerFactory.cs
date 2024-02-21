﻿using System.Collections.Generic;
using System.Linq;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Constants;
using Intent.Modules.Contracts.Clients.Shared.FileNamespaceProviders;
using Intent.Modules.Eventing.MassTransit.Templates.RequestResponse.MapperRequestMessage;

namespace Intent.Modules.Eventing.MassTransit.Templates.MassTransitConfiguration.Consumers;

internal class MediatRConsumerFactory : IConsumerFactory
{
    private readonly MassTransitConfigurationTemplate _template;

    public MediatRConsumerFactory(MassTransitConfigurationTemplate template)
    {
        _template = template;
    }

    public IReadOnlyCollection<Consumer> CreateConsumers()
    {
        var namespaceProvider = new SourcePackageFileNamespaceProvider();
        var services = _template.ExecutionContext.MetadataManager.Services(_template.ExecutionContext.GetApplicationConfig().Id);
        var relevantCommands = services.GetElementsOfType("Command")
            .Where(p => p.HasStereotype(Constants.MassTransitConsumerStereotype) && _template.ExecutionContext.TemplateExists(TemplateRoles.Application.Handler.Command, p.Id));
        var relevantQueries = services.GetElementsOfType("Query")
            .Where(p => p.HasStereotype(Constants.MassTransitConsumerStereotype) && _template.ExecutionContext.TemplateExists(TemplateRoles.Application.Handler.Query, p.Id));
        var consumers = relevantCommands.Concat(relevantQueries)
            .Select(commandQuery =>
            {
                string fullTypeName = _template.GetFullyQualifiedTypeName(MapperRequestMessageTemplate.TemplateId, commandQuery);
                var t = _template.GetTemplate<MapperRequestMessageTemplate>(MapperRequestMessageTemplate.TemplateId, commandQuery.Id);
                string destinationAddress = $"{namespaceProvider.GetFileNamespace(t)}.{t.ClassName}".ToKebabCase();

                return new Consumer(
                    MessageTypeFullName: fullTypeName,
                    ConsumerTypeName: $@"{_template.GetMediatRConsumerName()}<{fullTypeName}>",
                    ConsumerDefinitionTypeName: $"{_template.GetMediatRConsumerName()}Definition<{fullTypeName}>",
                    ConfigureConsumeTopology: false,
                    DestinationAddress: destinationAddress,
                    AzureConsumerSettings: null,
                    RabbitMqConsumerSettings: null);
            })
            .ToList();
        return consumers;
    }
}
using System;
using System.Reflection;
using Intent.RoslynWeaver.Attributes;
using MassTransit;
using MassTransit.Configuration;
using MassTransit.Messages.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Subscribe.MassTransit.OutboxEF.Application.Common.Eventing;
using Subscribe.MassTransit.OutboxEF.Infrastructure.Eventing;
using Subscribe.MassTransit.OutboxEF.Infrastructure.Persistence;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.MassTransitConfiguration", Version = "1.0")]

namespace Subscribe.MassTransit.OutboxEF.Infrastructure.Configuration
{
    public static class MassTransitConfiguration
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumers();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseMessageRetry(r => r.Interval(
                        configuration.GetValue<int?>("MassTransit:Retry:RetryCount") ?? 10,
                        configuration.GetValue<TimeSpan?>("MassTransit:Retry:Interval") ?? TimeSpan.FromSeconds(30)));
                    cfg.Host(configuration["RabbitMq:Host"], configuration["RabbitMq:VirtualHost"], host =>
                    {
                        host.Username(configuration["RabbitMq:Username"]);
                        host.Password(configuration["RabbitMq:Password"]);
                    });
                    cfg.ConfigureEndpoints(context);
                });
                x.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
                {
                    o.UseSqlServer();
                    o.UseBusOutbox();
                });
            });
        }

        private static void AddConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<WrapperConsumer<IIntegrationEventHandler<OrderCreatedEvent>, OrderCreatedEvent>>(typeof(WrapperConsumerDefinition<IIntegrationEventHandler<OrderCreatedEvent>, OrderCreatedEvent>)).Endpoint(config => config.InstanceId = "Subscribe-MassTransit-OutboxEF");
            cfg.AddConsumer<WrapperConsumer<IIntegrationEventHandler<OrderUpdatedEvent>, OrderUpdatedEvent>>(typeof(WrapperConsumerDefinition<IIntegrationEventHandler<OrderUpdatedEvent>, OrderUpdatedEvent>)).Endpoint(config => config.InstanceId = "Subscribe-MassTransit-OutboxEF");
            cfg.AddConsumer<WrapperConsumer<IIntegrationEventHandler<OrderDeletedEvent>, OrderDeletedEvent>>(typeof(WrapperConsumerDefinition<IIntegrationEventHandler<OrderDeletedEvent>, OrderDeletedEvent>)).Endpoint(config => config.InstanceId = "Subscribe-MassTransit-OutboxEF");
        }
    }
}
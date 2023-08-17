using System;
using System.Reflection;
using Intent.RoslynWeaver.Attributes;
using MassTransit;
using MassTransit.Configuration;
using MassTransitFinbuckle.Test.Infrastructure.Eventing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.MassTransitConfiguration", Version = "1.0")]

namespace MassTransitFinbuckle.Test.Infrastructure.Configuration
{
    public static class MassTransitConfiguration
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumers();
                x.UsingInMemory((context, cfg) =>
                {
                    cfg.UseMessageRetry(r => r.Interval(
                        configuration.GetValue<int?>("MassTransit:Retry:RetryCount") ?? 10,
                        configuration.GetValue<TimeSpan?>("MassTransit:Retry:Interval") ?? TimeSpan.FromSeconds(30)));
                    cfg.ConfigureEndpoints(context);
                    cfg.UseInMemoryOutbox();
                    cfg.UsePublishFilter(typeof(FinbucklePublishingFilter<>), context);
                    cfg.UseConsumeFilter(typeof(FinbuckleConsumingFilter<>), context);
                });
                x.AddInMemoryInboxOutbox();
            });
        }

        private static void AddConsumers(this IRegistrationConfigurator cfg)
        {
        }
    }
}
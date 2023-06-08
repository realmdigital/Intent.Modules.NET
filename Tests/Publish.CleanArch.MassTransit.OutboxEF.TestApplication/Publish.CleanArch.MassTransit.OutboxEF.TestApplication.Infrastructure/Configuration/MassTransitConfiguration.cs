using System;
using System.Reflection;
using Intent.RoslynWeaver.Attributes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publish.CleanArch.MassTransit.OutboxEF.TestApplication.Infrastructure.Persistence;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.MassTransitConfiguration", Version = "1.0")]

namespace Publish.CleanArch.MassTransit.OutboxEF.TestApplication.Infrastructure.Configuration
{
    public static class MassTransitConfiguration
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                AddConsumers(x);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseMessageRetry(r => r.Interval(10, TimeSpan.FromSeconds(30)));

                    cfg.Host(configuration["RabbitMq:Host"], configuration["RabbitMq:VirtualHost"], h =>
                    {
                        h.Username(configuration["RabbitMq:Username"]);
                        h.Password(configuration["RabbitMq:Password"]);
                    });

                    cfg.ConfigureEndpoints(context);
                });

                x.AddEntityFrameworkOutbox<ApplicationDbContext>((o) =>
                {
                    o.UseSqlServer();
                    o.UseBusOutbox();
                });
            });
        }

        private static void AddConsumers(IRegistrationConfigurator cfg)
        {

        }
    }
}
using System.Reflection;
using AutoMapper;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Subscribe.GooglePubSub.TestApplication.Application.Common.Eventing;
using Subscribe.GooglePubSub.TestApplication.Application.IntegrationEventHandlers;
using Subscribe.GooglePubSub.TestApplication.Application.IntegrationEvents;
using Subscribe.GooglePubSub.TestApplication.Eventing.Messages;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace Subscribe.GooglePubSub.TestApplication.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IIntegrationEventHandler<EventStartedEvent>, EventStartedEventHandler>();
            services.AddTransient<IIntegrationEventHandler<GenericMessage>, GenericEventHandler>();
            return services;
        }
    }
}
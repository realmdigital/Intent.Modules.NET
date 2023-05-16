using Intent.RoslynWeaver.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publish.CleanArch.GooglePubSub.TestApplication.Application.Common.Eventing;
using Publish.CleanArch.GooglePubSub.TestApplication.Eventing.Messages;
using Publish.CleanArch.GooglePubSub.TestApplication.Infrastructure.Eventing;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.GoogleCloud.PubSub.ConfigurationTemplates.GoogleCloudPubSubConfiguration", Version = "1.0")]

namespace Publish.CleanArch.GooglePubSub.TestApplication.Infrastructure.Configuration
{
    public static class GoogleCloudPubSubConfiguration
    {
        public static IServiceCollection RegisterGoogleCloudPubSubServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<PubSubOptions>(configuration.GetSection("GoogleCloudPubSub"));
            services.AddScoped<GooglePubSubEventBus>();
            services.AddTransient<IEventBus>(provider => provider.GetService<GooglePubSubEventBus>());
            services.AddSingleton<GoogleCloudResourceManager>();
            services.AddTransient<ICloudResourceManager>(provider => provider.GetService<GoogleCloudResourceManager>());
            return services;
        }

        public static IServiceCollection AddSubscribers(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection RegisterTopicEvents(this IServiceCollection services)
        {
            services.AddSingleton(
                provider =>
                {
                    var topicEventManager = new GoogleEventBusTopicEventManager(provider.GetService<ICloudResourceManager>());
                    RegisterTopicEvents(topicEventManager);
                    return topicEventManager;
                });
            services.AddTransient<IEventBusTopicEventManager>(provider => provider.GetService<GoogleEventBusTopicEventManager>());
            return services;
        }

        private static void RegisterTopicEvents(GoogleEventBusTopicEventManager topicEventManager)
        {
            topicEventManager.RegisterTopicEvent<EventStartedEvent>("test-app");
        }

        public static IServiceCollection RegisterEventHandlers(this IServiceCollection services)
        {
            var subscriptionManager = new GoogleEventBusSubscriptionManager();
            services.AddSingleton(subscriptionManager);
            services.AddTransient<IEventBusSubscriptionManager>(provider => provider.GetService<GoogleEventBusSubscriptionManager>());
            subscriptionManager.RegisterEventHandler<EventStartedEvent>();
            return services;
        }
    }
}
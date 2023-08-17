using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using MassTransit;
using MassTransitFinbuckle.Test.Application.Common.Eventing;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.MassTransitEventBus", Version = "1.0")]

namespace MassTransitFinbuckle.Test.Infrastructure.Eventing
{
    public class MassTransitEventBus : IEventBus
    {
        private readonly List<object> _messagesToPublish = new List<object>();

        public MassTransitEventBus(IPublishEndpoint publishEndpoint)
        {
            Current = publishEndpoint;
        }

        public IPublishEndpoint Current { get; set; }

        public void Publish<T>(T message) where T : class
        {
            _messagesToPublish.Add(message);
        }

        public async Task FlushAllAsync(CancellationToken cancellationToken = default)
        {
            await Current.PublishBatch(_messagesToPublish, cancellationToken);
            _messagesToPublish.Clear();
        }
    }
}
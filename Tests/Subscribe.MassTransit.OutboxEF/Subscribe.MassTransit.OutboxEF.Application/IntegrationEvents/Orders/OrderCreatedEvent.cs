using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.Contracts.IntegrationEventMessage", Version = "1.0")]

namespace MassTransit.Messages.Shared.Orders
{
    public record OrderCreatedEvent
    {
        public OrderCreatedEvent()
        {
            Number = null!;
            OrderItems = null!;
        }

        public Guid Id { get; init; }
        public string Number { get; init; }
        public List<OrderItemDto> OrderItems { get; init; }
    }
}
using System;
using IntegrationTesting.Tests.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace IntegrationTesting.Tests.Application.Orders.DeleteOrderOrderItem
{
    public class DeleteOrderOrderItemCommand : IRequest, ICommand
    {
        public DeleteOrderOrderItemCommand(Guid orderId, Guid id)
        {
            OrderId = orderId;
            Id = id;
        }

        public Guid OrderId { get; set; }
        public Guid Id { get; set; }
    }
}
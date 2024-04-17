using System;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace OpenApiImporterTest.Application.Stores.CreateStoreOrder
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CreateStoreOrderCommandHandler : IRequestHandler<CreateStoreOrderCommand, Order>
    {
        [IntentManaged(Mode.Merge)]
        public CreateStoreOrderCommandHandler()
        {
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public async Task<Order> Handle(CreateStoreOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("Your implementation here...");
        }
    }
}
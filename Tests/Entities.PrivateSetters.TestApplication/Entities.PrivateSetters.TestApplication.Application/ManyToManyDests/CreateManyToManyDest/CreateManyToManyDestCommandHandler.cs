using System;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace Entities.PrivateSetters.TestApplication.Application.ManyToManyDests.CreateManyToManyDest
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CreateManyToManyDestCommandHandler : IRequestHandler<CreateManyToManyDestCommand, Guid>
    {
        [IntentManaged(Mode.Merge)]
        public CreateManyToManyDestCommandHandler()
        {
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public async Task<Guid> Handle(CreateManyToManyDestCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("Your implementation here...");
        }
    }
}
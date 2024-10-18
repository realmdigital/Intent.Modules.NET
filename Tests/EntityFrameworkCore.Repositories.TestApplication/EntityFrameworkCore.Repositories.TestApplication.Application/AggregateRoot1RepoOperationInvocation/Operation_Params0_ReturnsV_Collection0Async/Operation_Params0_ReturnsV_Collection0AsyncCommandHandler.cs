using System;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Repositories.TestApplication.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace EntityFrameworkCore.Repositories.TestApplication.Application.AggregateRoot1RepoOperationInvocation.Operation_Params0_ReturnsV_Collection0Async
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class Operation_Params0_ReturnsV_Collection0AsyncCommandHandler : IRequestHandler<Operation_Params0_ReturnsV_Collection0AsyncCommand>
    {
        private readonly IAggregateRoot1Repository _aggregateRoot1Repository;

        [IntentManaged(Mode.Merge)]
        public Operation_Params0_ReturnsV_Collection0AsyncCommandHandler(IAggregateRoot1Repository aggregateRoot1Repository)
        {
            _aggregateRoot1Repository = aggregateRoot1Repository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(
            Operation_Params0_ReturnsV_Collection0AsyncCommand request,
            CancellationToken cancellationToken)
        {
            await _aggregateRoot1Repository.Operation_Params0_ReturnsV_Collection0Async(cancellationToken);
        }
    }
}
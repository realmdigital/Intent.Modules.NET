using EntityFrameworkCore.Repositories.TestApplication.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace EntityFrameworkCore.Repositories.TestApplication.Application.CustomRepoOperationInvocation.Operation_Params0_ReturnsV_Collection0
{
    public class Operation_Params0_ReturnsV_Collection0Command : IRequest, ICommand
    {
        public Operation_Params0_ReturnsV_Collection0Command()
        {
        }
    }
}
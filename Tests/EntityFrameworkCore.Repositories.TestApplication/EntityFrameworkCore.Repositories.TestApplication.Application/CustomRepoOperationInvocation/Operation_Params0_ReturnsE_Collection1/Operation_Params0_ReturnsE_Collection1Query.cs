using System.Collections.Generic;
using EntityFrameworkCore.Repositories.TestApplication.Application.Common.Interfaces;
using EntityFrameworkCore.Repositories.TestApplication.Application.CommonDtos;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace EntityFrameworkCore.Repositories.TestApplication.Application.CustomRepoOperationInvocation.Operation_Params0_ReturnsE_Collection1
{
    public class Operation_Params0_ReturnsE_Collection1Query : IRequest<List<AggregateRoot1Dto>>, IQuery
    {
        public Operation_Params0_ReturnsE_Collection1Query()
        {
        }
    }
}
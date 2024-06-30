using System.Collections.Generic;
using IntegrationTesting.Tests.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace IntegrationTesting.Tests.Application.UniqueConVals.GetUniqueConVals
{
    public class GetUniqueConValsQuery : IRequest<List<UniqueConValDto>>, IQuery
    {
        public GetUniqueConValsQuery()
        {
        }
    }
}
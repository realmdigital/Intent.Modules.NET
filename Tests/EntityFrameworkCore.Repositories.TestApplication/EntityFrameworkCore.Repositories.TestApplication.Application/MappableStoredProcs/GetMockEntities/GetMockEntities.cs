using System.Collections.Generic;
using EntityFrameworkCore.Repositories.TestApplication.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace EntityFrameworkCore.Repositories.TestApplication.Application.MappableStoredProcs.GetMockEntities
{
    public class GetMockEntities : IRequest<List<MockEntityDto>>, IQuery
    {
        public GetMockEntities()
        {
        }
    }
}
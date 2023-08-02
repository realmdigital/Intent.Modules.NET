using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.SqlServer.TestApplication.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace EntityFrameworkCore.SqlServer.TestApplication.Domain.Repositories
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public interface IParentNonStdIdRepository : IEFRepository<ParentNonStdId, ParentNonStdId>
    {
        [IntentManaged(Mode.Fully)]
        Task<ParentNonStdId?> FindByIdAsync(Guid myId, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<List<ParentNonStdId>> FindByIdsAsync(Guid[] myIds, CancellationToken cancellationToken = default);
    }
}
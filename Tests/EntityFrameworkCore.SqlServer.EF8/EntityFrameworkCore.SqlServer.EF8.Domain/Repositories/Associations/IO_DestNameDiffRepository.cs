using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.SqlServer.EF8.Domain.Entities.Associations;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Domain.Repositories.Associations
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public interface IO_DestNameDiffRepository : IEFRepository<O_DestNameDiff, O_DestNameDiff>
    {
        [IntentManaged(Mode.Fully)]
        Task<O_DestNameDiff?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<List<O_DestNameDiff>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);
    }
}
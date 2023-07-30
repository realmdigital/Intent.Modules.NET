using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities.PrivateSetters.TestApplication.Domain.Entities.Aggregational;
using Entities.PrivateSetters.TestApplication.Domain.Repositories;
using Entities.PrivateSetters.TestApplication.Domain.Repositories.Aggregational;
using Entities.PrivateSetters.TestApplication.Infrastructure.Persistence;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.Repository", Version = "1.0")]

namespace Entities.PrivateSetters.TestApplication.Infrastructure.Repositories.Aggregational
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class ManyToOneDestRepository : RepositoryBase<ManyToOneDest, ManyToOneDest, ApplicationDbContext>, IManyToOneDestRepository
    {
        public ManyToOneDestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ManyToOneDest?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<ManyToOneDest>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
        {
            return await FindAllAsync(x => ids.Contains(x.Id), cancellationToken);
        }
    }
}
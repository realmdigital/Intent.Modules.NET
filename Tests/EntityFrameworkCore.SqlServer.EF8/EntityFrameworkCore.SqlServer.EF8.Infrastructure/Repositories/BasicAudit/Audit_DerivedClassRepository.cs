using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.SqlServer.EF8.Domain.Entities.BasicAudit;
using EntityFrameworkCore.SqlServer.EF8.Domain.Repositories;
using EntityFrameworkCore.SqlServer.EF8.Domain.Repositories.BasicAudit;
using EntityFrameworkCore.SqlServer.EF8.Infrastructure.Persistence;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.Repository", Version = "1.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Infrastructure.Repositories.BasicAudit
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class Audit_DerivedClassRepository : RepositoryBase<Audit_DerivedClass, Audit_DerivedClass, ApplicationDbContext>, IAudit_DerivedClassRepository
    {
        public Audit_DerivedClassRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Audit_DerivedClass?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<Audit_DerivedClass>> FindByIdsAsync(
            Guid[] ids,
            CancellationToken cancellationToken = default)
        {
            // Force materialization - Some combinations of .net9 runtime and EF runtime crash with "Convert ReadOnlySpan to List since expression trees can't handle ref struct"
            var idList = ids.ToList();
            return await FindAllAsync(x => idList.Contains(x.Id), cancellationToken);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.SqlServer.EF8.Domain.Entities.NotSchema;
using EntityFrameworkCore.SqlServer.EF8.Domain.Repositories;
using EntityFrameworkCore.SqlServer.EF8.Domain.Repositories.NotSchema;
using EntityFrameworkCore.SqlServer.EF8.Infrastructure.Persistence;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.Repository", Version = "1.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Infrastructure.Repositories.NotSchema
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class TableFolderRepository : RepositoryBase<TableFolder, TableFolder, ApplicationDbContext>, ITableFolderRepository
    {
        public TableFolderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TableFolder?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<TableFolder>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
        {
            return await FindAllAsync(x => ids.Contains(x.Id), cancellationToken);
        }
    }
}
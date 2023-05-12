using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.TestApplication.Domain.Entities.DefaultDiagram;
using CleanArchitecture.TestApplication.Domain.Repositories.DefaultDiagram;
using CleanArchitecture.TestApplication.Infrastructure.Persistence;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.Repository", Version = "1.0")]

namespace CleanArchitecture.TestApplication.Infrastructure.Repositories.DefaultDiagram
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class ClassWithDefaultRepository : RepositoryBase<ClassWithDefault, ClassWithDefault, ApplicationDbContext>, IClassWithDefaultRepository
    {
        public ClassWithDefaultRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ClassWithDefault> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<ClassWithDefault>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
        {
            return await FindAllAsync(x => ids.Contains(x.Id), cancellationToken);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FastEndpointsTest.Domain.Entities.Pagination;
using FastEndpointsTest.Domain.Repositories;
using FastEndpointsTest.Domain.Repositories.Pagination;
using FastEndpointsTest.Infrastructure.Persistence;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.Repository", Version = "1.0")]

namespace FastEndpointsTest.Infrastructure.Repositories.Pagination
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class PersonEntryRepository : RepositoryBase<PersonEntry, PersonEntry, ApplicationDbContext>, IPersonEntryRepository
    {
        public PersonEntryRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<PersonEntry?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<PersonEntry>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
        {
            return await FindAllAsync(x => ids.Contains(x.Id), cancellationToken);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EntityFrameworkCore.SqlServer.TestApplication.Domain.Entities.ValueObjects;
using EntityFrameworkCore.SqlServer.TestApplication.Domain.Repositories;
using EntityFrameworkCore.SqlServer.TestApplication.Domain.Repositories.ValueObjects;
using EntityFrameworkCore.SqlServer.TestApplication.Infrastructure.Persistence;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.Repository", Version = "1.0")]

namespace EntityFrameworkCore.SqlServer.TestApplication.Infrastructure.Repositories.ValueObjects
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class PersonWithAddressNormalRepository : RepositoryBase<PersonWithAddressNormal, PersonWithAddressNormal, ApplicationDbContext>, IPersonWithAddressNormalRepository
    {
        public PersonWithAddressNormalRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<PersonWithAddressNormal?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<PersonWithAddressNormal>> FindByIdsAsync(
            Guid[] ids,
            CancellationToken cancellationToken = default)
        {
            return await FindAllAsync(x => ids.Contains(x.Id), cancellationToken);
        }
    }
}
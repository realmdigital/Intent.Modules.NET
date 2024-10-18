using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using Redis.Om.Repositories.Domain.Entities;
using Redis.Om.Repositories.Domain.Repositories.Documents;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace Redis.Om.Repositories.Domain.Repositories
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public interface ICustomerRepository : IRedisOmRepository<Customer, ICustomerDocument>
    {
        [IntentManaged(Mode.Fully)]
        Task<Customer?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
    }
}
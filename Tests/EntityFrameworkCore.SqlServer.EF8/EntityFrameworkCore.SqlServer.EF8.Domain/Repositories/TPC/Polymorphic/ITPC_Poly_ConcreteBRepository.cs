using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.SqlServer.EF8.Domain.Entities.TPC.Polymorphic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Domain.Repositories.TPC.Polymorphic
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public interface ITPC_Poly_ConcreteBRepository : IEFRepository<TPC_Poly_ConcreteB, TPC_Poly_ConcreteB>
    {
        [IntentManaged(Mode.Fully)]
        Task<TPC_Poly_ConcreteB?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<List<TPC_Poly_ConcreteB>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);
    }
}
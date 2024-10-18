using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.SqlServer.EF8.Domain.Entities.TPH.InheritanceAssociations;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Domain.Repositories.TPH.InheritanceAssociations
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public interface ITPH_DerivedClassForConcreteRepository : IEFRepository<TPH_DerivedClassForConcrete, TPH_DerivedClassForConcrete>
    {
        [IntentManaged(Mode.Fully)]
        Task<TPH_DerivedClassForConcrete?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<List<TPH_DerivedClassForConcrete>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);
    }
}
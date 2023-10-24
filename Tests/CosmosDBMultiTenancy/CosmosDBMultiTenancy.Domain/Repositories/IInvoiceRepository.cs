using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CosmosDBMultiTenancy.Domain.Entities;
using CosmosDBMultiTenancy.Domain.Repositories.Documents;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace CosmosDBMultiTenancy.Domain.Repositories
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public interface IInvoiceRepository : ICosmosDBRepository<Invoice, IInvoiceDocument>
    {
    }
}
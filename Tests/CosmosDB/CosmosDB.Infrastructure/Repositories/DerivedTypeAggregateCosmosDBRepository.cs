using System.Threading;
using System.Threading.Tasks;
using CosmosDB.Application.Common.Interfaces;
using CosmosDB.Domain.Entities;
using CosmosDB.Domain.Repositories;
using CosmosDB.Domain.Repositories.Documents;
using CosmosDB.Infrastructure.Persistence;
using CosmosDB.Infrastructure.Persistence.Documents;
using Intent.RoslynWeaver.Attributes;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Options;
using Microsoft.Azure.CosmosRepository.Providers;
using Microsoft.Extensions.Options;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.CosmosDB.CosmosDBRepository", Version = "1.0")]

namespace CosmosDB.Infrastructure.Repositories
{
    internal class DerivedTypeAggregateCosmosDBRepository : CosmosDBRepositoryBase<DerivedTypeAggregate, DerivedTypeAggregateDocument, IDerivedTypeAggregateDocument>, IDerivedTypeAggregateRepository
    {
        public DerivedTypeAggregateCosmosDBRepository(CosmosDBUnitOfWork unitOfWork,
            Microsoft.Azure.CosmosRepository.IRepository<DerivedTypeAggregateDocument> cosmosRepository,
            ICosmosContainerProvider<DerivedTypeAggregateDocument> containerProvider,
            IOptionsMonitor<RepositoryOptions> optionsMonitor,
            ICurrentUserService currentUserService) : base(unitOfWork, cosmosRepository, "id", containerProvider, optionsMonitor, currentUserService)
        {
        }

        public async Task<DerivedTypeAggregate?> FindByIdAsync(string id, CancellationToken cancellationToken = default) => await base.FindByIdAsync(id: id, cancellationToken: cancellationToken);
    }
}
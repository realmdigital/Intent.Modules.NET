using System.Threading;
using System.Threading.Tasks;
using CosmosDB.PrivateSetters.Application.Common.Interfaces;
using CosmosDB.PrivateSetters.Domain.Entities;
using CosmosDB.PrivateSetters.Domain.Repositories;
using CosmosDB.PrivateSetters.Domain.Repositories.Documents;
using CosmosDB.PrivateSetters.Infrastructure.Persistence;
using CosmosDB.PrivateSetters.Infrastructure.Persistence.Documents;
using Intent.RoslynWeaver.Attributes;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Options;
using Microsoft.Azure.CosmosRepository.Providers;
using Microsoft.Extensions.Options;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.CosmosDB.CosmosDBRepository", Version = "1.0")]

namespace CosmosDB.PrivateSetters.Infrastructure.Repositories
{
    internal class PackageContainerCosmosDBRepository : CosmosDBRepositoryBase<PackageContainer, PackageContainerDocument, IPackageContainerDocument>, IPackageContainerRepository
    {
        public PackageContainerCosmosDBRepository(CosmosDBUnitOfWork unitOfWork,
            Microsoft.Azure.CosmosRepository.IRepository<PackageContainerDocument> cosmosRepository,
            ICosmosContainerProvider<PackageContainerDocument> containerProvider,
            IOptionsMonitor<RepositoryOptions> optionsMonitor,
            ICurrentUserService currentUserService) : base(unitOfWork, cosmosRepository, "id", containerProvider, optionsMonitor, currentUserService)
        {
        }

        public async Task<PackageContainer?> FindByIdAsync(string id, CancellationToken cancellationToken = default) => await base.FindByIdAsync(id: id, cancellationToken: cancellationToken);
    }
}
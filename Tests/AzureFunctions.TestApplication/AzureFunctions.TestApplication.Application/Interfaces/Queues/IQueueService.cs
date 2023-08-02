using System;
using System.Threading;
using System.Threading.Tasks;
using AzureFunctions.TestApplication.Application.Customers;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Contracts.ServiceContract", Version = "1.0")]

namespace AzureFunctions.TestApplication.Application.Interfaces.Queues
{
    public interface IQueueService : IDisposable
    {
        Task CreateCustomerOp(CustomerDto dto, CancellationToken cancellationToken = default);
        Task<CustomerDto> CreateCustomerOpWrapped(CustomerDto dto, CancellationToken cancellationToken = default);
    }
}
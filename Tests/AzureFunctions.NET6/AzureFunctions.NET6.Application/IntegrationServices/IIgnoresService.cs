using System;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Application.Contracts.Clients.ServiceContract", Version = "2.0")]

namespace AzureFunctions.NET6.Application.IntegrationServices
{
    public interface IIgnoresService : IDisposable
    {
        Task CommandWithIgnoreInApiAsync(CancellationToken cancellationToken = default);
        Task<bool> QueryWithIgnoreInApiAsync(CancellationToken cancellationToken = default);
    }
}
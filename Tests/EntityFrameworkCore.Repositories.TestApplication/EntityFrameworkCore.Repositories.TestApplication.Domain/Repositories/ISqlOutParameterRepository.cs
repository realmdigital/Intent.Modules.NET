using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.CustomRepositoryInterface", Version = "1.0")]

namespace EntityFrameworkCore.Repositories.TestApplication.Domain.Repositories
{
    public interface ISqlOutParameterRepository
    {
        Task<int> Sp_out_params_int(CancellationToken cancellationToken = default);
        Task<string> Sp_out_params_string(CancellationToken cancellationToken = default);
    }
}
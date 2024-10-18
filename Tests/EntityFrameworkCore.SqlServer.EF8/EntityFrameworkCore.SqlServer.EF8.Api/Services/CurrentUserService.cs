using System.Threading.Tasks;
using EntityFrameworkCore.SqlServer.EF8.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Identity.CurrentUserService", Version = "1.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService()
        {
        }

        public string? UserId { get; set; }
        public string? UserName { get; set; }

        public async Task<bool> AuthorizeAsync(string policy)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> IsInRoleAsync(string role)
        {
            return await Task.FromResult(true);
        }
    }
}
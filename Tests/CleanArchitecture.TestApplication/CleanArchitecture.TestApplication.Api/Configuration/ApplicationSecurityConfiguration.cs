using CleanArchitecture.TestApplication.Api.Services;
using CleanArchitecture.TestApplication.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Identity.ApplicationSecurityConfiguration", Version = "1.0")]

namespace CleanArchitecture.TestApplication.Api.Configuration
{
    public static class ApplicationSecurityConfiguration
    {
        public static IServiceCollection ConfigureApplicationSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
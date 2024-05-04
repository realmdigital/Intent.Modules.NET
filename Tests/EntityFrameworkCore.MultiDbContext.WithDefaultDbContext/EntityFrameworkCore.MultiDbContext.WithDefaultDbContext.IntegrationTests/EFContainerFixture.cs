using System.Reflection;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using EntityFrameworkCore.MultiDbContext.WithDefaultDbContext.Infrastructure.Persistence;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Testcontainers.MsSql;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.IntegrationTesting.EFContainerFixture", Version = "1.0")]

namespace EntityFrameworkCore.MultiDbContext.WithDefaultDbContext.IntegrationTests
{
    public class EFContainerFixture
    {
        private readonly MsSqlContainer _dbContainer;

        public EFContainerFixture()
        {
            _dbContainer = new MsSqlBuilder()
                            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                            .WithPassword("Strong_password_123!")
                            .Build();
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
                            {
                                {
                                    options.UseSqlServer(
                                        _dbContainer.GetConnectionString(),
                                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                                    options.UseLazyLoadingProxies();
                                }
                            });
            //Schema Creation
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
        }

        public void OnHostCreation(IServiceProvider services)
        {
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
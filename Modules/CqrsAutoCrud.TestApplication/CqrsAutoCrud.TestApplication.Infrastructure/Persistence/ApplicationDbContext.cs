using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CqrsAutoCrud.TestApplication.Domain.Common.Interfaces;
using CqrsAutoCrud.TestApplication.Domain.Entities;
using CqrsAutoCrud.TestApplication.Infrastructure.Persistence.Configurations;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.DbContext", Version = "1.0")]

namespace CqrsAutoCrud.TestApplication.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IOptions<DbContextConfiguration> _dbContextConfig;

        [IntentManaged(Mode.Ignore)]
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _dbContextConfig = new OptionsWrapper<DbContextConfiguration>(new DbContextConfiguration());
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IOptions<DbContextConfiguration> dbContextConfig) : base(options)
        {
            _dbContextConfig = dbContextConfig;
        }

        public DbSet<AggregateRoot> AggregateRoots { get; set; }
        public DbSet<AggregateRootLong> AggregateRootLongs { get; set; }
        public DbSet<AggregateSingleC> AggregateSingleCs { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureModel(modelBuilder);

            modelBuilder.ApplyConfiguration(new AggregateRootConfiguration());
            modelBuilder.ApplyConfiguration(new AggregateRootLongConfiguration());
            modelBuilder.ApplyConfiguration(new AggregateSingleCConfiguration());
            if (!string.IsNullOrWhiteSpace(_dbContextConfig.Value?.DefaultSchemaName))
            {
                modelBuilder.HasDefaultSchema(_dbContextConfig.Value?.DefaultSchemaName);
            }
        }

        [IntentManaged(Mode.Ignore)]
        private void ConfigureModel(ModelBuilder modelBuilder)
        {
            // Seed data
            // https://rehansaeed.com/migrating-to-entity-framework-core-seed-data/
            /* Eg.

            modelBuilder.Entity<Car>().HasData(
                new Car() { CarId = 1, Make = "Ferrari", Model = "F40" },
                new Car() { CarId = 2, Make = "Ferrari", Model = "F50" },
                new Car() { CarId = 3, Make = "Labourghini", Model = "Countach" });
            */
        }


        /// <summary>
        /// If configured to do so, a check is performed to see
        /// whether the database exist and if not will create it
        /// based on this container configuration.
        /// </summary>
        public void EnsureDbCreated()
        {
            if (_dbContextConfig.Value.EnsureDbCreated == true)
            {
                Database.EnsureCreated();
            }
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.CosmosDb.TestApplication.Application.Common.Interfaces;
using EntityFrameworkCore.CosmosDb.TestApplication.Domain.Common;
using EntityFrameworkCore.CosmosDb.TestApplication.Domain.Common.Interfaces;
using EntityFrameworkCore.CosmosDb.TestApplication.Domain.Entities;
using EntityFrameworkCore.CosmosDb.TestApplication.Domain.Entities.Associations;
using EntityFrameworkCore.CosmosDb.TestApplication.Domain.Entities.Inheritance;
using EntityFrameworkCore.CosmosDb.TestApplication.Domain.Entities.InheritanceAssociations;
using EntityFrameworkCore.CosmosDb.TestApplication.Domain.Entities.NestedComposition;
using EntityFrameworkCore.CosmosDb.TestApplication.Domain.Entities.Polymorphic;
using EntityFrameworkCore.CosmosDb.TestApplication.Domain.Entities.SoftDelete;
using EntityFrameworkCore.CosmosDb.TestApplication.Domain.Entities.ValueObjects;
using EntityFrameworkCore.CosmosDb.TestApplication.Infrastructure.Persistence.Configurations;
using EntityFrameworkCore.CosmosDb.TestApplication.Infrastructure.Persistence.Configurations.Associations;
using EntityFrameworkCore.CosmosDb.TestApplication.Infrastructure.Persistence.Configurations.Inheritance;
using EntityFrameworkCore.CosmosDb.TestApplication.Infrastructure.Persistence.Configurations.InheritanceAssociations;
using EntityFrameworkCore.CosmosDb.TestApplication.Infrastructure.Persistence.Configurations.NestedComposition;
using EntityFrameworkCore.CosmosDb.TestApplication.Infrastructure.Persistence.Configurations.Polymorphic;
using EntityFrameworkCore.CosmosDb.TestApplication.Infrastructure.Persistence.Configurations.SoftDelete;
using EntityFrameworkCore.CosmosDb.TestApplication.Infrastructure.Persistence.Configurations.ValueObjects;
using EntityFrameworkCore.CosmosDb.TestApplication.Infrastructure.Services;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.DbContext", Version = "1.0")]

namespace EntityFrameworkCore.CosmosDb.TestApplication.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDomainEventService domainEventService) : base(options)
        {
            _domainEventService = domainEventService;
        }

        [IntentManaged(Mode.Ignore)]
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _domainEventService = new DummyDomainEventService();
        }

        public DbSet<A_RequiredComposite> A_RequiredComposites { get; set; }
        public DbSet<AbstractBaseClass> AbstractBaseClasses { get; set; }
        public DbSet<AbstractBaseClassAssociated> AbstractBaseClassAssociateds { get; set; }
        public DbSet<Associated> Associateds { get; set; }
        public DbSet<B_OptionalAggregate> B_OptionalAggregates { get; set; }
        public DbSet<B_OptionalDependent> B_OptionalDependents { get; set; }
        public DbSet<Base> Bases { get; set; }
        public DbSet<BaseAssociated> BaseAssociateds { get; set; }
        public DbSet<C_RequiredComposite> C_RequiredComposites { get; set; }
        public DbSet<ClassA> ClassAs { get; set; }
        public DbSet<ClassWithSoftDelete> ClassWithSoftDeletes { get; set; }
        public DbSet<Composite> Composites { get; set; }
        public DbSet<ConcreteBaseClass> ConcreteBaseClasses { get; set; }
        public DbSet<ConcreteBaseClassAssociated> ConcreteBaseClassAssociateds { get; set; }
        public DbSet<D_MultipleDependent> D_MultipleDependents { get; set; }
        public DbSet<D_OptionalAggregate> D_OptionalAggregates { get; set; }
        public DbSet<Derived> Deriveds { get; set; }
        public DbSet<DerivedClassForAbstract> DerivedClassForAbstracts { get; set; }
        public DbSet<DerivedClassForAbstractAssociated> DerivedClassForAbstractAssociateds { get; set; }
        public DbSet<DerivedClassForConcrete> DerivedClassForConcretes { get; set; }
        public DbSet<DerivedClassForConcreteAssociated> DerivedClassForConcreteAssociateds { get; set; }
        public DbSet<DictionaryWithKvPNormal> DictionaryWithKvPNormals { get; set; }
        public DbSet<DictionaryWithKvPSerialized> DictionaryWithKvPSerializeds { get; set; }
        public DbSet<E_RequiredCompositeNav> E_RequiredCompositeNavs { get; set; }
        public DbSet<ExplicitKeyClass> ExplicitKeyClasses { get; set; }
        public DbSet<F_OptionalAggregateNav> F_OptionalAggregateNavs { get; set; }
        public DbSet<F_OptionalDependent> F_OptionalDependents { get; set; }
        public DbSet<G_RequiredCompositeNav> G_RequiredCompositeNavs { get; set; }
        public DbSet<H_MultipleDependent> H_MultipleDependents { get; set; }
        public DbSet<H_OptionalAggregateNav> H_OptionalAggregateNavs { get; set; }
        public DbSet<ImplicitKeyClass> ImplicitKeyClasses { get; set; }
        public DbSet<J_MultipleAggregate> J_MultipleAggregates { get; set; }
        public DbSet<J_RequiredDependent> J_RequiredDependents { get; set; }
        public DbSet<K_SelfReference> K_SelfReferences { get; set; }
        public DbSet<M_SelfReferenceBiNav> M_SelfReferenceBiNavs { get; set; }
        public DbSet<PersonWithAddressNormal> PersonWithAddressNormals { get; set; }
        public DbSet<PersonWithAddressSerialized> PersonWithAddressSerializeds { get; set; }
        public DbSet<Poly_BaseClassNonAbstract> Poly_BaseClassNonAbstracts { get; set; }
        public DbSet<Poly_ConcreteA> Poly_ConcreteAs { get; set; }
        public DbSet<Poly_ConcreteB> Poly_ConcreteBs { get; set; }
        public DbSet<Poly_SecondLevel> Poly_SecondLevels { get; set; }
        public DbSet<StandaloneDerived> StandaloneDeriveds { get; set; }
        public DbSet<WeirdClass> WeirdClasses { get; set; }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            SetSoftDeleteProperties();
            await DispatchEventsAsync(cancellationToken);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureModel(modelBuilder);
            modelBuilder.ApplyConfiguration(new ExplicitKeyClassConfiguration());
            modelBuilder.ApplyConfiguration(new ImplicitKeyClassConfiguration());
            modelBuilder.ApplyConfiguration(new A_RequiredCompositeConfiguration());
            modelBuilder.ApplyConfiguration(new B_OptionalAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new B_OptionalDependentConfiguration());
            modelBuilder.ApplyConfiguration(new C_RequiredCompositeConfiguration());
            modelBuilder.ApplyConfiguration(new D_MultipleDependentConfiguration());
            modelBuilder.ApplyConfiguration(new D_OptionalAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new E_RequiredCompositeNavConfiguration());
            modelBuilder.ApplyConfiguration(new F_OptionalAggregateNavConfiguration());
            modelBuilder.ApplyConfiguration(new F_OptionalDependentConfiguration());
            modelBuilder.ApplyConfiguration(new G_RequiredCompositeNavConfiguration());
            modelBuilder.ApplyConfiguration(new H_MultipleDependentConfiguration());
            modelBuilder.ApplyConfiguration(new H_OptionalAggregateNavConfiguration());
            modelBuilder.ApplyConfiguration(new J_MultipleAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new J_RequiredDependentConfiguration());
            modelBuilder.ApplyConfiguration(new K_SelfReferenceConfiguration());
            modelBuilder.ApplyConfiguration(new M_SelfReferenceBiNavConfiguration());
            modelBuilder.ApplyConfiguration(new AssociatedConfiguration());
            modelBuilder.ApplyConfiguration(new BaseConfiguration());
            modelBuilder.ApplyConfiguration(new BaseAssociatedConfiguration());
            modelBuilder.ApplyConfiguration(new CompositeConfiguration());
            modelBuilder.ApplyConfiguration(new DerivedConfiguration());
            modelBuilder.ApplyConfiguration(new WeirdClassConfiguration());
            modelBuilder.ApplyConfiguration(new AbstractBaseClassConfiguration());
            modelBuilder.ApplyConfiguration(new AbstractBaseClassAssociatedConfiguration());
            modelBuilder.ApplyConfiguration(new ConcreteBaseClassConfiguration());
            modelBuilder.ApplyConfiguration(new ConcreteBaseClassAssociatedConfiguration());
            modelBuilder.ApplyConfiguration(new DerivedClassForAbstractConfiguration());
            modelBuilder.ApplyConfiguration(new DerivedClassForAbstractAssociatedConfiguration());
            modelBuilder.ApplyConfiguration(new DerivedClassForConcreteConfiguration());
            modelBuilder.ApplyConfiguration(new DerivedClassForConcreteAssociatedConfiguration());
            modelBuilder.ApplyConfiguration(new StandaloneDerivedConfiguration());
            modelBuilder.ApplyConfiguration(new ClassAConfiguration());
            modelBuilder.ApplyConfiguration(new Poly_BaseClassNonAbstractConfiguration());
            modelBuilder.ApplyConfiguration(new Poly_ConcreteAConfiguration());
            modelBuilder.ApplyConfiguration(new Poly_ConcreteBConfiguration());
            modelBuilder.ApplyConfiguration(new Poly_SecondLevelConfiguration());
            modelBuilder.ApplyConfiguration(new ClassWithSoftDeleteConfiguration());
            modelBuilder.ApplyConfiguration(new DictionaryWithKvPNormalConfiguration());
            modelBuilder.ApplyConfiguration(new DictionaryWithKvPSerializedConfiguration());
            modelBuilder.ApplyConfiguration(new PersonWithAddressNormalConfiguration());
            modelBuilder.ApplyConfiguration(new PersonWithAddressSerializedConfiguration());
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
        /// Calling EnsureCreatedAsync is necessary to create the required containers and insert the seed data if present in the model. 
        /// However EnsureCreatedAsync should only be called during deployment, not normal operation, as it may cause performance issues.
        /// </summary>
        public async Task EnsureDbCreatedAsync()
        {
            await Database.EnsureCreatedAsync();
        }

        private async Task DispatchEventsAsync(CancellationToken cancellationToken = default)
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker
                    .Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .FirstOrDefault(domainEvent => !domainEvent.IsPublished);

                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity, cancellationToken);
            }
        }

        private void SetSoftDeleteProperties()
        {
            ChangeTracker.DetectChanges();

            var entities = ChangeTracker
                .Entries()
                .Where(t => t.Entity is ISoftDelete && t.State == EntityState.Deleted)
                .ToArray();

            if (!entities.Any())
            {
                return;
            }

            foreach (var entry in entities)
            {
                var entity = (ISoftDelete)entry.Entity;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
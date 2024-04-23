using EntityFrameworkCore.SqlServer.EF8.Domain.Entities.TPC.Polymorphic;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Infrastructure.Persistence.Configurations.TPC.Polymorphic
{
    public class TPC_Poly_ConcreteBConfiguration : IEntityTypeConfiguration<TPC_Poly_ConcreteB>
    {
        public void Configure(EntityTypeBuilder<TPC_Poly_ConcreteB> builder)
        {
            builder.ToTable("TpcPoly_ConcreteB");

            builder.Property(x => x.ConcreteField)
                .IsRequired();
        }
    }
}
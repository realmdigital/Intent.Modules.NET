using EntityFrameworkCore.SqlServer.EF8.Domain.Entities.NestedAssociations;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Infrastructure.Persistence.Configurations.NestedAssociations
{
    public class InhabitantConfiguration : IEntityTypeConfiguration<Inhabitant>
    {
        public void Configure(EntityTypeBuilder<Inhabitant> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.InhabitantAttribute)
                .IsRequired();
        }
    }
}
using EntityFrameworkCore.Postgres.Domain.Entities.SoftDelete;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace EntityFrameworkCore.Postgres.Infrastructure.Persistence.Configurations.SoftDelete
{
    public class AbstractParentWithTableConfiguration : IEntityTypeConfiguration<AbstractParentWithTable>
    {
        public void Configure(EntityTypeBuilder<AbstractParentWithTable> builder)
        {
            builder.ToTable("Parent");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.HasQueryFilter(t => t.IsDeleted == false);
        }
    }
}
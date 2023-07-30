using Entities.PrivateSetters.TestApplication.Domain.Entities.Aggregational;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace Entities.PrivateSetters.TestApplication.Infrastructure.Persistence.Configurations.Aggregational
{
    public class ManyToOneSourceConfiguration : IEntityTypeConfiguration<ManyToOneSource>
    {
        public void Configure(EntityTypeBuilder<ManyToOneSource> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ManyToOneDestId)
                .IsRequired();

            builder.Property(x => x.Attribute)
                .IsRequired();

            builder.HasOne(x => x.ManyToOneDest)
                .WithMany(x => x.ManyToOneSources)
                .HasForeignKey(x => x.ManyToOneDestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
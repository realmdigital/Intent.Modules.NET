using CleanArchitecture.Comprehensive.Domain.Entities.General;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace CleanArchitecture.Comprehensive.Infrastructure.Persistence.Configurations.General
{
    public class CustomMappingConfiguration : IEntityTypeConfiguration<CustomMapping>
    {
        public void Configure(EntityTypeBuilder<CustomMapping> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.When);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}
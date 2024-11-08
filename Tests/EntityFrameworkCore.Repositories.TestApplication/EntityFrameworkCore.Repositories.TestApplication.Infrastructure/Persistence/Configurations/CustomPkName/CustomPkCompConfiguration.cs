using EntityFrameworkCore.Repositories.TestApplication.Domain.Entities.CustomPkName;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace EntityFrameworkCore.Repositories.TestApplication.Infrastructure.Persistence.Configurations.CustomPkName
{
    public class CustomPkCompConfiguration : IEntityTypeConfiguration<CustomPkComp>
    {
        public void Configure(EntityTypeBuilder<CustomPkComp> builder)
        {
            builder.HasKey(x => x.MyId);

            builder.Property(x => x.MyId2)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Ignore(e => e.DomainEvents);
        }
    }
}
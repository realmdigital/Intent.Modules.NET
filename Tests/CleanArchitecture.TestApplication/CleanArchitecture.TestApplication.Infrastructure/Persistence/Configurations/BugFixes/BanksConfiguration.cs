using CleanArchitecture.TestApplication.Domain.Entities.BugFixes;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace CleanArchitecture.TestApplication.Infrastructure.Persistence.Configurations.BugFixes
{
    public class BanksConfiguration : IEntityTypeConfiguration<Banks>
    {
        public void Configure(EntityTypeBuilder<Banks> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}
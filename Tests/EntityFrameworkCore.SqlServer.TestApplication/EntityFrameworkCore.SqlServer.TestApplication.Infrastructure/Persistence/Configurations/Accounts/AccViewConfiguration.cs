using EntityFrameworkCore.SqlServer.TestApplication.Domain.Entities.Accounts;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace EntityFrameworkCore.SqlServer.TestApplication.Infrastructure.Persistence.Configurations.Accounts
{
    public class AccViewConfiguration : IEntityTypeConfiguration<AccView>
    {
        public void Configure(EntityTypeBuilder<AccView> builder)
        {
            builder.ToView("AccViews", "accounts");

            builder.HasKey(x => x.Id);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}
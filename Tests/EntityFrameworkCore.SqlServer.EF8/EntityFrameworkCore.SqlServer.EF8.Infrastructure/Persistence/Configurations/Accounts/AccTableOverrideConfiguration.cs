using EntityFrameworkCore.SqlServer.EF8.Domain.Entities.Accounts;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Infrastructure.Persistence.Configurations.Accounts
{
    public class AccTableOverrideConfiguration : IEntityTypeConfiguration<AccTableOverride>
    {
        public void Configure(EntityTypeBuilder<AccTableOverride> builder)
        {
            builder.ToTable("AccTableOverrides", "imoutofcontrol");

            builder.HasKey(x => x.Id);
        }
    }
}
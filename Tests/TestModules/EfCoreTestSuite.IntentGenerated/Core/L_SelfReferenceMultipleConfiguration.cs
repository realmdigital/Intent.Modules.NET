using System;
using EfCoreTestSuite.IntentGenerated.Entities;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace EfCoreTestSuite.IntentGenerated.Core
{
    public class L_SelfReferenceMultipleConfiguration : IEntityTypeConfiguration<L_SelfReferenceMultiple>
    {
        public void Configure(EntityTypeBuilder<L_SelfReferenceMultiple> builder)
        {
            builder.HasKey(x => x.Id);


            builder.HasMany(x => x.L_SelfReferenceMultiples)
                .WithMany(x => x.L_SelfReferenceMultiples)
                .UsingEntity(x => x.ToTable("L_SelfReferenceMultipleL_SelfReferenceMultiples"));

        }
    }
}
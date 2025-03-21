using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace EntityFrameworkCore.MySql.Domain.Entities.Indexes
{
    public class SortDirectionIndex
    {
        public SortDirectionIndex()
        {
            FieldA = null!;
            FieldB = null!;
        }
        public Guid Id { get; set; }

        public string FieldA { get; set; }

        public string FieldB { get; set; }
    }
}
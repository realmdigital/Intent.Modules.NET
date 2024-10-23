using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace EntityFrameworkCore.Postgres.Domain.Entities.NestedAssociations
{
    public class Tree
    {
        public Guid Id { get; set; }

        public string TreeAttribute { get; set; }

        public virtual ICollection<Branch> Branches { get; set; } = [];
    }
}
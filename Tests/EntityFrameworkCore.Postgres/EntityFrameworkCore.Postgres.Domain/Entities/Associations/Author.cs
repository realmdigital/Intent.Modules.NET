using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace EntityFrameworkCore.Postgres.Domain.Entities.Associations
{
    public class Author
    {
        public Author()
        {
            Name = null!;
            Surname = null!;
            DisplayName = null!;
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string DisplayName { get; set; }
    }
}
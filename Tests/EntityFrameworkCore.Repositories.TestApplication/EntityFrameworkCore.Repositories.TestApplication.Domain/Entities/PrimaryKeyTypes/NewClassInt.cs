using System.Collections.Generic;
using EntityFrameworkCore.Repositories.TestApplication.Domain.Common;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace EntityFrameworkCore.Repositories.TestApplication.Domain.Entities.PrimaryKeyTypes
{
    public class NewClassInt : IHasDomainEvent
    {
        public NewClassInt()
        {
            IntName = null!;
        }
        public int Id { get; set; }

        public string IntName { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = [];
    }
}
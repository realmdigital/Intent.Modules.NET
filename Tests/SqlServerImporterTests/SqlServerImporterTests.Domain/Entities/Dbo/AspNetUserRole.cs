using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;
using SqlServerImporterTests.Domain.Common;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace SqlServerImporterTests.Domain.Entities.Dbo
{
    public class AspNetUserRole : IHasDomainEvent
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }

        public virtual AspNetRole RoleIdAspNetRoles { get; set; }

        public virtual AspNetUser UserIdAspNetUsers { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
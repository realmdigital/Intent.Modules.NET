using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Domain.Entities
{
    public class TablePlain
    {
        public Guid Id { get; set; }
    }
}
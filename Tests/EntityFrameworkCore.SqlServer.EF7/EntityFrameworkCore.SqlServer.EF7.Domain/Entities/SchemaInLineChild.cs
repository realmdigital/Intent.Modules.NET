using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace EntityFrameworkCore.SqlServer.EF7.Domain.Entities
{
    public class SchemaInLineChild
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
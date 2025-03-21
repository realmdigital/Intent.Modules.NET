using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Domain.Entities.Associations
{
    public class A_OptionalDependent
    {
        public A_OptionalDependent()
        {
            OptionalDepAttr = null!;
        }
        public Guid Id { get; set; }

        public string OptionalDepAttr { get; set; }
    }
}
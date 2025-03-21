using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Domain.Entities.BasicAudit
{
    public class Audit_DerivedClass : Audit_BaseClass
    {
        public Audit_DerivedClass()
        {
            DerivedAttr = null!;
        }
        public string DerivedAttr { get; set; }
    }
}
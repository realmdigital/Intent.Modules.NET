using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace EntityFrameworkCore.Postgres.Domain.Entities.TPH.InheritanceAssociations
{
    public class TPH_DerivedClassForAbstract : TPH_AbstractBaseClass
    {
        public TPH_DerivedClassForAbstract()
        {
            DerivedAttribute = null!;
        }
        public string DerivedAttribute { get; set; }
    }
}
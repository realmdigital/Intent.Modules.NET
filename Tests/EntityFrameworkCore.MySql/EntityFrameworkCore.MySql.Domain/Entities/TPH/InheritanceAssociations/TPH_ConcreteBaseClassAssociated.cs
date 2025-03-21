using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace EntityFrameworkCore.MySql.Domain.Entities.TPH.InheritanceAssociations
{
    public class TPH_ConcreteBaseClassAssociated
    {
        public TPH_ConcreteBaseClassAssociated()
        {
            AssociatedField = null!;
            ConcreteBaseClass = null!;
        }
        public Guid Id { get; set; }

        public string AssociatedField { get; set; }

        public Guid ConcreteBaseClassId { get; set; }

        public virtual TPH_ConcreteBaseClass ConcreteBaseClass { get; set; }
    }
}
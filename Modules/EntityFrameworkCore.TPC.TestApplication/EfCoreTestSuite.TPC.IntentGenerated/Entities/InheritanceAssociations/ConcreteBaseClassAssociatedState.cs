using System;
using System.Collections.Generic;
using EfCoreTestSuite.TPC.IntentGenerated.Core;
using EfCoreTestSuite.TPC.IntentGenerated.DomainEvents;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.DomainEntityState", Version = "1.0")]

namespace EfCoreTestSuite.TPC.IntentGenerated.Entities.InheritanceAssociations
{

    public partial class ConcreteBaseClassAssociated : IConcreteBaseClassAssociated, IHasDomainEvent
    {

        public Guid Id { get; set; }

        public string AssociatedField { get; set; }


        public Guid ConcreteBaseClassId { get; set; }

        public virtual ConcreteBaseClass ConcreteBaseClass { get; set; }

        IConcreteBaseClass IConcreteBaseClassAssociated.ConcreteBaseClass
        {
            get => ConcreteBaseClass;
            set => ConcreteBaseClass = (ConcreteBaseClass)value;
        }



        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}

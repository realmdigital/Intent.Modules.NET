using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.DomainEntityInterface", Version = "1.0")]

namespace EfCoreTestSuite.TPC.IntentGenerated.Entities.InheritanceAssociations
{

    public interface IConcreteBaseClassAssociated
    {
        string AssociatedField { get; set; }

        IConcreteBaseClass ConcreteBaseClass { get; set; }

    }
}

using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Ignore)]
[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "1.0")]

namespace EfCoreTestSuite.TPT.IntentGenerated.Entities
{
    [IntentManaged(Mode.Merge)]
    [DefaultIntentManaged(Mode.Merge, Signature = Mode.Fully, Body = Mode.Ignore, Targets = Targets.Methods, AccessModifiers = AccessModifiers.Public)]
    public partial class FkAssociatedClass
    {

    }
}
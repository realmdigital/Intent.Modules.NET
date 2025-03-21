using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace MongoDb.TestApplication.Domain.Entities.NestedAssociations
{
    [DefaultIntentManaged(Mode.Fully, Targets = Targets.Methods, Body = Mode.Ignore, AccessModifiers = AccessModifiers.Public)]
    public class AggregateA
    {
        public AggregateA()
        {
            Id = null!;
            Attribute = null!;
            NestedCompositionA = null!;
        }
        public string Id { get; set; }

        public string Attribute { get; set; }

        public NestedCompositionA NestedCompositionA { get; set; }
    }
}
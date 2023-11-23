using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTagModeImplicit]

namespace CleanArchitecture.TestApplication.Domain.Entities.CRUD
{
    [DefaultIntentManaged(Mode.Fully, Targets = Targets.Methods, Body = Mode.Ignore, AccessModifiers = AccessModifiers.Public)]
    public class CompositeSingleAA
    {

        public Guid Id { get; set; }

        public string CompositeAttr { get; set; }
    }
}
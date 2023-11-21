using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTagModeImplicit]

namespace GraphQL.AzureFunction.TestApplication.Domain.Entities
{
    [DefaultIntentManaged(Mode.Fully, Targets = Targets.Methods, Body = Mode.Ignore, AccessModifiers = AccessModifiers.Public)]
    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
    }
}
using System;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "1.0")]

namespace CleanArchitecture.TestApplication.Domain.Entities.DDD
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    [DefaultIntentManaged(Mode.Fully, Targets = Targets.Properties)]
    [DefaultIntentManaged(Mode.Fully, Targets = Targets.Methods, Body = Mode.Ignore, AccessModifiers = AccessModifiers.Public)]
    public class Account
    {
        public Guid Id { get; set; }

        public string AccNumber { get; set; }

        public Guid AccountHolderId { get; set; }

        public string Note { get; set; }

        [IntentManaged(Mode.Fully, Body = Mode.Merge)]
        public string UpdateNote(string note)
        {
            Note = note;
            return note;
        }
    }
}
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.DomainEnum", Version = "1.0")]

namespace Redis.Om.Repositories.Domain
{
    public enum ClientType
    {
        Individual,
        Business
    }
}
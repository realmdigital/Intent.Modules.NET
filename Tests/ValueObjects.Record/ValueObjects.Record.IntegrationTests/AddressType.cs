using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.IntegrationTesting.EnumContract", Version = "1.0")]

namespace ValueObjects.Record.IntegrationTests
{
    public enum AddressType
    {
        Residential,
        Office
    }
}
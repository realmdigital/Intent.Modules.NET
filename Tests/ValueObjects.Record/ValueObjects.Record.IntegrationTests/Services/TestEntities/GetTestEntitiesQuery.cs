using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.AspNetCore.IntegrationTesting.DtoContract", Version = "2.0")]

namespace ValueObjects.Record.IntegrationTests.Services.TestEntities
{
    public class GetTestEntitiesQuery
    {
        public static GetTestEntitiesQuery Create()
        {
            return new GetTestEntitiesQuery
            {
            };
        }
    }
}
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.AspNetCore.IntegrationTesting.DtoContract", Version = "2.0")]

namespace AdvancedMappingCrud.Cosmos.Tests.IntegrationTests.Services.Orders
{
    public class UpdateOrderCommandOrderTagsDto
    {
        public UpdateOrderCommandOrderTagsDto()
        {
            Name = null!;
            Value = null!;
        }

        public string Name { get; set; }
        public string Value { get; set; }

        public static UpdateOrderCommandOrderTagsDto Create(string name, string value)
        {
            return new UpdateOrderCommandOrderTagsDto
            {
                Name = name,
                Value = value
            };
        }
    }
}
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace AdvancedMappingCrud.RichDomain.SeparatedEntityState.Tests.Application.People
{
    public class UpdatePersonDetailsNameDto
    {
        public UpdatePersonDetailsNameDto()
        {
            First = null!;
            Last = null!;
        }

        public string First { get; set; }
        public string Last { get; set; }

        public static UpdatePersonDetailsNameDto Create(string first, string last)
        {
            return new UpdatePersonDetailsNameDto
            {
                First = first,
                Last = last
            };
        }
    }
}
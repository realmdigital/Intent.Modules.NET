using System.ComponentModel.DataAnnotations;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.DtoContract", Version = "2.0")]

namespace CleanArchitecture.TestApplication.BlazorClient.HttpClients.Contracts.Services.UniqueIndexConstraint
{
    public class CreateAggregateWithUniqueConstraintIndexElementCommand
    {
        public CreateAggregateWithUniqueConstraintIndexElementCommand()
        {
            SingleUniqueField = null!;
            CompUniqueFieldA = null!;
            CompUniqueFieldB = null!;
        }

        [Required(ErrorMessage = "Single unique field is required.")]
        [MaxLength(256, ErrorMessage = "Single unique field must be 256 or less characters.")]
        public string SingleUniqueField { get; set; }
        [Required(ErrorMessage = "Comp unique field a is required.")]
        [MaxLength(256, ErrorMessage = "Comp unique field a must be 256 or less characters.")]
        public string CompUniqueFieldA { get; set; }
        [Required(ErrorMessage = "Comp unique field b is required.")]
        [MaxLength(256, ErrorMessage = "Comp unique field b must be 256 or less characters.")]
        public string CompUniqueFieldB { get; set; }

        public static CreateAggregateWithUniqueConstraintIndexElementCommand Create(
            string singleUniqueField,
            string compUniqueFieldA,
            string compUniqueFieldB)
        {
            return new CreateAggregateWithUniqueConstraintIndexElementCommand
            {
                SingleUniqueField = singleUniqueField,
                CompUniqueFieldA = compUniqueFieldA,
                CompUniqueFieldB = compUniqueFieldB
            };
        }
    }
}
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.FluentValidation.Dtos.DTOValidator", Version = "2.0")]

namespace AdvancedMappingCrud.Repositories.Tests.Application.ParentWithAnemicChildren
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class CreateParentWithAnemicChildAnemicChildDtoValidator : AbstractValidator<CreateParentWithAnemicChildAnemicChildDto>
    {
        [IntentManaged(Mode.Merge)]
        public CreateParentWithAnemicChildAnemicChildDtoValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Line1)
                .NotNull();

            RuleFor(v => v.Line2)
                .NotNull();

            RuleFor(v => v.City)
                .NotNull();
        }
    }
}
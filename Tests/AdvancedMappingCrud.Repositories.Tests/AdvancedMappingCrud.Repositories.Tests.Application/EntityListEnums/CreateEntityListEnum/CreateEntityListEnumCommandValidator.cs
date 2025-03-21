using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AdvancedMappingCrud.Repositories.Tests.Application.EntityListEnums.CreateEntityListEnum
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class CreateEntityListEnumCommandValidator : AbstractValidator<CreateEntityListEnumCommand>
    {
        [IntentManaged(Mode.Merge)]
        public CreateEntityListEnumCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Name)
                .NotNull();

            RuleFor(v => v.OrderStatuses)
                .NotNull()
                .ForEach(x => x.IsInEnum());
        }
    }
}
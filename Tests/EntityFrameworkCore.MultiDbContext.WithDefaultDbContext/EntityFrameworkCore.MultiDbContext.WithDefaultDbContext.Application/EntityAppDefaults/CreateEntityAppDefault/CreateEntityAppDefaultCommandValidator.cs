using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace EntityFrameworkCore.MultiDbContext.WithDefaultDbContext.Application.EntityAppDefaults.CreateEntityAppDefault
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class CreateEntityAppDefaultCommandValidator : AbstractValidator<CreateEntityAppDefaultCommand>
    {
        [IntentManaged(Mode.Merge)]
        public CreateEntityAppDefaultCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Message)
                .NotNull();
        }
    }
}
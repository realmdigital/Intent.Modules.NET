using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace IntegrationTesting.Tests.Application.HasMissingDeps.DeleteHasMissingDep
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class DeleteHasMissingDepCommandValidator : AbstractValidator<DeleteHasMissingDepCommand>
    {
        [IntentManaged(Mode.Merge)]
        public DeleteHasMissingDepCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}
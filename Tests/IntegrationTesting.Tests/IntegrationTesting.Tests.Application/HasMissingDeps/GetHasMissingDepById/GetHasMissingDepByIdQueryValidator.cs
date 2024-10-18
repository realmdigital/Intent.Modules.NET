using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace IntegrationTesting.Tests.Application.HasMissingDeps.GetHasMissingDepById
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class GetHasMissingDepByIdQueryValidator : AbstractValidator<GetHasMissingDepByIdQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetHasMissingDepByIdQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}
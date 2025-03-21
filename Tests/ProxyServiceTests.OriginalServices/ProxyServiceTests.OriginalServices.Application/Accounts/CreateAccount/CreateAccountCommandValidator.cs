using FluentValidation;
using Intent.RoslynWeaver.Attributes;
using ProxyServiceTests.OriginalServices.Application.Common.Validation;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace ProxyServiceTests.OriginalServices.Application.Accounts.CreateAccount
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        [IntentManaged(Mode.Merge)]
        public CreateAccountCommandValidator(IValidatorProvider provider)
        {
            ConfigureValidationRules(provider);
        }

        private void ConfigureValidationRules(IValidatorProvider provider)
        {

            RuleFor(v => v.Amount)
                .NotNull()
                .SetValidator(provider.GetValidator<CreateAccountMoneyDto>()!);

            RuleFor(v => v.Number)
                .NotNull();
        }
    }
}
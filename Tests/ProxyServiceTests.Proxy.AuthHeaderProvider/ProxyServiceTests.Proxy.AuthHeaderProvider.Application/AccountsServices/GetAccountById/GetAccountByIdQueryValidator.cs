using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace ProxyServiceTests.Proxy.AuthHeaderProvider.Application.AccountsServices.GetAccountById
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class GetAccountByIdQueryValidator : AbstractValidator<GetAccountByIdQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetAccountByIdQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}
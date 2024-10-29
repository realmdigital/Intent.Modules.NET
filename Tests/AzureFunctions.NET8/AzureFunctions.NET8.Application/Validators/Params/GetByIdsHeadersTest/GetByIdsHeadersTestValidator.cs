using System;
using AzureFunctions.NET6.Application.Params.GetByIdsHeadersTest;
using AzureFunctions.NET8.Application.Params.GetByIdsHeadersTest;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace AzureFunctions.NET8.Application.Validators.Params.GetByIdsHeadersTest
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class GetByIdsHeadersTestValidator : AbstractValidator<Application.Params.GetByIdsHeadersTest.GetByIdsHeadersTest>
    {
        [IntentManaged(Mode.Fully, Body = Mode.Merge, Signature = Mode.Merge)]
        public GetByIdsHeadersTestValidator()
        {
            ConfigureValidationRules();
        }

        [IntentManaged(Mode.Fully)]
        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Ids)
                .NotNull();
        }
    }
}
using System;
using AzureFunctions.NET8.Application.Params.FromBodyTest;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AzureFunctions.NET8.Application.Validators.Params.FromBodyTest
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class FromBodyTestCommandValidator : AbstractValidator<FromBodyTestCommand>
    {
        [IntentManaged(Mode.Fully, Body = Mode.Merge, Signature = Mode.Merge)]
        public FromBodyTestCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Ids)
                .NotNull();
        }
    }
}
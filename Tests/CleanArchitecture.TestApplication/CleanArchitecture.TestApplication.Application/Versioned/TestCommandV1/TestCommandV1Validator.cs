using System;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace CleanArchitecture.TestApplication.Application.Versioned.TestCommandV1
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class TestCommandV1Validator : AbstractValidator<TestCommandV1>
    {
        [IntentManaged(Mode.Merge)]
        public TestCommandV1Validator()
        {
            ConfigureValidationRules();

        }

        [IntentManaged(Mode.Fully)]
        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Value)
                .NotNull();
        }
    }
}
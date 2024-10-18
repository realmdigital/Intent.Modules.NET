using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace EntityFrameworkCore.Repositories.TestApplication.Application.AggregateRoot1RepoOperationInvocation.Operation_Params1_ReturnsD_Collection1Async
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class Operation_Params1_ReturnsD_Collection1AsyncQueryValidator : AbstractValidator<Operation_Params1_ReturnsD_Collection1AsyncQuery>
    {
        [IntentManaged(Mode.Merge)]
        public Operation_Params1_ReturnsD_Collection1AsyncQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.AttributeBinary)
                .NotNull();

            RuleFor(v => v.AttributeString)
                .NotNull();
        }
    }
}
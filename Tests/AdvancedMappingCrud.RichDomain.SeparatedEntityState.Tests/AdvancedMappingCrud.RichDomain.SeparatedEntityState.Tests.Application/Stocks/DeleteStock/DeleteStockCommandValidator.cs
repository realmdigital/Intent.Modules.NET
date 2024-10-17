using AdvancedMappingCrud.RichDomain.SeparatedEntityState.Tests.Application.Common.Validation;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AdvancedMappingCrud.RichDomain.SeparatedEntityState.Tests.Application.Stocks.DeleteStock
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class DeleteStockCommandValidator : AbstractValidator<DeleteStockCommand>
    {
        [IntentManaged(Mode.Merge)]
        public DeleteStockCommandValidator(IValidatorProvider provider)
        {
            ConfigureValidationRules(provider);
        }

        private void ConfigureValidationRules(IValidatorProvider provider)
        {
            RuleFor(v => v.Data)
                .NotNull()
                .SetValidator(provider.GetValidator<StockDto>()!);
        }
    }
}
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.FluentValidation.Dtos.DTOValidator", Version = "2.0")]

namespace AdvancedMappingCrud.RichDomain.Tests.Application.Stocks
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class StockDtoValidator : AbstractValidator<StockDto>
    {
        [IntentManaged(Mode.Merge)]
        public StockDtoValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            Include(new BaseStockDtoValidator());

            RuleFor(v => v.Name)
                .NotNull();
        }
    }
}
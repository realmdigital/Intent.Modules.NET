using AdvancedMappingCrud.Repositories.Tests.Application.Common.Validation;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AdvancedMappingCrud.Repositories.Tests.Application.Farmers.AddPlotFarmer
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class AddPlotFarmerCommandValidator : AbstractValidator<AddPlotFarmerCommand>
    {
        [IntentManaged(Mode.Merge)]
        public AddPlotFarmerCommandValidator(IValidatorProvider provider)
        {
            ConfigureValidationRules(provider);
        }

        private void ConfigureValidationRules(IValidatorProvider provider)
        {
            RuleFor(v => v.Address)
                .NotNull()
                .SetValidator(provider.GetValidator<AddPlotDto>()!);
        }
    }
}
using System;
using System.Text.RegularExpressions;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace CleanArchitecture.Comprehensive.Application.AccountHolders.UpdateNoteAccountHolder
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class UpdateNoteAccountHolderCommandValidator : AbstractValidator<UpdateNoteAccountHolderCommand>
    {
        [IntentManaged(Mode.Merge)]
        public UpdateNoteAccountHolderCommandValidator()
        {
            ConfigureValidationRules();

        }

        [IntentManaged(Mode.Fully)]
        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Note)
                .NotNull()
                .Matches(new Regex(@"^[a-z]*$", RegexOptions.Compiled, TimeSpan.FromSeconds(1)))
                .WithMessage("Lower case only notes");
        }
    }
}
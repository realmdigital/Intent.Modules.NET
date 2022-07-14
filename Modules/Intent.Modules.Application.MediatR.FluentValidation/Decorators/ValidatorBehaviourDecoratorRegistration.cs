using System.ComponentModel;
using Intent.Engine;
using Intent.Modules.Application.FluentValidation.Templates.ValidationBehaviour;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorRegistration", Version = "1.0")]

namespace Intent.Modules.Application.MediatR.FluentValidation.Decorators
{
    [Description(ValidatorBehaviourDecorator.DecoratorId)]
    public class ValidatorBehaviourDecoratorRegistration : DecoratorRegistration<ValidationBehaviourTemplate, ValidationBehaviourContract>
    {
        public override ValidationBehaviourContract CreateDecoratorInstance(ValidationBehaviourTemplate template, IApplication application)
        {
            return new ValidatorBehaviourDecorator(template, application);
        }

        public override string DecoratorId => ValidatorBehaviourDecorator.DecoratorId;
    }
}
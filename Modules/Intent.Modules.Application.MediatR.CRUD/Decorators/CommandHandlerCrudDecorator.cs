using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Application.MediatR.CRUD.CrudMappingStrategies;
using Intent.Modules.Application.MediatR.CRUD.CrudStrategies;
using Intent.Modules.Application.MediatR.Templates;
using Intent.Modules.Application.MediatR.Templates.CommandHandler;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.Builder;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Utils;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecorator", Version = "1.0")]

namespace Intent.Modules.Application.MediatR.CRUD.Decorators
{
    [IntentManaged(Mode.Merge)]
    public class CommandHandlerCrudDecorator : CommandHandlerDecorator
    {
        [IntentManaged(Mode.Fully)]
        public const string DecoratorId = "Intent.Application.MediatR.CRUD.CommandHandlerCrudDecorator";

        [IntentManaged(Mode.Fully)]
        private readonly CommandHandlerTemplate _template;
        [IntentManaged(Mode.Fully)]
        private readonly IApplication _application;

        [IntentManaged(Mode.Merge)]
        public CommandHandlerCrudDecorator(CommandHandlerTemplate template, IApplication application)
        {
            _template = template;
            _application = application;

            var matchedStrategy = StrategyFactory.GetMatchedCommandStrategy(template);
            if (matchedStrategy is not null)
            {
                template.CSharpFile.AfterBuild(file => matchedStrategy.ApplyStrategy());
            };
        }
    }
}
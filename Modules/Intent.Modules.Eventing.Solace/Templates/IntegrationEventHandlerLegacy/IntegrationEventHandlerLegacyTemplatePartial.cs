using System;
using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.Eventing.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.Builder;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.Templates;
using Intent.Modules.Eventing.Shared.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.CSharp.Templates.CSharpTemplatePartial", Version = "1.0")]

namespace Intent.Modules.Eventing.Solace.Templates.IntegrationEventHandlerLegacy
{
    [IntentManaged(Mode.Ignore)]
    public partial class IntegrationEventHandlerLegacyTemplate : IntegrationEventHandlerImplementationTemplateBase
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.Eventing.Solace.IntegrationEventHandlerLegacy";

        public IntegrationEventHandlerLegacyTemplate(IOutputTarget outputTarget, MessageSubscribeAssocationTargetEndModel model)
            : base(
                templateId: TemplateId,
                outputTarget: outputTarget,
                model: model)
        {
        }
    }
}
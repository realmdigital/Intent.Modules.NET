using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.UI.Api;
using Intent.Modules.Blazor.Settings;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.FilePerModel", Version = "1.0")]

namespace Intent.Modules.Blazor.Templates.Templates.Client.RazorComponentCodeBehind
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class RazorComponentCodeBehindTemplateRegistration : FilePerModelTemplateRegistration<ComponentModel>
    {
        private readonly IMetadataManager _metadataManager;

        public RazorComponentCodeBehindTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }

        public override string TemplateId => RazorComponentCodeBehindTemplate.TemplateId;

        [IntentManaged(Mode.Fully)]
        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget, ComponentModel model)
        {
            return new RazorComponentCodeBehindTemplate(outputTarget, model);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override IEnumerable<ComponentModel> GetModels(IApplication application)
        {
            if (!application.Settings.GetBlazor().UseCodeBehindFilesForComponents())
            {
                return [];
            }

            return _metadataManager.UserInterface(application).GetComponentModels();
        }
    }
}
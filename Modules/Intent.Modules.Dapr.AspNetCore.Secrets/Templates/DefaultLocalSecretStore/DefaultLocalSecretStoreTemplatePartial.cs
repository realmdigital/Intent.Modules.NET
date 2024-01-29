using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.ProjectItemTemplate.Partial", Version = "1.0")]

namespace Intent.Modules.Dapr.AspNetCore.Secrets.Templates.DefaultLocalSecretStore
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class DefaultLocalSecretStoreTemplate : IntentTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.Dapr.AspNetCore.Secrets.DefaultLocalSecretStore";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public DefaultLocalSecretStoreTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            var config = new TemplateFileConfig(
                fileName: $"secret-store",
                fileExtension: "yaml",
                relativeLocation: "dapr/components/"
            );

            config.CustomMetadata.Add("RelativeOutputPathPrefix", "dapr/components");

            return config;
        }
    }
}
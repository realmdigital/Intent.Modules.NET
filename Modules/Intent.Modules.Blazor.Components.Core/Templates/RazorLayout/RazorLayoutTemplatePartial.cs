using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.UI.Api;
using Intent.Modules.Blazor.Components.Core.Templates.RazorComponent;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.Mapping;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.ProjectItemTemplate.Partial", Version = "1.0")]

namespace Intent.Modules.Blazor.Components.Core.Templates.RazorLayout
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class RazorLayoutTemplate : CSharpTemplateBase<LayoutModel>, IRazorComponentTemplate
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.Blazor.Components.Core.RazorLayoutTemplate";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public RazorLayoutTemplate(IOutputTarget outputTarget, LayoutModel model) : base(TemplateId, outputTarget, model)
        {
            BlazorFile = new BlazorFile(this);
            BindingManager = new BindingManager(this, Model.InternalElement.Mappings.FirstOrDefault());
        }

        public BlazorFile BlazorFile { get; set; }
        public IRazorComponentBuilderProvider ComponentBuilderResolver { get; }

        RazorFile IRazorComponentTemplate.BlazorFile => BlazorFile;

        public BindingManager BindingManager { get; }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        protected override CSharpFileConfig DefineFileConfig()
        {
            return new CSharpFileConfig(
                className: $"{Model.Name}",
                @namespace: this.GetNamespace(),
                relativeLocation: this.GetFolderPath(),
                fileExtension: "razor"
            );
        }

        public override string TransformText()
        {
            var razorFile = BlazorFile.Build();
            foreach (var @using in this.ResolveAllUsings(
                             "System",
                             "System.Collections.Generic"
                             ).Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                razorFile.AddUsing(@using);
            }

            return razorFile.ToString();
        }

        public override string RunTemplate()
        {
            return TransformText();
        }

        public void AddInjectDirective(string fullyQualifiedTypeName, string propertyName = null)
        {
            throw new NotImplementedException();
        }

        public CSharpClassMappingManager CreateMappingManager()
        {
            throw new NotImplementedException();
        }
    }
}
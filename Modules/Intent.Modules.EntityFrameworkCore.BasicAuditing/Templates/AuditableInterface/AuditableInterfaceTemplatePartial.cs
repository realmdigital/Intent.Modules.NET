using System;
using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.Builder;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.CSharp.VisualStudio;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.CSharp.Templates.CSharpTemplatePartial", Version = "1.0")]

namespace Intent.Modules.EntityFrameworkCore.BasicAuditing.Templates.AuditableInterface
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class AuditableInterfaceTemplate : CSharpTemplateBase<object>, ICSharpFileBuilderTemplate
    {
        public const string TemplateId = "Intent.EntityFrameworkCore.BasicAuditing.AuditableInterface";

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public AuditableInterfaceTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            CSharpFile = new CSharpFile(this.GetNamespace(), this.GetFolderPath())
                .AddUsing("System")
                .AddInterface($"IAuditable", @interface =>
                {
                    @interface.AddMethod("void", "SetCreated", method =>
                    {
                        method.AddParameter("string", "createdBy");
                        method.AddParameter("DateTimeOffset", "createdDate");
                    });

                    @interface.AddMethod("void", "SetUpdated", method =>
                    {
                        method.AddParameter("string", "updatedBy");
                        method.AddParameter("DateTimeOffset", "updatedDate");
                    });
                });
        }

        [IntentManaged(Mode.Fully)]
        public CSharpFile CSharpFile { get; }

        [IntentManaged(Mode.Fully)]
        protected override CSharpFileConfig DefineFileConfig()
        {
            return CSharpFile.GetConfig();
        }

        [IntentManaged(Mode.Fully)]
        public override string TransformText()
        {
            return CSharpFile.ToString();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Eventing.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.CSharp.VisualStudio;
using Intent.Modules.Common.Templates;
using Intent.Modules.Eventing.MassTransit.Templates.IntegrationEventMessage;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.CSharp.Templates.CSharpTemplatePartial", Version = "1.0")]

namespace Intent.Modules.Eventing.MassTransit.Templates.WrapperConsumer
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    partial class WrapperConsumerTemplate : CSharpTemplateBase<object, ConsumerDecorator>
    {
        public const string TemplateId = "Intent.Eventing.MassTransit.WrapperConsumer";

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public WrapperConsumerTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddNugetDependency(NuGetPackages.MassTransitAbstractions);
            AddTypeSource(IntegrationEventMessageTemplate.TemplateId);
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        protected override CSharpFileConfig DefineFileConfig()
        {
            return new CSharpFileConfig(
                className: $"WrapperConsumer",
                @namespace: $"{this.GetNamespace()}",
                relativeLocation: $"{this.GetFolderPath()}");
        }

        private bool UseExplicitNullSymbol => Project.GetProject().NullableEnabled;

        private string GetClassMembers()
        {
            var members = new List<string>();

            members.Add($@"private readonly IServiceProvider _serviceProvider;");

            members.AddRange(GetDecorators().SelectMany(s => s.RequiredServices()).Select(s => $@"private readonly {s.Type.ToPascalCase()} {s.FieldName.ToCamelCase()};"));

            if (!members.Any())
            {
                return string.Empty;
            }

            const string newLine = @"
        ";
            return newLine + string.Join(newLine, members) + newLine;
        }

        private string GetConstructorParameters()
        {
            var parameters = new List<string>();

            parameters.Add($@"IServiceProvider serviceProvider");

            parameters.AddRange(GetDecorators().SelectMany(s => s.RequiredServices()).Select(s => $@"{s.Type.ToPascalCase()} {s.Name.ToCamelCase()}"));

            return string.Join(", ", parameters);
        }

        private string GetConstructorImplementation()
        {
            var statements = new List<string>();

            statements.Add($@"_serviceProvider = serviceProvider;");

            statements.AddRange(GetDecorators().SelectMany(s => s.RequiredServices()).Select(s => $@"{s.FieldName.ToCamelCase()} = {s.Name.ToCamelCase()};"));

            if (!statements.Any())
            {
                return string.Empty;
            }

            const string newLine = @"
            ";
            return newLine + string.Join(newLine, statements);
        }

        private string GetConsumeEnterCode()
        {
            var lines = new List<string>();

            lines.AddRange(GetDecorators().SelectMany(x => x.GetConsumeEnterCode()));

            const string newLine = @"
        ";
            return newLine + string.Join(newLine, lines);
        }

        private string GetConsumeExitCode()
        {
            var lines = new List<string>();

            lines.AddRange(GetDecorators().SelectMany(x => x.GetConsumeExitCode()));

            const string newLine = @"
        ";
            return newLine + string.Join(newLine, lines);
        }
    }
}
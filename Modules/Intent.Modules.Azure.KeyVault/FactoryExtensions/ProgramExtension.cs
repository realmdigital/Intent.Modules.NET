using System.Linq;
using Intent.Engine;
using Intent.Modules.Azure.KeyVault.Templates.AzureKeyVaultConfiguration;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.AppStartup;
using Intent.Modules.Common.CSharp.Builder;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.CSharp.VisualStudio;
using Intent.Modules.Common.Plugins;
using Intent.Modules.Common.Templates;
using Intent.Modules.Constants;
using Intent.Plugins.FactoryExtensions;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.FactoryExtension", Version = "1.0")]

namespace Intent.Modules.Azure.KeyVault.FactoryExtensions;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class ProgramExtension : FactoryExtensionBase
{
    public override string Id => "Intent.Azure.KeyVault.ProgramExtension";

    [IntentManaged(Mode.Ignore)] public override int Order => 0;

    protected override void OnAfterTemplateRegistrations(IApplication application)
    {
        var programTemplate = application.FindTemplateInstance<IProgramTemplate>(TemplateDependency.OnTemplate("App.Program"));
        if (programTemplate == null)
        {
            return;
        }

        var configTemplate = application.FindTemplateInstance<ICSharpFileBuilderTemplate>(TemplateDependency.OnTemplate(AzureKeyVaultConfigurationTemplate.TemplateId));
        if (configTemplate == null)
        {
            return;
        }

        programTemplate.CSharpFile.OnBuild(file =>
        {
            file.AddUsing(configTemplate.Namespace);
            file.AddUsing("Microsoft.Extensions.Configuration");

            if (programTemplate.ProgramFile.UsesMinimalHostingModel)
            {
                programTemplate.ProgramFile.AddHostBuilderConfigurationStatement(new CSharpIfStatement("builder.Configuration.GetValue<bool?>(\"KeyVault:Enabled\") == true"), @if =>
                {
                    @if.AddStatement("builder.Configuration.ConfigureAzureKeyVault(builder.Configuration);");
                });
            }
            else
            {
                programTemplate.ProgramFile.ConfigureAppConfiguration(true, (statements, parameters) =>
                {
                    var builder = statements.FindStatement(s => s.HasMetadata("configuration-builder"));
                    if (builder is null)
                    {
                        builder = new CSharpStatement($"var configuration = {parameters[^1]}.Build();")
                            .AddMetadata("configuration-builder", true)
                            .SeparatedFromNext();
                        statements.InsertStatement(0, builder);
                    }
                    statements
                        .AddIfStatement(@"configuration.GetValue<bool?>(""KeyVault:Enabled"") == true", @if =>
                        {
                            @if.AddStatement($"{parameters[^1]}.ConfigureAzureKeyVault(configuration);");
                        });
                });
            }
        }, 30);
    }
}
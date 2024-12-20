using GraphQL.AzureFunction.TestApplication.Api.Configuration;
using GraphQL.AzureFunction.TestApplication.Application;
using GraphQL.AzureFunction.TestApplication.Infrastructure;
using Intent.RoslynWeaver.Attributes;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AzureFunctions.InProcess.Startup", Version = "1.0")]

[assembly: FunctionsStartup(typeof(GraphQL.AzureFunction.TestApplication.Api.Startup))]

namespace GraphQL.AzureFunction.TestApplication.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;
            builder.Services.AddApplication(configuration);
            builder.Services.ConfigureApplicationSecurity(configuration);
            builder.Services.AddInfrastructure(configuration);
            builder.Services.ConfigureGraphQL();
        }
    }
}
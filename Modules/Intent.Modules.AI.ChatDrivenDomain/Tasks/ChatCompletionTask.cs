using System;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Intent.Engine;
using Intent.Modules.AI.ChatDrivenDomain.Plugins;
using Intent.Modules.AI.ChatDrivenDomain.Settings;
using Intent.Modules.AI.ChatDrivenDomain.Utils;
using Intent.Plugins;
using Intent.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OllamaSharp;

namespace Intent.Modules.AI.ChatDrivenDomain.Tasks;

public class ChatCompletionTask : IModuleTask
{
    private readonly IApplicationConfigurationProvider _applicationConfigurationProvider;

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public ChatCompletionTask(IApplicationConfigurationProvider applicationConfigurationProvider)
    {
        _applicationConfigurationProvider = applicationConfigurationProvider;
    }

    public string TaskTypeId => "Intent.Modules.ChatDrivenDomain.Tasks.ChatCompletionTask";
    public string TaskTypeName => "Consulting the great LLAMA...";
    public int Order => 0;

    public string Execute(params string[] args)
    {
        Logging.Log.Info($"Args: {string.Join(",", args)}");

        try
        {
            var inputModel = JsonSerializer.Deserialize<InputModel>(args[0], SerializerOptions)!;
            var modelMutationPlugin = new ModelMutationPlugin(inputModel);
            var kernel = BuildSemanticKernel(modelMutationPlugin);

            var requestFunction = kernel.CreateFunctionFromPrompt(
                """
                # Domain Modeling Expert for Intent Architect
                
                You are a specialized domain modeling expert for Intent Architect. 
                Your task is analyzing, optimizing, and modifying domain models using Domain-Driven Design principles. 
                You MUST apply proper domain modeling constraints and best practices.
                
                ## Domain Model Structure
                
                The domain model consists of Classes, Attributes, and Associations:
                
                - Classes have a name, optional comment, attributes, and associations.
                - Attributes have a name, type, and can be nullable or collections. Attributes are storage fields of a Class.
                - Associations define relationships between classes.
                
                ## Primitive Types
                - string, int, long, decimal, datetime, bool, guid, object, float, double
                
                ## Relationship Rules - CRITICAL
                
                1. **Composite Relationships (1 -> 1)**: 
                   - An entity can have only ONE composite owner
                   - INVALID: If ClassA and ClassB both have a 1 -> 1 composite relationship to ClassC.
                   - CORRECT: If an entity has a 1 -> 1 relationship, it cannot be owned by multiple entities.
                
                2. **One-to-Many (1 -> *)**: 
                   - Source has one reference to Target.
                   - Target has a collection of Source entities.
                   - This is considered a Composite relationship by default.
                   
                3. **Many-to-Many (* -> *)**:
                   - Both sides have collections of each other.
                   - This is considered an Aggregate relationship by default.
                
                4. **Navigability**:
                   - "Source End" is where the relationship originates.
                   - "Target End" is where the relationship points to.
                   - Composite relationship will have the "Source End" on the Class being the "owner" and the "Target End" on the Class "being owned".
                   - Aggregate relationship will favor the "Source End" on the Class needing the association on the other class while still retaining a unidirectional relationship (unless explicitly asked to make bidirectional by the user). 
                
                ## Available Functions
                
                Instead of generating JSON directly, you must use the following functions to modify the domain model:
                
                1. `CreateClass(name, comment)` - Creates a new class and returns its ID
                2. `UpdateClass(classId, name, comment)` - Updates an existing class by ID and returns ID|name
                3. `RemoveClass(classId)` - Removes a class by ID and all its associations
                
                4. `CreateAttribute(classId, name, type, isNullable, isCollection, comment)` - Creates an attribute for a class and returns its ID
                5. `UpdateAttribute(classId, attributeId, name, type, isNullable, isCollection, comment)` - Updates an attribute by ID
                6. `RemoveAttribute(classId, attributeId)` - Removes an attribute by ID
                
                7. `CreateAssociation(sourceClassId, targetClassId, relationship)` - Creates an association between classes and returns its ID
                8. `RemoveAssociation(sourceClassId, targetClassId)` - Removes an association between classes
                
                9. `ListClasses()` - Lists all classes in the model
                10. `GetClassDetails(classNameOrId)` - Gets detailed information about a class (can use name or ID)
                11. `GetDomainModel()` - Gets the current domain model as JSON
                
                ## User Instructions
                
                ```
                {{$prompt}}
                ```
                
                ## Current Domain Model
                
                First, analyze the current domain model by calling ListClasses() and GetClassDetails() for each class of interest.
                Then, implement the user's instructions by calling the appropriate functions.
                
                DO NOT generate JSON directly. ONLY use the provided functions to modify the domain model.
                """, new OpenAIPromptExecutionSettings()
                {
                    MaxTokens = _applicationConfigurationProvider.GetSettings().GetChatDrivenDomainSettings().MaxTokens(),
                    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
                });

            var result = requestFunction.InvokeAsync(kernel, new KernelArguments
            {
                ["prompt"] = inputModel.Prompt
            }).Result;

            Logging.Log.Info($"LLM Interaction Complete");

            // Get the final domain model from the plugin
            var finalModel = modelMutationPlugin.GetClasses();
            var jsonResult = JsonSerializer.Serialize(finalModel, SerializerOptions);
            
            Logging.Log.Debug($"Result: \r\n{jsonResult}");
            
            return jsonResult;
        }
        catch (Exception e)
        {
            Logging.Log.Failure(e);
            return Fail(e.GetBaseException().Message);
        }
    }

    private Kernel BuildSemanticKernel(ModelMutationPlugin modelMutationPlugin)
    {
        var settings = _applicationConfigurationProvider.GetSettings().GetChatDrivenDomainSettings();
        var model = string.IsNullOrWhiteSpace(settings.Model()) ? "gpt-4o" : settings.Model();
        var apiKey = settings.APIKey();

        var builder = Kernel.CreateBuilder();
        builder.Services.AddLogging(b => b.AddProvider(new SoftwareFactoryLoggingProvider()).SetMinimumLevel(LogLevel.Trace));
        
        // Register the ModelMutationPlugin with the kernel
        builder.Plugins.AddFromObject(modelMutationPlugin);

        switch (settings.Provider().AsEnum())
        {
            case ChatDrivenDomainSettings.ProviderOptionsEnum.OpenAi:
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
                }

                builder.Services.AddOpenAIChatCompletion(
                    modelId: model,
                    apiKey: apiKey ?? throw new Exception("No API Key defined. Locate the ChatDrivenDomainSettings App Settings or set the OPENAI_API_KEY environment variable."));
                break;
            case ChatDrivenDomainSettings.ProviderOptionsEnum.AzureOpenAi:
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
                }

                builder.Services.AddAzureOpenAIChatCompletion(
                    deploymentName: settings.DeploymentName(),
                    endpoint: settings.APIUrl(),
                    apiKey: apiKey ?? throw new Exception("No API Key defined. Locate the ChatDrivenDomainSettings App Settings or set the AZURE_OPENAI_API_KEY environment variable."),
                    modelId: model);
                break;
            case ChatDrivenDomainSettings.ProviderOptionsEnum.Ollama:
#pragma warning disable SKEXP0070
                builder.Services.AddOllamaChatCompletion(
                    new OllamaApiClient(
                        new HttpClient
                        {
                            Timeout = TimeSpan.FromMinutes(10),
                            BaseAddress = new Uri(settings.APIUrl())
                        },
                        model)
                );
#pragma warning restore SKEXP0070
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        var kernel = builder.Build();
        return kernel;
    }

    private static string Fail(string reason)
    {
        Logging.Log.Failure(reason);
        var errorObject = new { errorMessage = reason };
        var json = JsonSerializer.Serialize(errorObject, SerializerOptions);
        return json;
    }
}

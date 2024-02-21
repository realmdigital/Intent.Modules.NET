using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.Builder;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.Templates;
using Intent.Modules.Constants;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.CSharp.Templates.CSharpTemplatePartial", Version = "1.0")]

namespace Intent.Modules.Eventing.MassTransit.Templates.MediatRConsumer
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class MediatRConsumerTemplate : CSharpTemplateBase<object>, ICSharpFileBuilderTemplate
    {
        public const string TemplateId = "Intent.Eventing.MassTransit.MediatRConsumer";

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public MediatRConsumerTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            ConsumerHelper.AddConsumerDependencies(this);
            CSharpFile = new CSharpFile(this.GetNamespace(), this.GetFolderPath());
            ConsumerHelper.AddConsumerClass(this,
                baseName: "MediatR",
                configureClass: (@class, tMessage) =>
                {
                    @class.AddMethod("object", "RespondWithPayload", method =>
                    {
                        method.Private().Static();
                        method.AddParameter("object", "response");

                        method.AddStatement(
                            $$"""
                              var responseType = response.GetType();
                              var genericType = typeof({{this.GetRequestCompletedMessageName()}}<>).MakeGenericType(responseType);
                              var responseInstance = Activator.CreateInstance(genericType, new[] { response });
                              return responseInstance!;
                              """);
                    });
                },
                configureConsumeMethod: (@class, method, tMessage) =>
                {
                    method.AddStatement($"var sender = _serviceProvider.GetRequiredService<{UseType("MediatR.ISender")}>();",
                        stmt => stmt.AddMetadata("handler", "instantiate").SeparatedFromPrevious());
                    method.AddStatement($"var response = await sender.Send(context.Message, context.CancellationToken);",
                        stmt => stmt.AddMetadata("handler", "execute"));

                    method.AddStatement(
                        $$"""
                          switch (response)
                          {
                              case null:
                                  break;
                              case int or float or double or decimal or long or bool or char or byte or sbyte or short or ushort or uint or ulong or string or System.Collections.IEnumerable:
                                  var res = RespondWithPayload(response);
                                  await context.RespondAsync(res);
                                  break;
                              case MediatR.Unit:
                                  await context.RespondAsync({{this.GetRequestCompletedMessageName()}}.Instance);
                                  break;
                              case not MediatR.Unit:
                                  await context.RespondAsync(response);
                                  break;
                          }
                          """, stmt => stmt.SeparatedFromPrevious());
                },
                applyStandardUnitOfWorkLogic: false);
            ConsumerHelper.AddConsumerDefinitionClass(this,
                baseName: "MediatR");
        }

        public override bool CanRunTemplate()
        {
            var services = ExecutionContext.MetadataManager.Services(ExecutionContext.GetApplicationConfig().Id);
            var relevantCommands = services.GetElementsOfType("Command")
                .Where(p => p.HasStereotype(Constants.MassTransitConsumerStereotype) && ExecutionContext.TemplateExists(TemplateRoles.Application.Handler.Command, p.Id));
            var relevantQueries = services.GetElementsOfType("Query")
                .Where(p => p.HasStereotype(Constants.MassTransitConsumerStereotype) && ExecutionContext.TemplateExists(TemplateRoles.Application.Handler.Query, p.Id));
            return relevantCommands.Concat(relevantQueries).Any();
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
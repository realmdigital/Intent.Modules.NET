using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.DocumentDB.Api;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.Builder;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.Templates;
using Intent.Modules.Constants;
using Intent.Modules.CosmosDB.Templates.CosmosDBDocumentInterface;
using Intent.Modules.Entities.Repositories.Api.Templates;
using Intent.Modules.Entities.Repositories.Api.Templates.EntityRepositoryInterface;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.CSharp.Templates.CSharpTemplatePartial", Version = "1.0")]

namespace Intent.Modules.CosmosDB.Templates.CosmosDBRepositoryInterface
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class CosmosDBRepositoryInterfaceTemplate : CSharpTemplateBase<IList<ClassModel>>, ICSharpFileBuilderTemplate
    {
        public const string TemplateId = "Intent.CosmosDB.CosmosDBRepositoryInterface";

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public CosmosDBRepositoryInterfaceTemplate(IOutputTarget outputTarget, IList<ClassModel> model) : base(TemplateId, outputTarget, model)
        {
            CSharpFile = new CSharpFile(this.GetNamespace(), this.GetFolderPath())
                .AddUsing("System")
                .AddUsing("System.Collections.Generic")
                .AddUsing("System.Linq.Expressions")
                .AddUsing("System.Threading")
                .AddUsing("System.Threading.Tasks")
                .AddInterface($"ICosmosDBRepository", @interface =>
                {
                    @interface
                        .AddGenericParameter("TDomain", out var tDomain)
                        .AddGenericParameter("TDocumentInterface", out var toDocumentInterface);

                    @interface
                        .ImplementsInterfaces($"{this.GetRepositoryInterfaceName()}<{tDomain}>")
                        .AddProperty(this.GetCosmosDBUnitOfWorkInterfaceName(), "UnitOfWork", property => property
                            .WithoutSetter()
                        )
                        .AddMethod($"Task<List<{tDomain}>>", "FindAllAsync", method => method
                            .AddParameter("CancellationToken", "cancellationToken",
                                parameter => parameter.WithDefaultValue("default"))
                        )
                        .AddMethod($"Task<{tDomain}?>", "FindByIdAsync", method => method
                            .AddParameter("string", "id")
                            .AddParameter("CancellationToken", "cancellationToken",
                                parameter => parameter.WithDefaultValue("default"))
                        )
                        .AddMethod($"Task<List<{tDomain}>>", "FindAllAsync", method => method
                            .AddParameter($"Expression<Func<{toDocumentInterface}, bool>>", "filterExpression")
                            .AddParameter("CancellationToken", "cancellationToken", x => x.WithDefaultValue("default"))
                        )
                        .AddMethod($"Task<IPagedResult<{tDomain}>>", "FindAllAsync", method => method
                            .AddParameter($"int", "pageNo")
                            .AddParameter($"int", "pageSize")
                            .AddParameter("CancellationToken", "cancellationToken",
                                parameter => parameter.WithDefaultValue("default"))
                        )
                        .AddMethod($"Task<IPagedResult<{tDomain}>>", "FindAllAsync", method => method
                            .AddParameter($"Expression<Func<{toDocumentInterface}, bool>>", "filterExpression")
                            .AddParameter($"int", "pageNo")
                            .AddParameter($"int", "pageSize")
                            .AddParameter("CancellationToken", "cancellationToken",
                                parameter => parameter.WithDefaultValue("default"))
                        )
                        .AddMethod($"Task<List<{tDomain}>>", "FindByIdsAsync", method => method
                            .AddParameter($"IEnumerable<string>", "ids")
                            .AddParameter("CancellationToken", "cancellationToken",
                                parameter => parameter.WithDefaultValue("default"))
                        );
                });
        }

        public override void AfterTemplateRegistration()
        {
            base.AfterTemplateRegistration();

            foreach (var model in Model)
            {
                var template = GetTemplate<ICSharpFileBuilderTemplate>(EntityRepositoryInterfaceTemplate.TemplateId, model.Id);
                template.CSharpFile.AfterBuild(file =>
                {
                    var @interface = file.Interfaces.Single();
                    @interface.Interfaces.Clear();
                    var genericTypeParameters = model.GenericTypes.Any()
                        ? $"<{string.Join(", ", model.GenericTypes)}>"
                        : string.Empty;
                    var tDomainGenericArgument = template.GetTypeName(TemplateRoles.Domain.Entity.Interface, model);
                    var tDocumentInterfaceGenericArgument = template.GetTypeName(CosmosDBDocumentInterfaceTemplate.TemplateId, model);
                    @interface.ImplementsInterfaces($"{this.GetCosmosDBRepositoryInterfaceName()}<{tDomainGenericArgument}{genericTypeParameters}, {tDocumentInterfaceGenericArgument}{genericTypeParameters}>");

                    if (model.GetPrimaryKeyAttribute()?.TypeReference?.Element.Name == "string")
                    {
                        var toRemove = @interface.Methods
                            .Where(x => x.Name is "FindByIdAsync" or "FindByIdsAsync")
                            .ToArray();

                        foreach (var method in toRemove)
                        {
                            @interface.Methods.Remove(method);
                        }
                    }
                }, 1000);
            }
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
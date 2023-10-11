using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Modelers.Services.CQRS.Api;
using Intent.Modules.Application.MediatR.CRUD.CrudStrategies;
using Intent.Modules.Application.MediatR.CRUD.Tests.Templates.Assertions.AssertionClass;
using Intent.Modules.Application.MediatR.Templates;
using Intent.Modules.Application.MediatR.Templates.QueryHandler;
using Intent.Modules.Application.MediatR.Templates.QueryModels;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.Builder;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.Templates;
using Intent.Modules.Constants;
using Intent.Modules.Entities.Repositories.Api.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.CSharp.Templates.CSharpTemplatePartial", Version = "1.0")]

namespace Intent.Modules.Application.MediatR.CRUD.Tests.Templates.Owner.GetByIdQueryHandlerTests;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public partial class GetByIdQueryHandlerTestsTemplate : CSharpTemplateBase<QueryModel>, ICSharpFileBuilderTemplate
{
    public const string TemplateId = "Intent.Application.MediatR.CRUD.Tests.Owner.GetByIdQueryHandlerTests";

    [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
    public GetByIdQueryHandlerTestsTemplate(IOutputTarget outputTarget, QueryModel model) : base(TemplateId, outputTarget, model)
    {
        AddNugetDependency(NugetPackages.AutoFixture);
        AddNugetDependency(NugetPackages.FluentAssertions);
        AddNugetDependency(NugetPackages.MicrosoftNetTestSdk);
        AddNugetDependency(NugetPackages.NSubstitute);
        AddNugetDependency(NugetPackages.Xunit);
        AddNugetDependency(NugetPackages.XunitRunnerVisualstudio);

        AddTypeSource(TemplateFulfillingRoles.Domain.Entity.Primary);
        AddTypeSource(QueryModelsTemplate.TemplateId);
        AddTypeSource(TemplateFulfillingRoles.Application.Contracts.Dto);

        CSharpFile = new CSharpFile(this.GetNamespace(), this.GetFolderPath())
            .AddClass($"{Model.Name}HandlerTests")
            .AfterBuild(file =>
            {
                var facade = new QueryHandlerFacade(this, model);

                AddUsingDirectives(file);
                facade.AddHandlerConstructorMockUsings();

                var priClass = file.Classes.First();
                priClass.AddConstructor(ctor =>
                {
                    ctor.AddStatements(facade.GetAutoMapperProfilesAndAddBackendField(priClass));
                });

                priClass.AddMethod("IEnumerable<object[]>", "GetSuccessfulResultTestData", method =>
                {
                    method.Static();
                    method.AddStatements(facade.Get_InitialAutoFixture_TestDataStatements(
                        includeVarKeyword: true));
                    method.AddStatements(facade.Get_SingleDomainEntity_TestDataStatements(
                        queryTargetDomain: QueryTargetDomain.Aggregate,
                        dataReturn: QueryTestDataReturn.QueryAndAggregateWithNestedEntityDomain,
                        includeVarKeyword: true,
                        nestedEntitiesAreEmpty: false));
                });

                priClass.AddMethod("Task", $"Handle_WithValidQuery_Retrieves{facade.SingularTargetDomainName}", method =>
                {
                    method.Async();
                    method.AddAttribute("Theory");
                    method.AddAttribute("MemberData(nameof(GetSuccessfulResultTestData))");
                    method.AddParameter(facade.QueryTypeName, "testQuery");
                    method.AddParameter(facade.TargetDomainTypeName, "existingEntity");

                    method.AddStatement("// Arrange");
                    method.AddStatements(facade.GetQueryHandlerConstructorParameterMockStatements());
                    method.AddStatements(facade.GetDomainAggregateRepositoryFindByIdMockingStatements("testQuery", "existingEntity", QueryHandlerFacade.MockRepositoryResponse.ReturnDomainVariable));
                    method.AddStatement(string.Empty);
                    method.AddStatements(facade.GetQueryHandlerConstructorSutStatement());

                    method.AddStatement(string.Empty);
                    method.AddStatement("// Act");
                    method.AddStatements(facade.GetSutHandleInvocationStatement("testQuery"));

                    method.AddStatement(string.Empty);
                    method.AddStatement("// Assert");
                    method.AddStatements(facade.Get_Aggregate_AssertionComparingHandlerResultsWithExpectedResults("existingEntity"));
                });

                priClass.AddMethod("Task", "Handle_WithInvalidIdQuery_ThrowsNotFoundException", method =>
                {
                    method.Async();
                    method.AddAttribute("Fact");

                    method.AddStatements("// Arrange");
                    method.AddStatements(facade.GetNewQueryAutoFixtureInlineStatements("query"));
                    method.AddStatements(facade.GetQueryHandlerConstructorParameterMockStatements());
                    method.AddStatements(facade.GetDomainAggregateRepositoryFindByIdMockingStatements("query", "", QueryHandlerFacade.MockRepositoryResponse.ReturnDefault));
                    method.AddStatements(facade.GetQueryHandlerConstructorSutStatement());

                    method.AddStatement(string.Empty);
                    method.AddStatements("// Act");
                    method.AddStatements(facade.GetSutHandleInvocationActLambdaStatement("query"));

                    method.AddStatement(string.Empty);
                    method.AddStatement("// Assert");
                    method.AddStatements(facade.GetThrowsExceptionAssertionStatement(this.GetNotFoundExceptionName()));
                });

                AddAssertionMethods();
            });
    }

    private bool? _canRunTemplate;

    public override bool CanRunTemplate()
    {
        if (_canRunTemplate.HasValue)
        {
            return _canRunTemplate.Value;
        }

        var template = this.GetQueryHandlerTemplate(Model, trackDependency: false);
        if (template is null)
        {
            _canRunTemplate = false;
        }
        else if (StrategyFactory.GetMatchedQueryStrategy(template, Project.Application) is GetByIdImplementationStrategy strategy && strategy.IsMatch())
        {
            _canRunTemplate = Model.GetClassModel()?.IsAggregateRoot() == true && Model.GetClassModel().InternalElement.Package.HasStereotype("Relational Database");
        }
        else
        {
            _canRunTemplate = false;
        }

        return _canRunTemplate.Value;
    }

    private static void AddUsingDirectives(CSharpFile file)
    {
        file.AddUsing("System");
        file.AddUsing("System.Collections.Generic");
        file.AddUsing("System.Linq");
        file.AddUsing("System.Threading");
        file.AddUsing("System.Threading.Tasks");
        file.AddUsing("AutoFixture");
        file.AddUsing("FluentAssertions");
        file.AddUsing("NSubstitute");
        file.AddUsing("Xunit");
        file.AddUsing("AutoMapper");
    }

    private void AddAssertionMethods()
    {
        if (Model?.Mapping?.Element?.IsClassModel() != true)
        {
            return;
        }

        var dtoModel = Model.TypeReference.Element.AsDTOModel();
        var domainModel = Model.Mapping.Element.AsClassModel();
        var template = ExecutionContext.FindTemplateInstance<ICSharpFileBuilderTemplate>(
            TemplateDependency.OnModel(AssertionClassTemplate.TemplateId, domainModel));
        if (template == null)
        {
            return;
        }

        template.AddAssertionMethods(template.CSharpFile.Classes.First(), domainModel, dtoModel, false);
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
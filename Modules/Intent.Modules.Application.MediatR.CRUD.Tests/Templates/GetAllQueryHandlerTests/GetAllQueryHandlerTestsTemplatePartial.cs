using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Modelers.Services.CQRS.Api;
using Intent.Modules.Application.MediatR.Templates;
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

namespace Intent.Modules.Application.MediatR.CRUD.Tests.Templates.GetAllQueryHandlerTests;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public partial class GetAllQueryHandlerTestsTemplate : CSharpTemplateBase<QueryModel>, ICSharpFileBuilderTemplate
{
    public const string TemplateId = "Intent.Application.MediatR.CRUD.Tests.GetAllQueryHandlerTests";

    [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
    public GetAllQueryHandlerTestsTemplate(IOutputTarget outputTarget, QueryModel model) : base(TemplateId, outputTarget, model)
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
        AddTypeSource("Intent.DomainEvents.DomainEventBase");
        
        CSharpFile = new CSharpFile(this.GetNamespace(), this.GetFolderPath())
            .AddClass($"{Model.Name}HandlerTests")
            .OnBuild(file =>
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

                var dtoModel = Model.TypeReference.Element.AsDTOModel();
                var domainElement = dtoModel.Mapping.Element.AsClassModel();
                var domainElementName = domainElement.Name.ToPascalCase();
                var domainElementPluralName = domainElementName.Pluralize();
                
                var priClass = file.Classes.First();
                priClass.AddField("IMapper", "_mapper", prop => prop.PrivateReadOnly());
                priClass.AddConstructor(ctor =>
                {
                    ctor.AddStatement(new CSharpInvocationStatement("var mapperConfiguration = new MapperConfiguration")
                        .AddArgument(new CSharpLambdaBlock("config")
                            .AddStatement($"config.AddMaps(typeof({this.GetQueryHandlerName(Model)}));"))
                        .WithArgumentsOnNewLines());
                    ctor.AddStatement("_mapper = mapperConfiguration.CreateMapper();");
                });

                priClass.AddMethod("Task", $"Handle_WithValidQuery_Retrieves{domainElementPluralName}", method =>
                {
                    method.Async();
                    method.AddAttribute("Theory");
                    method.AddAttribute("MemberData(nameof(GetTestData))");
                    method.AddParameter($"List<{GetTypeName(domainElement.InternalElement)}>", "testEntities");
                    method.AddStatements($@"
        // Arrange
        var expectedDtos = testEntities.Select(CreateExpected{dtoModel.Name.ToPascalCase()}).ToArray();
        
        var query = new Get{domainElementPluralName}Query();
        var repository = Substitute.For<{this.GetEntityRepositoryInterfaceName(domainElement)}>();
        repository.FindAllAsync(CancellationToken.None).Returns(Task.FromResult(testEntities));

        var sut = new {this.GetQueryHandlerName(Model)}(repository, _mapper);

        // Act
        var result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedDtos);");
                });
                
                priClass.AddMethod("IEnumerable<object[]>", "GetTestData", method =>
                {
                    method.Static();
                    method.AddStatements($@"var fixture = new Fixture();");
                    
                    if (TryGetTypeName("Intent.DomainEvents.DomainEventBase", out var domainEventBaseName))
                    {
                        method.AddStatements($@"
        fixture.Register<{domainEventBaseName}>(() => null);
        fixture.Customize<{GetTypeName(domainElement.InternalElement)}>(comp => comp.Without(x => x.DomainEvents));");
                    }

                    method.AddStatement($@"yield return new object[] {{ fixture.CreateMany<{GetTypeName(domainElement.InternalElement)}>().ToList() }};");

                    method.AddStatement($@"yield return new object[] {{ fixture.CreateMany<{GetTypeName(domainElement.InternalElement)}>(0).ToList() }};");
                });
                
                this.AddDomainToDtoMappingMethods(priClass, domainElement, dtoModel);
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
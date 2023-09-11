using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Modelers.Services.CQRS.Api;
using Intent.Modules.Application.MediatR.CRUD.CrudStrategies;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.FilePerModel", Version = "1.0")]

namespace Intent.Modules.Application.MediatR.CRUD.Tests.Templates.Owner.GetByIdQueryHandlerTests
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class GetByIdQueryHandlerTestsTemplateRegistration : FilePerModelTemplateRegistration<QueryModel>
    {
        private readonly IMetadataManager _metadataManager;

        public GetByIdQueryHandlerTestsTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }

        public override string TemplateId => GetByIdQueryHandlerTestsTemplate.TemplateId;

        [IntentManaged(Mode.Fully)]
        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget, QueryModel model)
        {
            return new GetByIdQueryHandlerTestsTemplate(outputTarget, model);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override IEnumerable<QueryModel> GetModels(IApplication application)
        {
            return _metadataManager.Services(application)
                .GetQueryModels()
                .Where(query => query.Name.StartsWith("get", StringComparison.InvariantCultureIgnoreCase) &&
                                query.Name.Contains("ById", StringComparison.InvariantCultureIgnoreCase) &&
                                query.TypeReference?.Element?.AsDTOModel()?.Mapping?.Element?.Id == query.Mapping?.Element?.Id &&
                                query.Mapping?.Element.AsClassModel().IsAggregateRoot() == true &&
                                query.HasIdentityKeys())
                .ToList();
        }
    }
}
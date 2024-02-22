using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.SingleFileListModel", Version = "1.0")]

namespace Intent.Modules.Eventing.MassTransit.Templates.RequestResponse.ResponseMappingFactory
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class ResponseMappingFactoryTemplateRegistration : SingleFileListModelTemplateRegistration<DTOModel>
    {
        private readonly IMetadataManager _metadataManager;

        public ResponseMappingFactoryTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }
        public override string TemplateId => ResponseMappingFactoryTemplate.TemplateId;

        [IntentManaged(Mode.Fully)]
        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget, IList<DTOModel> model)
        {
            return new ResponseMappingFactoryTemplate(outputTarget, model);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override IList<DTOModel> GetModels(IApplication application)
        {
            return _metadataManager.Services(application)
                .Elements
                .Where(element => HybridDtoModel.IsHybridDtoModel(element) &&
                                  element.HasStereotype(Constants.MessageRequestEndpointStereotype) &&
                                  element.TypeReference?.Element is not null &&
                                  element.TypeReference.Element.SpecializationType == DTOModel.SpecializationType)
                .DistinctBy(k => k.TypeReference.Element.Id)
                .Select(element => new DTOModel((IElement)element.TypeReference.Element))
                .ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Intent.DomainServices.Api;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Domain.Services.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.FilePerModel", Version = "1.0")]

namespace Intent.Modules.DomainServices.Templates.DomainServiceImplementation
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class DomainServiceImplementationTemplateRegistration : FilePerModelTemplateRegistration<DomainServiceModel>
    {
        private readonly IMetadataManager _metadataManager;

        public DomainServiceImplementationTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }

        public override string TemplateId => DomainServiceImplementationTemplate.TemplateId;

        [IntentManaged(Mode.Fully)]
        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget, DomainServiceModel model)
        {
            return new DomainServiceImplementationTemplate(outputTarget, model);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override IEnumerable<DomainServiceModel> GetModels(IApplication application)
        {
            return _metadataManager.Domain(application)
                .GetDomainServiceModels()
                .Where(p => !p.HasContractOnly())
                .ToArray();
        }
    }
}
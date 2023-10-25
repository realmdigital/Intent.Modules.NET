using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.FilePerModel", Version = "1.0")]

namespace Intent.Modules.CosmosDB.Templates.CosmosDBValueObjectDocumentInterface
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class CosmosDBValueObjectDocumentInterfaceTemplateRegistration : FilePerModelTemplateRegistration<IElement>
    {
        private readonly IMetadataManager _metadataManager;

        public CosmosDBValueObjectDocumentInterfaceTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }

        public override string TemplateId => CosmosDBValueObjectDocumentInterfaceTemplate.TemplateId;

        [IntentManaged(Mode.Fully)]
        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget, IElement model)
        {
            return new CosmosDBValueObjectDocumentInterfaceTemplate(outputTarget, model);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override IEnumerable<IElement> GetModels(IApplication application)
        {
            // To avoid requiring the installation of the "Intent.Modelers.Domain.ValueObjects" module,
            // we find value objects by their element type id.
            return _metadataManager.Domain(application).GetValueObjects();
        }
    }
}
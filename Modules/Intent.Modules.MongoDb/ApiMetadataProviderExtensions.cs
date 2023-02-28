using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.Api.ApiMetadataProviderExtensions", Version = "1.0")]

namespace Intent.MongoDb.Api
{
    public static class ApiMetadataProviderExtensions
    {
        public static IList<MongoDBDiagramModel> GetMongoDBDiagramModels(this IDesigner designer)
        {
            return designer.GetElementsOfType(MongoDBDiagramModel.SpecializationTypeId)
                .Select(x => new MongoDBDiagramModel(x))
                .ToList();
        }

    }
}
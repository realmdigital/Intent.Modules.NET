using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Types.Api;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.Api.ApiPackageModel", Version = "1.0")]

namespace Intent.MongoDb.Api
{
    [IntentManaged(Mode.Fully)]
    public class MongoDomainPackageModel : IHasStereotypes, IMetadataModel
    {
        public const string SpecializationType = "Mongo Domain Package";
        public const string SpecializationTypeId = "9d80acf2-6f29-4688-955b-6c17f94042ff";

        [IntentManaged(Mode.Ignore)]
        public MongoDomainPackageModel(IPackage package)
        {
            if (!SpecializationType.Equals(package.SpecializationType, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception($"Cannot create a '{GetType().Name}' from package with specialization type '{package.SpecializationType}'. Must be of type '{SpecializationType}'");
            }

            UnderlyingPackage = package;
        }

        public IPackage UnderlyingPackage { get; }
        public string Id => UnderlyingPackage.Id;
        public string Name => UnderlyingPackage.Name;
        public IEnumerable<IStereotype> Stereotypes => UnderlyingPackage.Stereotypes;
        public string FileLocation => UnderlyingPackage.FileLocation;

        public IList<TypeDefinitionModel> Types => UnderlyingPackage.ChildElements
            .GetElementsOfType(TypeDefinitionModel.SpecializationTypeId)
            .Select(x => new TypeDefinitionModel(x))
            .ToList();

        public IList<EnumModel> Enums => UnderlyingPackage.ChildElements
            .GetElementsOfType(EnumModel.SpecializationTypeId)
            .Select(x => new EnumModel(x))
            .ToList();

        public IList<CommentModel> Comments => UnderlyingPackage.ChildElements
            .GetElementsOfType(CommentModel.SpecializationTypeId)
            .Select(x => new CommentModel(x))
            .ToList();

        public IList<MongoDBDiagramModel> Diagrams => UnderlyingPackage.ChildElements
            .GetElementsOfType(MongoDBDiagramModel.SpecializationTypeId)
            .Select(x => new MongoDBDiagramModel(x))
            .ToList();

        public IList<FolderModel> Folders => UnderlyingPackage.ChildElements
            .GetElementsOfType(FolderModel.SpecializationTypeId)
            .Select(x => new FolderModel(x))
            .ToList();

        public IList<ClassModel> Classes => UnderlyingPackage.ChildElements
            .GetElementsOfType(ClassModel.SpecializationTypeId)
            .Select(x => new ClassModel(x))
            .ToList();

    }
}
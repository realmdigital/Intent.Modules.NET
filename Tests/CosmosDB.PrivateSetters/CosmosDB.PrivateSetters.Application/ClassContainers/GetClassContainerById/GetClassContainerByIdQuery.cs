using CosmosDB.PrivateSetters.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace CosmosDB.PrivateSetters.Application.ClassContainers.GetClassContainerById
{
    public class GetClassContainerByIdQuery : IRequest<ClassContainerDto>, IQuery
    {
        public GetClassContainerByIdQuery(string id, string classPartitionKey)
        {
            Id = id;
            ClassPartitionKey = classPartitionKey;
        }

        public string Id { get; set; }
        public string ClassPartitionKey { get; set; }
    }
}
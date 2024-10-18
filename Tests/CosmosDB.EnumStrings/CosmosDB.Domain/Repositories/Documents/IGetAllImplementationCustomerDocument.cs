using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.CosmosDB.CosmosDBDocumentInterface", Version = "1.0")]

namespace CosmosDB.Domain.Repositories.Documents
{
    public interface IGetAllImplementationCustomerDocument
    {
        string Id { get; }
        string Name { get; }
        IGetAllImplementationOrderDocument GetAllImplementationOrder { get; }
    }
}
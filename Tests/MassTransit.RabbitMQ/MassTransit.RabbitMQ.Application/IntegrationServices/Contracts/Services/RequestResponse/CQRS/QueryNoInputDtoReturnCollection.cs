using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.ClientContracts.DtoContract", Version = "2.0")]

namespace MassTransit.RabbitMQ.Application.IntegrationServices.Contracts.Services.RequestResponse.CQRS
{
    public class QueryNoInputDtoReturnCollection
    {
        public static QueryNoInputDtoReturnCollection Create()
        {
            return new QueryNoInputDtoReturnCollection
            {
            };
        }
    }
}
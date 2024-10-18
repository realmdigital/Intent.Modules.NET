using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.RequestResponse.ClientContracts.DtoContract", Version = "2.0")]

namespace MassTransit.RabbitMQ.Application.IntegrationServices.Contracts.Services.RequestResponse.CQRS
{
    public class QueryResponseDto
    {
        public QueryResponseDto()
        {
            Result = null!;
        }

        public string Result { get; set; }

        public static QueryResponseDto Create(string result)
        {
            return new QueryResponseDto
            {
                Result = result
            };
        }
    }
}
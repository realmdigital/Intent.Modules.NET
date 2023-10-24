using CosmosDB.PrivateSetters.Application.Common.Interfaces;
using CosmosDB.PrivateSetters.Domain;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace CosmosDB.PrivateSetters.Application.Clients.CreateClient
{
    public class CreateClientCommand : IRequest<string>, ICommand
    {
        public CreateClientCommand(ClientType type, string name)
        {
            Type = type;
            Name = name;
        }

        public ClientType Type { get; set; }
        public string Name { get; set; }
    }
}
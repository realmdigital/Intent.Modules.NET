using Intent.RoslynWeaver.Attributes;
using MassTransit.RabbitMQ.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace MassTransit.RabbitMQ.Application.People.CreatePerson
{
    public class CreatePersonCommand : IRequest, ICommand
    {
        public CreatePersonCommand(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
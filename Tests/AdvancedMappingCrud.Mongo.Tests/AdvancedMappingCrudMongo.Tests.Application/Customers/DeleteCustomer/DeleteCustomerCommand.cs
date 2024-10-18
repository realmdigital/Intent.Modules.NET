using AdvancedMappingCrudMongo.Tests.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AdvancedMappingCrudMongo.Tests.Application.Customers.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest, ICommand
    {
        public DeleteCustomerCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
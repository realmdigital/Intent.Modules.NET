using CosmosDBMultiTenancy.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace CosmosDBMultiTenancy.Application.Invoices.DeleteInvoice
{
    public class DeleteInvoiceCommand : IRequest, ICommand
    {
        public DeleteInvoiceCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
using System;
using AutoMapper;
using DtoSettings.Record.Public.Application.Common.Mappings;
using DtoSettings.Record.Public.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace DtoSettings.Record.Public.Application.Invoices
{
    public record InvoiceLineDto : IMapFrom<InvoiceLine>
    {
        public InvoiceLineDto(string description, decimal amount, string currency, Guid invoiceId, Guid id)
        {
            Description = description;
            Amount = amount;
            Currency = currency;
            InvoiceId = invoiceId;
            Id = id;
        }

        protected InvoiceLineDto()
        {
            Description = null!;
            Currency = null!;
        }

        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        public Guid InvoiceId { get; private set; }
        public Guid Id { get; private set; }

        public static InvoiceLineDto Create(string description, decimal amount, string currency, Guid invoiceId, Guid id)
        {
            return new InvoiceLineDto(description, amount, currency, invoiceId, id);
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InvoiceLine, InvoiceLineDto>();
        }
    }
}
using System;
using AutoMapper;
using DtoSettings.Record.Public.Application.Common.Mappings;
using DtoSettings.Record.Public.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace DtoSettings.Record.Public.Application.InvoicesAdvanced
{
    public record InvoiceDto : IMapFrom<Invoice>
    {
        public InvoiceDto(Guid id, string number)
        {
            Id = id;
            Number = number;
        }

        protected InvoiceDto()
        {
            Number = null!;
        }

        public Guid Id { get; protected set; }
        public string Number { get; protected set; }

        public static InvoiceDto Create(Guid id, string number)
        {
            return new InvoiceDto(id, number);
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Invoice, InvoiceDto>();
        }
    }
}
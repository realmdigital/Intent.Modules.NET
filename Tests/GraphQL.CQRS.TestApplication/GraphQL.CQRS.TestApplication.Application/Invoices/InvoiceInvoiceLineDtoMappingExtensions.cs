using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;
using GraphQL.CQRS.TestApplication.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace GraphQL.CQRS.TestApplication.Application.Invoices
{
    public static class InvoiceInvoiceLineDtoMappingExtensions
    {
        public static InvoiceInvoiceLineDto MapToInvoiceInvoiceLineDto(this InvoiceLine projectFrom, IMapper mapper)
        {
            return mapper.Map<InvoiceInvoiceLineDto>(projectFrom);
        }

        public static List<InvoiceInvoiceLineDto> MapToInvoiceInvoiceLineDtoList(this IEnumerable<InvoiceLine> projectFrom, IMapper mapper)
        {
            return projectFrom.Select(x => x.MapToInvoiceInvoiceLineDto(mapper)).ToList();
        }
    }
}
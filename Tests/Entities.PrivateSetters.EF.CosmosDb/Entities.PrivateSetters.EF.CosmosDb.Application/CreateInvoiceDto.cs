using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Entities.PrivateSetters.EF.CosmosDb.Application
{
    public class CreateInvoiceDto
    {
        public CreateInvoiceDto()
        {
        }

        public DateTime Date { get; set; }
        public IEnumerable<Guid> TagIds { get; set; }
        public List<CreateInvoiceLineDto> Lines { get; set; }

        public static CreateInvoiceDto Create(DateTime date, IEnumerable<Guid> tagIds, List<CreateInvoiceLineDto> lines)
        {
            return new CreateInvoiceDto
            {
                Date = date,
                TagIds = tagIds,
                Lines = lines
            };
        }
    }
}
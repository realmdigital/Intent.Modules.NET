using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTagModeImplicit]

namespace DtoSettings.Class.Init.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
    }
}
using System;
using System.Collections.Generic;
using AdvancedMappingCrud.DbContext.ProjectTo.Tests.Domain.Common;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace AdvancedMappingCrud.DbContext.ProjectTo.Tests.Domain.Entities
{
    public class Order : IHasDomainEvent
    {
        public Order()
        {
            RefNo = null!;
            Customer = null!;
        }
        public Guid Id { get; set; }

        public string RefNo { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public Guid CustomerId { get; set; }

        public EntityStatus EntityStatus { get; set; } = EntityStatus.Active;

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = [];

        public List<DomainEvent> DomainEvents { get; set; } = [];
    }
}
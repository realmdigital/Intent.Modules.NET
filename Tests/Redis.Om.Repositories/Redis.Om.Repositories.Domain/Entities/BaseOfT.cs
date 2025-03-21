using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;
using Redis.Om.Repositories.Domain.Common;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace Redis.Om.Repositories.Domain.Entities
{
    public abstract class BaseOfT<T> : IHasDomainEvent
    {
        private string? _id;
        public BaseOfT()
        {
            Id = null!;
        }
        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public T GenericAttribute { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = [];
    }
}
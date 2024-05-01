using System;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using OutputCachingRedis.Tests.Application.Common.Interfaces;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace OutputCachingRedis.Tests.Application.Orders.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderDto>, IQuery
    {
        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
using System;
using AspNetCore.Controllers.Secured.Application.Common.Interfaces;
using AspNetCore.Controllers.Secured.Application.Common.Security;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace AspNetCore.Controllers.Secured.Application.Buyers.GetBuyerById
{
    [Authorize(Roles = "Role2", Policy = "Policy2")]
    public class GetBuyerByIdQuery : IRequest<BuyerDto>, IQuery
    {
        public GetBuyerByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
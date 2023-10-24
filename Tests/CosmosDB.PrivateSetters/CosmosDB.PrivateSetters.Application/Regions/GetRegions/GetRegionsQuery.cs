using System.Collections.Generic;
using CosmosDB.PrivateSetters.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace CosmosDB.PrivateSetters.Application.Regions.GetRegions
{
    public class GetRegionsQuery : IRequest<List<RegionDto>>, IQuery
    {
        public GetRegionsQuery()
        {
        }
    }
}
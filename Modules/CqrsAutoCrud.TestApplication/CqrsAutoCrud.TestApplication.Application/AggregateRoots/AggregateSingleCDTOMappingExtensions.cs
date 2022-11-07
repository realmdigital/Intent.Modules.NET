using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;
using CqrsAutoCrud.TestApplication.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace CqrsAutoCrud.TestApplication.Application.AggregateRoots
{
    public static class AggregateSingleCDTOMappingExtensions
    {
        public static AggregateSingleCDTO MapToAggregateSingleCDTO(this IAggregateSingleC projectFrom, IMapper mapper)
        {
            return mapper.Map<AggregateSingleCDTO>(projectFrom);
        }

        public static List<AggregateSingleCDTO> MapToAggregateSingleCDTOList(this IEnumerable<IAggregateSingleC> projectFrom, IMapper mapper)
        {
            return projectFrom.Select(x => x.MapToAggregateSingleCDTO(mapper)).ToList();
        }
    }
}
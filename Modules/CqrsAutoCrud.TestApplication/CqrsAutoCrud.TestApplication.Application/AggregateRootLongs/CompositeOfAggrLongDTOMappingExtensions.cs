using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;
using CqrsAutoCrud.TestApplication.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace CqrsAutoCrud.TestApplication.Application.AggregateRootLongs
{
    public static class CompositeOfAggrLongDTOMappingExtensions
    {
        public static CompositeOfAggrLongDTO MapToCompositeOfAggrLongDTO(this ICompositeOfAggrLong projectFrom, IMapper mapper)
        {
            return mapper.Map<CompositeOfAggrLongDTO>(projectFrom);
        }

        public static List<CompositeOfAggrLongDTO> MapToCompositeOfAggrLongDTOList(this IEnumerable<ICompositeOfAggrLong> projectFrom, IMapper mapper)
        {
            return projectFrom.Select(x => x.MapToCompositeOfAggrLongDTO(mapper)).ToList();
        }
    }
}
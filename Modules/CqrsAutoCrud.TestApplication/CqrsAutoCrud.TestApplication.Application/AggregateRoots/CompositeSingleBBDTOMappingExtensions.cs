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
    public static class CompositeSingleBBDTOMappingExtensions
    {
        public static CompositeSingleBBDTO MapToCompositeSingleBBDTO(this ICompositeSingleBB projectFrom, IMapper mapper)
        {
            return mapper.Map<CompositeSingleBBDTO>(projectFrom);
        }

        public static List<CompositeSingleBBDTO> MapToCompositeSingleBBDTOList(this IEnumerable<ICompositeSingleBB> projectFrom, IMapper mapper)
        {
            return projectFrom.Select(x => x.MapToCompositeSingleBBDTO(mapper)).ToList();
        }
    }
}
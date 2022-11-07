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
    public static class UpdateCompositeManyBBDTOMappingExtensions
    {
        public static UpdateCompositeManyBBDTO MapToUpdateCompositeManyBBDTO(this ICompositeManyBB projectFrom, IMapper mapper)
        {
            return mapper.Map<UpdateCompositeManyBBDTO>(projectFrom);
        }

        public static List<UpdateCompositeManyBBDTO> MapToUpdateCompositeManyBBDTOList(this IEnumerable<ICompositeManyBB> projectFrom, IMapper mapper)
        {
            return projectFrom.Select(x => x.MapToUpdateCompositeManyBBDTO(mapper)).ToList();
        }
    }
}
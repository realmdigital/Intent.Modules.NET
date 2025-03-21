using System;
using AdvancedMappingCrud.Repositories.Tests.Application.Common.Mappings;
using AdvancedMappingCrud.Repositories.Tests.Domain.Entities;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace AdvancedMappingCrud.Repositories.Tests.Application.MultiKeyParents
{
    public class MultiKeyParentDto : IMapFrom<MultiKeyParent>
    {
        public MultiKeyParentDto()
        {
            Name = null!;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public static MultiKeyParentDto Create(Guid id, string name)
        {
            return new MultiKeyParentDto
            {
                Id = id,
                Name = name
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MultiKeyParent, MultiKeyParentDto>();
        }
    }
}
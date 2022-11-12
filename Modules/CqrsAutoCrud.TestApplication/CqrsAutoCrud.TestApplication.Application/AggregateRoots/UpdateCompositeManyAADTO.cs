using System;
using System.Collections.Generic;
using AutoMapper;
using CqrsAutoCrud.TestApplication.Application.Common.Mappings;
using CqrsAutoCrud.TestApplication.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace CqrsAutoCrud.TestApplication.Application.AggregateRoots
{

    public class UpdateCompositeManyAADTO : IMapFrom<CompositeManyAA>
    {
        public UpdateCompositeManyAADTO()
        {
        }

        public static UpdateCompositeManyAADTO Create(
            Guid id,
            string compositeAttr,
            Guid aCompositeSingleId)
        {
            return new UpdateCompositeManyAADTO
            {
                Id = id,
                CompositeAttr = compositeAttr,
                ACompositeSingleId = aCompositeSingleId,
            };
        }

        public Guid Id { get; set; }

        public string CompositeAttr { get; set; }

        public Guid ACompositeSingleId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CompositeManyAA, UpdateCompositeManyAADTO>();
        }
    }
}
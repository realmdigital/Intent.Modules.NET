using System;
using System.Collections.Generic;
using CleanArchitecture.TestApplication.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace CleanArchitecture.TestApplication.Application.AggregateRoots.UpdateAggregateRoot
{
    public class UpdateAggregateRootCommand : IRequest, ICommand
    {
        public UpdateAggregateRootCommand(Guid id,
            string aggregateAttr,
            List<UpdateAggregateRootCompositeManyBDto> composites,
            UpdateAggregateRootCompositeSingleADto? composite,
            UpdateAggregateRootAggregateSingleCDto? aggregate,
            string limitedDomain,
            string limitedService)
        {
            Id = id;
            AggregateAttr = aggregateAttr;
            Composites = composites;
            Composite = composite;
            Aggregate = aggregate;
            LimitedDomain = limitedDomain;
            LimitedService = limitedService;
        }

        public Guid Id { get; private set; }
        public string AggregateAttr { get; set; }
        public List<UpdateAggregateRootCompositeManyBDto> Composites { get; set; }
        public UpdateAggregateRootCompositeSingleADto? Composite { get; set; }
        public UpdateAggregateRootAggregateSingleCDto? Aggregate { get; set; }
        public string LimitedDomain { get; set; }
        public string LimitedService { get; set; }

        public void SetId(Guid id)
        {
            if (Id == default)
            {
                Id = id;
            }
        }
    }
}
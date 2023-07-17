using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.DtoContract", Version = "1.0")]

namespace CleanArchitecture.TestApplication.BlazorClient.HttpClients.AggregateRootsService
{
    public class UpdateAggregateRootCompositeManyBDto
    {
        public string CompositeAttr { get; set; }
        public DateTime? SomeDate { get; set; }
        public Guid AggregateRootId { get; set; }
        public Guid Id { get; set; }
        public List<UpdateAggregateRootCompositeManyBCompositeManyBBDto> Composites { get; set; }
        public UpdateAggregateRootCompositeManyBCompositeSingleBBDto Composite { get; set; }

        public static UpdateAggregateRootCompositeManyBDto Create(
            string compositeAttr,
            DateTime? someDate,
            Guid aggregateRootId,
            Guid id,
            List<UpdateAggregateRootCompositeManyBCompositeManyBBDto> composites,
            UpdateAggregateRootCompositeManyBCompositeSingleBBDto composite)
        {
            return new UpdateAggregateRootCompositeManyBDto
            {
                CompositeAttr = compositeAttr,
                SomeDate = someDate,
                AggregateRootId = aggregateRootId,
                Id = id,
                Composites = composites,
                Composite = composite
            };
        }
    }
}
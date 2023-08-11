using System.ComponentModel.DataAnnotations;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.DtoContract", Version = "2.0")]

namespace CleanArchitecture.TestApplication.BlazorClient.HttpClients.Services.AggregateRoots
{
    public class UpdateAggregateRootCompositeSingleADto
    {
        [Required(ErrorMessage = "Composite attr is required.")]
        public string CompositeAttr { get; set; }
        public Guid Id { get; set; }
        public UpdateAggregateRootCompositeSingleACompositeSingleAADto? Composite { get; set; }
        [Required(ErrorMessage = "Composites is required.")]
        public List<UpdateAggregateRootCompositeSingleACompositeManyAADto> Composites { get; set; }

        public static UpdateAggregateRootCompositeSingleADto Create(
            string compositeAttr,
            Guid id,
            UpdateAggregateRootCompositeSingleACompositeSingleAADto? composite,
            List<UpdateAggregateRootCompositeSingleACompositeManyAADto> composites)
        {
            return new UpdateAggregateRootCompositeSingleADto
            {
                CompositeAttr = compositeAttr,
                Id = id,
                Composite = composite,
                Composites = composites
            };
        }
    }
}
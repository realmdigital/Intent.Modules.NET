using CosmosDB.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.CosmosDB.CosmosDBDocument", Version = "1.0")]

namespace CosmosDB.Infrastructure.Persistence.Documents
{
    internal class CountryDocument
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public Country ToEntity(Country? entity = default)
        {
            entity ??= new Country();

            entity.Id = Id;
            entity.Name = Name;

            return entity;
        }

        public CountryDocument PopulateFromEntity(Country entity)
        {
            Id = entity.Id;
            Name = entity.Name;

            return this;
        }

        public static CountryDocument? FromEntity(Country? entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new CountryDocument().PopulateFromEntity(entity);
        }
    }
}
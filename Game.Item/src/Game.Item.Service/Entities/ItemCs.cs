using Game.Common;

namespace Game.Item.Service.Entities
{

    public class ItemCs : IEntity
    {
        public string? Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }

        public decimal Price { get; set; }

        public DateTimeOffset CreatedDate { get; init; }
    }
}
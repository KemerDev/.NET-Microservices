using Game.Common;

namespace Game.Item.Service.Entities
{

    public class ItemCs : IEntity
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }
}
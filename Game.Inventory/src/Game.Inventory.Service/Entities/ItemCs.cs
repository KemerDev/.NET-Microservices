using Game.Common;

namespace Game.Inventory.Service.Entities
{
    public class ItemCs : IEntity
    {
        public string? Id { get; init; }

        public string? Name { get; init; }

        public string? Description { get; init; }

        public DateTimeOffset AcquiredDate { get; init; }
    }
}
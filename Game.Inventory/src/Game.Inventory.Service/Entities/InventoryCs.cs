using Game.Common;

namespace Game.Inventory.Service.Entities
{
    public class InventoryCs : IEntity
    {
        public string? Id { get; init; }
        public string? UserId { get; init; }

        public List<IEntity> inventoryItems { get; set; }
    }
}
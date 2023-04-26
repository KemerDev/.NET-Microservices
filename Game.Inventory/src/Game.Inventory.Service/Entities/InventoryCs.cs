using Game.Common;
using Game.Inventory.Service.Dtos;

namespace Game.Inventory.Service.Entities
{
    public class InventoryCs : IEntity
    {
        public string? Id { get; init; }
        public string? UserId { get; init; }
        public List<ItemDto> inventoryItems { get; set; }
    }
}
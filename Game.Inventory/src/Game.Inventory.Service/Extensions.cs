using Game.Inventory.Service.Entities;
using Game.Inventory.Service.Dtos;

namespace Game.Inventory.Service
{
    public static class Extensions
    {
        public static InventoryDto AsDto(this InventoryCs inventory)
        {
            return new InventoryDto(inventory.Id, inventory.UserId, inventory.inventoryItems);
        }
    }
}
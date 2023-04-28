using Game.Inventory.Service.Entities;

namespace Game.Inventory.Service.Dtos
{
    public record InventoryDto(List<ContainerCs> inventoryItems);

    public record CreateInventoryDto(string UserId, bool worldInit = false);

    public record GetInventoryDto(string UserId);

    public record InventoryItemDto(string Id, string Name, string Description, DateTimeOffset CreatedDate);

    //public record CreateItemsDto(string Id, string Name, string Description, decimal Price);

    //public record UpdateItemDto(string Name, string Description, decimal Price);

    //public record DeleteItemDto(string Id);
}
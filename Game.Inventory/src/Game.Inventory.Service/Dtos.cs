using Game.Common;

namespace Game.Inventory.Service.Dtos
{
    public record InventoryDto(string Id, string UserId, List<IEntity> inventoryItems);

    public record InventoryItemDto(string itemId, string Name, string Description, DateTimeOffset AcquiredDate);

    public record ItemDto(string Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);

    public record CreateItemsDto(string Id, string Name, string Description, decimal Price);

    public record UpdateItemDto(string Name, string Description, decimal Price);

    public record DeleteItemDto(string Id);
}
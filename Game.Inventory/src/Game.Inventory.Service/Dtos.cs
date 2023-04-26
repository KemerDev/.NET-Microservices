namespace Game.Inventory.Service.Dtos
{
    public record InventoryDto(List<ItemDto> inventoryItems);

    public record CreateInventoryDto(string UserId);

    public record GetInventoryDto(string UserId);

    public record InventoryItemDto(string itemId, string Name, string Description, DateTimeOffset AcquiredDate);

    public record ItemDto(string Id, string Name, string Description);

    //public record CreateItemsDto(string Id, string Name, string Description, decimal Price);

    //public record UpdateItemDto(string Name, string Description, decimal Price);

    //public record DeleteItemDto(string Id);
}
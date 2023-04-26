namespace Game.Item.Service.Dtos
{
    public record ItemDto(string Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);

    public record CreateItemsDto(string Id, string Name, string Description, decimal Price);

    public record UpdateItemDto(string Id, decimal Price);

    public record DeleteItemDto(string Id);
}
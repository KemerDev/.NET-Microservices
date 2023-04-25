using Game.Item.Service.Entities;

namespace Game.Item.Service.Repositories
{
    public interface IItemsRepository
    {
        Task CreateItemsAsync(List<ItemCs> itemList);
        Task DeleteItemAsync(ItemCs entity);
        Task<IEnumerable<ItemCs>> GetItemAsync(List<string> idList);
        Task<IReadOnlyCollection<ItemCs>> GetItemsAsync();
        Task UpdateItemAsync(ItemCs entity);
    }
}
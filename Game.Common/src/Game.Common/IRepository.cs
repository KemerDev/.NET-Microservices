namespace Game.Common
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetItemAsync(List<string> idList);
        Task<IReadOnlyCollection<T>> GetItemsAsync();
        Task<IEnumerable<T>> GetRandomItemsAsync();
        Task CreateItemsAsync(List<T> itemList);
        Task UpdateItemAsync(List<T> entities);
        Task DeleteItemAsync(List<string> entities);
    }
}
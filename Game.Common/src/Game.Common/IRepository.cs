namespace Game.Common
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateItemsAsync(List<T> itemList);
        Task DeleteItemAsync(T entity);
        Task<IEnumerable<T>> GetItemAsync(List<string> idList);
        Task<IReadOnlyCollection<T>> GetItemsAsync();
        Task UpdateItemAsync(T entity);
    }
}
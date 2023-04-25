using MongoDB.Driver;
using Game.Item.Service.Entities;

namespace Game.Item.Service.Repositories
{

    public class ItemsRepository : IItemsRepository
    {
        // Collection Name
        private const string collectionName = "Items";

        // Instance of MongoDB collection
        private readonly IMongoCollection<ItemCs>? dbCollection;

        // MongoDB query filter
        private readonly FilterDefinitionBuilder<ItemCs> filterBuilder = Builders<ItemCs>.Filter;

        public ItemsRepository(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<ItemCs>(collectionName);
        }

        public async Task<IReadOnlyCollection<ItemCs>> GetItemsAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<IEnumerable<ItemCs>> GetItemAsync(List<string> idList)
        {
            FilterDefinition<ItemCs> filter = filterBuilder.In(f => f.Id, idList);

            return await dbCollection.Find(filter).ToListAsync();
        }

        public async Task CreateItemsAsync(List<ItemCs> itemList)
        {
            if (itemList == null)
            {
                throw new ArgumentNullException(nameof(itemList));
            }

            await dbCollection.InsertManyAsync(itemList);
        }

        public async Task UpdateItemAsync(ItemCs entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<ItemCs> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);

            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteItemAsync(ItemCs entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<ItemCs> filter = filterBuilder.Eq(deleteEntity => deleteEntity.Id, entity.Id);

            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
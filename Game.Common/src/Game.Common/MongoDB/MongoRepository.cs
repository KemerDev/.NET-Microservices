using MongoDB.Bson;
using MongoDB.Driver;

namespace Game.Common.MongoDB
{

    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        // Instance of MongoDB collection
        private readonly IMongoCollection<T>? dbCollection;

        // MongoDB query filter
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            dbCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetItemsAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetItemAsync(List<string> idList)
        {
            FilterDefinition<T> filter = filterBuilder.In(f => f.Id, idList);

            return await dbCollection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetRandomItemsAsync()
        {
            return await dbCollection.Aggregate().AppendStage<T>($@"{{ $sample: {{ size: 1000}}}}").ToListAsync();
        }

        public async Task CreateItemsAsync(List<T> itemList)
        {
            if (itemList == null)
            {
                throw new ArgumentNullException(nameof(itemList));
            }

            await dbCollection.InsertManyAsync(itemList);
        }

        public async Task UpdateItemAsync(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                FilterDefinition<T> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
                await dbCollection.ReplaceOneAsync(filter, entity);
            }
        }

        public async Task DeleteItemAsync(List<string> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            FilterDefinition<T> filter = filterBuilder.In(deleteEntity => deleteEntity.Id, entities);

            await dbCollection.DeleteManyAsync(filter);
        }
    }
}
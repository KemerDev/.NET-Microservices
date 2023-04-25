using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Game.Common.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Game.Common.MongoDB
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection builder)
        {
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            builder.AddSingleton(ServiceProvider =>
            {

                var configuration = ServiceProvider.GetService<IConfiguration>();
                var serviceSettings = configuration?.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDbSettings = configuration?.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings?.ConnectionString);

                return mongoClient.GetDatabase(serviceSettings?.ServiceName);
            });

            return builder;
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection builder, string collectionName) where T : IEntity
        {
            builder.AddSingleton<IRepository<T>>(ServiceProvider =>
            {
                var database = ServiceProvider.GetService<IMongoDatabase>();
                return new MongoRepository<T>(database, collectionName);
            });

            return builder;
        }
    }
}
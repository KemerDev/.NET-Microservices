namespace Game.Item.Service.Settings
{
    public class MongoDbSettings
    {
        // init, we don't want the values to be modified
        public string? Host { get; init; }
        public string? Port { get; init; }
        // Populate string
        public string? ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
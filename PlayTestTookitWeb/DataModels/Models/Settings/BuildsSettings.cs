namespace Data.Models.Settings
{
    public class PlayTestBuildsSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDBSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public enum DatabaseCollections
    {
        Builds,
        Configs,
        Data
    }
}

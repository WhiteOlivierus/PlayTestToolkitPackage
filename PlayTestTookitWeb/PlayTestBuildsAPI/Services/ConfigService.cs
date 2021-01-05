using MongoDB.Driver;
using PlayTestBuildsAPI.Models;
using PlayTestBuildsAPI.Models.Settings;
using System.Collections.Generic;
using System.Linq;

namespace PlayTestBuildsAPI.Services
{
    public class ConfigService
    {
        private readonly IMongoCollection<ConfigFile> _configFile;

        public ConfigService(IMongoDBSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);

            string collectionName = DatabaseCollections.Configs.ToString();
            try { _configFile = database.GetCollection<ConfigFile>(collectionName); }
            catch { database.CreateCollection(collectionName); }
            finally { _configFile = database.GetCollection<ConfigFile>(collectionName); }
        }

        public List<ConfigFile> Get() =>
            _configFile.Find(configFile => true).ToList();

        public ConfigFile Get(string id) =>
            _configFile.Find(configFile => configFile.Id == id).FirstOrDefault();

        public ConfigFile Create(ConfigFile configFile)
        {
            _configFile.InsertOne(configFile);
            return configFile;
        }

        public void Update(string id, ConfigFile configFileIn) =>
            _configFile.ReplaceOne(configFile => configFile.Id == id, configFileIn);

        public void Remove(ConfigFile configFileIn) =>
            _configFile.DeleteOne(configFile => configFile.Id == configFileIn.Id);

        public void Remove(string id) =>
            _configFile.DeleteOne(configFile => configFile.Id == id);
    }
}

using MongoDB.Driver;
using PlayTestBuildsAPI.Models;
using PlayTestBuildsAPI.Models.Settings;
using System.Collections.Generic;
using System.Linq;

namespace PlayTestBuildsAPI.Services
{
    public class BuildsService
    {
        private readonly IMongoCollection<BuildFile> _buildFile;

        public BuildsService(IMongoDBSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);

            try { _buildFile = database.GetCollection<BuildFile>(settings.CollectionName); }
            catch { database.CreateCollection(settings.CollectionName); }
            finally { _buildFile = database.GetCollection<BuildFile>(settings.CollectionName); }
        }

        public List<BuildFile> Get() =>
            _buildFile.Find(book => true).ToList();

        public BuildFile Get(string id) =>
            _buildFile.Find(book => book.Id == id).FirstOrDefault();

        public BuildFile Create(BuildFile buildFile)
        {
            _buildFile.InsertOne(buildFile);
            return buildFile;
        }

        public void Update(string id, BuildFile bookIn) =>
            _buildFile.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(BuildFile bookIn) =>
            _buildFile.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _buildFile.DeleteOne(book => book.Id == id);
    }
}

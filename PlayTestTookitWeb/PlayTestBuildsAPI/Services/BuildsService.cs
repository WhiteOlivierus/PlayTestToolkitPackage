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
            _buildFile.Find(buildFile => true).ToList();

        public BuildFile Get(string id) =>
            _buildFile.Find(buildFile => buildFile.Id == id).FirstOrDefault();

        public BuildFile Create(BuildFile buildFile)
        {
            _buildFile.InsertOne(buildFile);
            return buildFile;
        }

        public void Update(string id, BuildFile buildFileIn) =>
            _buildFile.ReplaceOne(buildFile => buildFile.Id == id, buildFileIn);

        public void Remove(BuildFile buildFileIn) =>
            _buildFile.DeleteOne(buildFile => buildFile.Id == buildFileIn.Id);

        public void Remove(string id) =>
            _buildFile.DeleteOne(buildFile => buildFile.Id == id);
    }
}

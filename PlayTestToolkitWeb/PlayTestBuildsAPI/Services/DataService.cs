﻿using Data.Models;
using Data.Models.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace PlayTestBuildsAPI.Services
{
    public class DataService
    {
        private readonly IMongoCollection<DataFile> _dataFile;

        public DataService(IMongoDBSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);

            string collectionName = DatabaseCollections.Data.ToString();
            try { _dataFile = database.GetCollection<DataFile>(collectionName); }
            catch { database.CreateCollection(collectionName); }
            finally { _dataFile = database.GetCollection<DataFile>(collectionName); }
        }

        public IList<DataFile> Get() =>
            _dataFile.Find(dataFile => true).ToList();

        public IList<DataFile> Get(string id) =>
            _dataFile.Find(dataFile => dataFile.ConfigId == id).ToList();

        public DataFile Create(DataFile dataFile)
        {
            _dataFile.InsertOne(dataFile);
            return dataFile;
        }

        public void Update(string id, DataFile dataFileIn) =>
            _dataFile.ReplaceOne(dataFile => dataFile.Id == id, dataFileIn);

        public void Remove(DataFile dataFileIn) =>
            _dataFile.DeleteOne(dataFile => dataFile.Id == dataFileIn.Id);

        public void Remove(string id) =>
            _dataFile.DeleteOne(dataFile => dataFile.Id == id);
    }
}

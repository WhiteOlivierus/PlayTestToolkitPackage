using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace PlayTestBuildsAPI.Models
{
    public class ConfigFile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string BuildId { get; set; }
        public string DataId { get; set; }

        public string Name { get; set; }
        public string ResearchQuestion { get; set; }
        public string Description { get; set; }

        public bool Active { get; set; }
        public int Version { get; set; }

        public string TutorialDescription { get; set; }
        public List<KeyValuePair<string, string>> Input { get; set; }
        public string GoogleForm { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace PlayTestBuildsAPI.Models
{
    public class DataFile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ConfigId { get; set; }

        public double StartTime { get; set; }

        public IList<InputObject> Input { get; set; } = new List<InputObject>();
    }
}

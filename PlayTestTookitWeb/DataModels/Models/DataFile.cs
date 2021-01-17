using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Data.Models
{
    public class DataFile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("configId")]
        public string ConfigId { get; set; }

        [JsonPropertyName("startTime")]
        public double StartTime { get; set; }

        [JsonPropertyName("input")]
        public IList<InputObject> Input { get; set; } = new List<InputObject>();
    }
}

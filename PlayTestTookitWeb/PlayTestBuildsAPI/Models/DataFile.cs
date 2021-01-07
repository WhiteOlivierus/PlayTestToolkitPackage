using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Numerics;

namespace PlayTestBuildsAPI.Models
{
    public class DataFile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ConfigId { get; set; }

        public double StartTime { get; set; }

        public List<InputObject> Input { get; set; } = new List<InputObject>();
    }

    public class InputObject
    {
        public float StartTime { get; set; }
        public float Duration { get; set; }
        public string Key { get; set; }
        public Vector2 ScreenSpacePosition { get; set; }
    }
}

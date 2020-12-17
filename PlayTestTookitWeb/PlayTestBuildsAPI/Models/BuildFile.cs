using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PlayTestBuildsAPI.Models
{
    public class BuildFile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string FileName { get; set; }

        public string Path { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PlayTestID { get; set; }
    }
}

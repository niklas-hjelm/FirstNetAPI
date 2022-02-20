using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FirstNetAPI.Models
{
    public class AnimalDoc
    {
        [BsonElement("id")]
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        
    }
}

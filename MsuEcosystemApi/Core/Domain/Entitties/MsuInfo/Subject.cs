using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entitties.MsuInfo
{
    public class Subject
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
    }
}

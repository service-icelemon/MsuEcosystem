using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entitties.MsuInfo
{
    public class Faculty
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("image")]
        public string ImageUrl { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("deanId")]
        public string DeanId { get; set; }
    }
}

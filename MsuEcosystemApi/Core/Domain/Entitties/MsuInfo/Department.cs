using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entitties.MsuInfo
{
    public class Department
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("facultyId")]
        public string FacultyId { get; set; }
        [BsonElement("image")]
        public string ImageUrl { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("managerId")]
        public string ManagerId { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
    }
}

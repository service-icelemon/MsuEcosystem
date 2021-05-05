using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entitties.Schedule
{
    public class GroupShedule
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("groupNum")]
        public int GroupNumber { get; set; }
        [BsonElement("facultyId")]
        public string FacultyId { get; set; }
        [BsonElement("days")]
        public Day[] Days { get; set; }
    }
}

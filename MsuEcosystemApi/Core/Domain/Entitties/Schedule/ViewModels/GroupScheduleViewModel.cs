using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entitties.Schedule.ViewModels
{
    public class GroupScheduleViewModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("groupNum")]
        public int GroupNumber { get; set; }
        [BsonElement("days")]
        public DayViewModel[] Days { get; set; }
    }
}

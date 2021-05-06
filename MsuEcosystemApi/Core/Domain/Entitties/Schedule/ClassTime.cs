using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entitties.Schedule
{
    public class ClassTime
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}

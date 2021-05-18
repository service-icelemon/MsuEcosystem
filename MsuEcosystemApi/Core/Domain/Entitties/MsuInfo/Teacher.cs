using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entitties.MsuInfo
{
    public class Teacher
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("photo")]
        public string PhotoUrl { get; set; }
        [BsonElement("firstname")]
        public string FirstName { get; set; }
        [BsonElement("lastname")]
        public string LastName { get; set; }
        [BsonElement("fathername")]
        public string FatherName { get; set; }
        [BsonElement("departmentId")]
        public string DepartmentId { get; set; }
        [BsonElement("ScienceDegree")]
        public string ScienceDegree { get; set; }
        [BsonElement("facultyId")]
        public string FacultyId { get; set; }
        [BsonElement("biography")]
        public string Biography { get; set; }
        [BsonElement("code")]
        public int Code { get; set; }
        [BsonElement("subjectIds")]
        public string[] SubjectIds { get; set; }
    }
}

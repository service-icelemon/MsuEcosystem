using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entitties.MsuInfo
{
    public class Student
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
        [BsonElement("groupNumber")]
        public int GroupNumber { get; set; }
        [BsonElement("educationform")]
        public string EducationFormId { get; set; }
        [BsonElement("facultyId")]
        public string FacultyId { get; set; }
        [BsonElement("specialityId")]
        public string SpecialtyId { get; set; }
        [BsonElement("AdmissionYear")]
        public int AdmissionYear { get; set; }
        [BsonElement("studentCardId")]
        public int StudentCardId { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Domain.Entitties.MsuInfo
{
    public class Speciality
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("image")]
        public string ImageUrl { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("departmentId")]
        public string DepartmentId { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("budgetScores")]
        public PassingScores[] BudgetScores { get; set; }
        [BsonElement("paidScores")]
        public PassingScores[] PaidScores { get; set; }
        [BsonElement("subjects")]
        public SpecialitySubjects[] Subjects { get; set; }
        [BsonElement("educationForms")]
        public string[] EducationFormIds { get; set; }
    }
}

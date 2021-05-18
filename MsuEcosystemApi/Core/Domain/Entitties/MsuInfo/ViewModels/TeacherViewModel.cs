namespace Domain.Entitties.MsuInfo.ViewModels
{
    public class TeacherViewModel
    {
        public string Id { get; set; }
        public string PhotoUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string ScienceDegree { get; set; }
        public string FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string Biography { get; set; }
        public Subject[] Subjects { get; set; }
    }
}

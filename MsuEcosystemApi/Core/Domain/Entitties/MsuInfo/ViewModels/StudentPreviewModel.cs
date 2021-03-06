namespace Domain.Entitties.MsuInfo.ViewModels
{
    public class StudentPreviewModel
    {
        public string Id { get; set; }
        public string PhotoUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public int GroupNumber { get; set; }
        public EducationForm EducationForm { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public int AdmissionYear { get; set; }
        public int StudentCardId { get; set; }
    }
}

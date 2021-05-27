namespace Domain.Entitties.MsuInfo.ViewModels
{
    public class SpecialityViewModel
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public DepartmentPreviewModel Department { get; set; }
        public string Description { get; set; }
        public PassingScores[] Scores { get; set; }
        public SpecialitySubjectsViewModel[] Subjects { get; set; }
        public EducationForm[] EducationForms { get; set; }
    }
}

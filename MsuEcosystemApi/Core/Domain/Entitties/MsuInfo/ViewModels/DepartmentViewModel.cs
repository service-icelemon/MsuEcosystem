namespace Domain.Entitties.MsuInfo.ViewModels
{
    public class DepartmentViewModel
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public TeacherPreviewModel Manager { get; set; }
        public string Description { get; set; }
        public TeacherPreviewModel[] Teachers { get; set; }
        public SpecialityPreviewModel[] Specialities { get; set; }
    }
}

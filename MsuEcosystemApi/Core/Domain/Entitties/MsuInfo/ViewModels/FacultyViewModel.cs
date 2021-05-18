namespace Domain.Entitties.MsuInfo.ViewModels
{
    public class FacultyViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public TeacherPreviewModel Dean { get; set; }
    }
}

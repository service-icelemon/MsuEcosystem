namespace Domain.Entitties.News.ViewModels
{
    public class DraftPreviewModel
    {
        public string Id { get; set; }
        public string PreviewImageUrl { get; set; }
        public string Title { get; set; }
        public bool IsReviewed { get; set; }
        //public UserPreviewModel Author { get; set; }
    }
}

using Domain.Entitties.Identity.ViewModels;

namespace Domain.Entitties.News.ViewModels
{
    public class ReviewPreviewModel
    {
        public string ReviewId { get; set; }
        public string Title { get; set; }
        public string PreviewImageUrl { get; set; }
        public UserViewModel Author { get; set; }
    }
}

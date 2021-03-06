using Domain.Entitties.Identity.ViewModels;
using System;

namespace Domain.Entitties.News.ViewModels
{
    public class PublicationViewModel
    {
        public string Id { get; set; }
        public string PreviewImageUrl { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime PublicationDate { get; set; }

        public UserPreviewModel Author { get; set; }
        public UserPreviewModel Editor { get; set; }
    }
}

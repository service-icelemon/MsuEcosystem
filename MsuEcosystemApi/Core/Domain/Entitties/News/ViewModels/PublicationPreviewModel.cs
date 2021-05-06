using Domain.Entitties.Identity.ViewModels;
using System;

namespace Domain.Entitties.News.ViewModels
{
    public class PublicationPreviewModel
    {
        public string Id { get; set; }
        public string PreviewImageUrl { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }

        public UserViewModel Author { get; set; }
        public UserViewModel Editor { get; set; }
    }
}

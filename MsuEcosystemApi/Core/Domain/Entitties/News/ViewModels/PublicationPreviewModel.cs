using Domain.Entitties.Identity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitties.News.ViewModels
{
    public class PublicationPreviewModel
    {
        public string Id { get; set; }
        public string PreviewImageUrl { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }

        public UserViewModel Author { get; set; }
        public UserViewModel Editor { get; set; }
    }
}

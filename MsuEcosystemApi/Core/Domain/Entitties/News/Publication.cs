using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitties.News
{
    public class Publication
    {
        public string Id { get; set; }
        public string ReviewId { get; set; }
        public DateTime PublicationDate { get; set; }
       // public bool IsPinned { get; set; }

        public Review EditedArticle { get; set; }
    }
}

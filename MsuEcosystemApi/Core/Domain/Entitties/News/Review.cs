using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitties.News
{
    public class Review
    {
        public string Id { get; set; }
        public string DraftId { get; set; }
        public string ReviewerId { get; set; }
        public string EditedText { get; set; }
        public string EditetTitle { get; set; }
        public string NewPreviewImageUrl { get; set; }
        public string ReviewText { get; set; }

        public Draft Draft { get; set; } 
    }
}

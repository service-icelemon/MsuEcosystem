﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitties.News
{
    public class Draft
    {
        public string Id { get; set; }
        public string PreviewImageUrl { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsReadyForReview { get; set; }
        public bool IsReviewed { get; set; }
        //public bool IsApproved { get; set; }
        //public bool IsRequiresChanges { get; set; }
    }
}

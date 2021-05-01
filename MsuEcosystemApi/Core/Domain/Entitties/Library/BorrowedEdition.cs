using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entitties.Library
{
    public class BorrowedEdition
    {
        public string Id { get; set; }
        public string RequestId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime ActualReturnDate { get; set; }

        [ForeignKey(nameof(RequestId))]
        public EditionRequest Request { get; set; }
    }
}

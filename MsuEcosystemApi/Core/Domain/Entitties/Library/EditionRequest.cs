using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entitties.Library
{
    public class EditionRequest
    {
        public string Id { get; set; }
        public string EditionId { get; set; }
        public string ReaderId { get; set; }
        public bool Approved { get; set; }
        public bool IsPickedUp { get; set; }
        public string PickUpPointId { get; set; }

        [ForeignKey(nameof(EditionId))]
        public Edition Edition { get; set; }

        [ForeignKey(nameof(PickUpPointId))]
        public PickUpPoint PickUpPoint { get; set; }
    }
}

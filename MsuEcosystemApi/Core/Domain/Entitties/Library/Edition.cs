using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entitties.Library
{
    public class Edition
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string AuthorId { get; set; }
        public string TypeId { get; set; }
        public string GenreId { get; set; }
        public string Description { get; set; }
        public string PublishingHouseId { get; set; }
        public int PublishingYear { get; set; }
        public int AvaibleAmount { get; set; }

        [ForeignKey(nameof(PublishingHouseId))]
        public PublishingHouse PublishingHouse { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }

        [ForeignKey(nameof(TypeId))]
        public EditionType Type { get; set; }
    }
}

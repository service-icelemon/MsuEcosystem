using Domain.Entitties.Library;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class MsuLibraryContext : DbContext
    {
        public MsuLibraryContext(DbContextOptions<MsuLibraryContext> options) : base(options)
        {

        }

        public DbSet<Edition> Editions { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PickUpPoint> PickUpPoints { get; set; }
        public DbSet<EditionRequest> EditionRequests { get; set; }
        public DbSet<BorrowedEdition> BorrowedEditions { get; set; }
        public DbSet<EditionType> EditionTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PublishingHouse> PublishingHouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Edition>()
                .HasOne(i => i.Author);

            modelBuilder.Entity<Edition>()
                .HasOne(i => i.Genre);

            modelBuilder.Entity<Edition>()
                .HasOne(i => i.PublishingHouse);

            modelBuilder.Entity<Edition>()
                .HasOne(i => i.Type);

            modelBuilder.Entity<BorrowedEdition>()
                .HasOne(p => p.Request);

            modelBuilder.Entity<Author>()
                .HasMany(p => p.Editions);

            modelBuilder.Entity<EditionRequest>()
                .HasOne(p => p.PickUpPoint);
        }
    }
}

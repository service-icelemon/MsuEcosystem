using Domain.Entitties.News;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class MsuNewsContext : DbContext
    {
        public MsuNewsContext(DbContextOptions<MsuNewsContext> options) : base(options)
        {

        }

        public DbSet<Draft> Drafts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Publication> Publications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasOne(i => i.Draft);

            modelBuilder.Entity<Publication>()
                .HasOne(p => p.EditedArticle);
        }
    }
}

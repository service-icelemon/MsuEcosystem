using Domain.Entitties.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class MsuIdentityContext : IdentityDbContext<MsuUser>
    {
        public MsuIdentityContext(DbContextOptions<MsuIdentityContext> options) : base(options)
        {

        }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}

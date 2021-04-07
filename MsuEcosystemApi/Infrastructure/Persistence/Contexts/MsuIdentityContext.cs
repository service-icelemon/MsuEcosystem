using Domain.Entitties.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class MsuIdentityContext : IdentityDbContext<MsuUser>
    {
        public MsuIdentityContext(DbContextOptions<MsuIdentityContext> options) : base(options)
        {

        }
    }
}

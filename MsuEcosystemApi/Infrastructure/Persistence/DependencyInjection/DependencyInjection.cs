using Domain.Entitties.News;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.News;

namespace Persistence.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<MsuIdentityContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
            //     b => b.MigrationsAssembly(typeof(MsuIdentityContext).Assembly.FullName));
            //});

            services.AddDbContext<MsuIdentityContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                 b => b.MigrationsAssembly(typeof(MsuIdentityContext).Assembly.FullName)),
                 ServiceLifetime.Transient);

            services.AddDbContext<MsuNewsContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MsuNewsConnection"),
                 b => b.MigrationsAssembly(typeof(MsuNewsContext).Assembly.FullName));
            });


            services.AddScoped<IRepository<Draft>, DraftRepository>();
            services.AddScoped<IRepository<Review>, ReviewRepository>();
            services.AddScoped<IRepository<Publication>, PublicationRepository>();
        }
    }
}

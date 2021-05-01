using Domain.Entitties.Library;
using Domain.Entitties.News;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.Library;
using Persistence.Repositories.News;

namespace Persistence.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<MsuIdentityContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                 b => b.MigrationsAssembly(typeof(MsuIdentityContext).Assembly.FullName)),
                 ServiceLifetime.Transient);

            services.AddDbContext<MsuNewsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MsuNewsConnection"),
                 b => b.MigrationsAssembly(typeof(MsuNewsContext).Assembly.FullName)),
                 ServiceLifetime.Transient);

            services.AddDbContext<MsuLibraryContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("MsuLibraryConnection"),
                 b => b.MigrationsAssembly(typeof(MsuLibraryContext).Assembly.FullName)),
                 ServiceLifetime.Transient);


            services.AddScoped<IRepository<Draft>, DraftRepository>();
            services.AddScoped<IRepository<Review>, ReviewRepository>();
            services.AddScoped<IRepository<Publication>, PublicationRepository>();

            services.AddScoped<IRepository<Edition>, EditionRepository>();
            services.AddScoped<IRepository<Author>, AuthorRepository>();
            services.AddScoped<IRepository<Genre>, GenreRepository>();
            services.AddScoped<IRepository<PickUpPoint>, PickUpPointRepository>();
            services.AddScoped<IRepository<EditionType>, EditionTypeRepository>();
            services.AddScoped<IRepository<BorrowedEdition>, BorrowedEditionRepository>();
            services.AddScoped<IRepository<EditionRequest>, EditionRequestRepository>();
            services.AddScoped<IRepository<PublishingHouse>, PublishingHouseRepository>();
        }
    }
}

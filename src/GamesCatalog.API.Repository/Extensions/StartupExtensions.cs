using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamesCatalog.API.Repository.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDataContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GameCatalogContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("GameCatalogConnectionString")));

            return services;
        }
    }
}

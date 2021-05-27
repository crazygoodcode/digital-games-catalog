using AutoMapper;
using GamesCatalog.API.Repository.Mapping;
using GamesCatalog.API.Repository.Services;
using GamesCatalog.Core.Data;
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

            services.AddTransient<ISeedingService, SeedingService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGameRepository, GameRepository>();

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DefaultProfile>();
            });

            var mapper = mapConfig.CreateMapper();

            services.AddSingleton<IMapper>(mapper);

            return services;
        }
    }
}

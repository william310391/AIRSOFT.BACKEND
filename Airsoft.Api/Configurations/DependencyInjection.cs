using Airsoft.Application.Interfaces;
using Airsoft.Application.Mappings;
using Airsoft.Application.Services;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Repositories;

namespace Airsoft.Api.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            //BD
            services.AddSingleton<DapperContext>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("AirsoftBD");
                return new DapperContext(connectionString);
            });

            // Repositories
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Automapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            // Services
            services.AddScoped<IPersonaService, PersonaService>();


            return services;
        }
    }
}

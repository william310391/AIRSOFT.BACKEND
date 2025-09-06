using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Airsoft.Application.Mappings;
using Airsoft.Application.Services;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using System.Text.Json;


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


            services.AddCors(options =>
             {
                 options.AddPolicy("AllowAll", policy =>
                 {
                     policy.AllowAnyOrigin()   // Permite cualquier origen
                           .AllowAnyMethod()   // Permite cualquier método (GET, POST, etc.)
                           .AllowAnyHeader();  // Permite cualquier header
                 });
             });


            // Repositories
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();            

            // Automapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            services.AddHttpContextAccessor();

            // Services
            services.AddScoped<IPersonaService, PersonaService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserContextService, UserContextService>();

          

            // JWT
            var jwtSettings = config.GetSection("Jwt");
            var keyString = jwtSettings["Key"];
            if (string.IsNullOrEmpty(keyString))
            {
                throw new InvalidOperationException("La clave JWT no está configurada correctamente en la configuración.");
            }
            var key = Encoding.UTF8.GetBytes(keyString.Trim());

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                };

                // Para depurar errores de JWT
                opt.Events = new JwtBearerEvents
                {
                    //OnMessageReceived = context =>
                    //{
                    //    // Intentar leer del header estándar
                    //    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

                    //    if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    //    {
                    //        context.Token = authHeader.Substring("Bearer ".Length).Trim();
                    //    }
                    //    else
                    //    {
                    //        // Si viene en un header personalizado
                    //        context.Token = context.Request.Headers["X-Token"].FirstOrDefault();
                    //    }

                    //    Console.WriteLine($"TOKEN RECIBIDO: {context.Token}");
                    //    return Task.CompletedTask;
                    //},

                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";

                        var response = new ApiResponse<string>
                        {
                            Success = false,
                            Message = "Token inválido o no proporcionado"
                        };

                        var result = JsonSerializer.Serialize(response);
                        await context.Response.WriteAsync(result);
                    },
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Response.ContentType = "application/json";

                        var response = new ApiResponse<string>
                        {
                            Success = false,
                            Message = "Acceso denegado: Rol insuficiente"
                        };

                        var result = JsonSerializer.Serialize(response);
                        await context.Response.WriteAsync(result);
                    },


                    OnMessageReceived = context =>
                    {
                        Console.WriteLine($"TOKEN RECIBIDO: {context.Token}");
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"JWT error: {context.Exception}");
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization();



            return services;
        }
    }
}

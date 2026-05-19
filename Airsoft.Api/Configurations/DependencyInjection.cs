using Airsoft.Application.DTOs;
using Airsoft.Application.Interfaces;
using Airsoft.Application.Mappings;
using Airsoft.Application.Services;
using Airsoft.Domain.Enum;
using Airsoft.Infrastructure.Helpers;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;


namespace Airsoft.Api.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppServices(
     this IServiceCollection services,
     IConfiguration config
 )
        {
            // =========================
            // BASE DE DATOS
            // =========================
            services.AddSingleton<DapperContext>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("AirsoftBD");
                return new DapperContext(connectionString);
            });

            // =========================
            // CORS (CORREGIDO)
            // =========================
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins(
                            "http://localhost:4200",
                            "https://localhost:4200"
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            // =========================
            // REPOSITORIES
            // =========================
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IMenuPaginaRepository, MenuPaginaRepository>();
            services.AddScoped<IDatosRepository, DatosRepository>();
            services.AddScoped<IPersonaCorreoRepository, PersonaCorreoRepository>();
            services.AddScoped<IPersonaTelefonoRepository, PersonaTelefonoRepository>();
            services.AddScoped<IPaisRepository, PaisRepository>();
            services.AddScoped<IUbigeoRepository, UbigeoRepository>();
            services.AddScoped<IContactoRepository, ContactoRepository>();
            services.AddScoped<IContactoSolicitudRepository, ContactoSolicitudRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatMiembroRepository, ChatMiembroRepository>();
            services.AddScoped<IMensajeRepository, MensajeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // =========================
            // AUTOMAPPER
            // =========================
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            // =========================
            // REDIS
            // =========================
            services.AddScoped(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var redisConnection = configuration["Redis:ConnectionString"];

                return new RedisCacheHelper(redisConnection!);
            });

            services.AddHttpContextAccessor();

            // =========================
            // SERVICES
            // =========================
            services.AddScoped<IPersonaService, PersonaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IMenuPaginaService, MenuPaginaService>();
            services.AddScoped<IPersonaCorreoServices, PersonaCorreoServices>();
            services.AddScoped<IPersonaTelefonoService, PersonaTelefonoService>();
            services.AddScoped<IPaisService, PaisService>();
            services.AddScoped<IDatosService, DatosService>();
            services.AddScoped<IUbigeoService, UbigeoService>();
            services.AddScoped<IContactoService, ContactoService>();
            services.AddScoped<IContactoSolicitudService, ContactoSolicitudService>();
            services.AddScoped<IMensajeService, MensajeService>();

            // =========================
            // SIGNALR
            // =========================
            services.AddSignalR();

            // =========================
            // JWT
            // =========================
            var jwtSettings = config.GetSection("Jwt");
            var keyString = jwtSettings["Key"];

            if (string.IsNullOrEmpty(keyString))
                throw new InvalidOperationException("La clave JWT no está configurada.");

            var key = Encoding.UTF8.GetBytes(keyString.Trim());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    RoleClaimType = EnumClaims.UsuarioRol,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var request = context.HttpContext.Request;
                        var path = request.Path;

                        /**
                         * =========================
                         * SIGNALR TOKEN
                         * =========================
                         */
                        var accessToken = request.Query["access_token"];

                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/hub/chat"))
                        {
                            context.Token = accessToken;
                            return Task.CompletedTask;
                        }

                        /**
                         * =========================
                         * API NORMAL
                         * =========================
                         */
                        var authHeader =
                            request.Headers["Authorization"].FirstOrDefault();

                        if (!string.IsNullOrEmpty(authHeader) &&
                            authHeader.StartsWith(
                                "Bearer ",
                                StringComparison.OrdinalIgnoreCase
                            ))
                        {
                            context.Token =
                                authHeader["Bearer ".Length..].Trim();
                        }
                        else
                        {
                            context.Token =
                                request.Headers["X-Token"].FirstOrDefault();
                        }

                        return Task.CompletedTask;
                    },

                    /**
                     * =========================
                     * TOKEN INVÁLIDO
                     * =========================
                     */
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode =
                            StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var response = new ApiResponse<string>
                        {
                            Success = false,
                            Message = "Token inválido o no proporcionado"
                        };

                        await context.Response.WriteAsync(
                            JsonSerializer.Serialize(response)
                        );
                    },

                    /**
                     * =========================
                     * SIN PERMISOS
                     * =========================
                     */
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode =
                            StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var response = new ApiResponse<string>
                        {
                            Success = false,
                            Message = "Acceso denegado: Rol insuficiente"
                        };

                        await context.Response.WriteAsync(
                            JsonSerializer.Serialize(response)
                        );
                    },

                    /**
                     * =========================
                     * ERROR JWT
                     * =========================
                     */
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine(
                            $"JWT ERROR: {context.Exception.Message}"
                        );

                        return Task.CompletedTask;
                    }
                };
            });

            // =========================
            // AUTHORIZATION
            // =========================
            services.AddAuthorization();

            return services;
        }
    }
}

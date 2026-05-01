using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Interfaces;
using Airsoft.Application.Mappings;
using Airsoft.Application.Services;
using Airsoft.Domain.Enum;
using Airsoft.Infrastructure.Intefaces;
using Airsoft.Infrastructure.Persistence;
using Airsoft.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;


namespace Airsoft.Api.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
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
            // CORS
            // =========================
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            // =========================
            // AUTOMAPPER
            // =========================
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
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

            // =========================
            // JWT CONFIG
            // =========================
            var jwtSettings = config.GetSection("Jwt");
            var keyString = jwtSettings["Key"];

            if (string.IsNullOrEmpty(keyString))
                throw new InvalidOperationException("La clave JWT no está configurada.");

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

                    RoleClaimType = EnumClaims.UsuarioRol
                };

                opt.Events = new JwtBearerEvents
                {
                    // =========================
                    // LEER TOKEN
                    // =========================
                    OnMessageReceived = async context =>
                    {
                        var endpoint = context.HttpContext.GetEndpoint();

                        var hasAuthorize = endpoint?.Metadata?.GetMetadata<IAuthorizeData>() != null;

                        if (!hasAuthorize)
                            return;

                        var request = context.HttpContext.Request;

                        // Obtener token
                        var authHeader = request.Headers["Authorization"].FirstOrDefault();

                        if (!string.IsNullOrEmpty(authHeader) &&
                            authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Token = authHeader.Substring("Bearer ".Length).Trim();
                        }
                        else
                        {
                            context.Token = request.Headers["X-Token"].FirstOrDefault();
                        }

                        if (!string.IsNullOrEmpty(context.Token))
                        {
                            var url = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";

                            Console.WriteLine($"Endpoint: {url}");

                            request.EnableBuffering();

                            request.Body.Position = 0;

                            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
                            var body = await reader.ReadToEndAsync();

                            request.Body.Position = 0;

                            Console.WriteLine($"Body: {body}");


                            // Solo leer body si aplica
                            //if (request.ContentLength > 0 &&
                            //    request.Body.CanRead &&
                            //    (request.Method == "POST" || request.Method == "PUT" || request.Method == "PATCH"))
                            //{
                            //    request.EnableBuffering();

                            //    request.Body.Position = 0;

                            //    using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
                            //    var body = await reader.ReadToEndAsync();

                            //    request.Body.Position = 0;

                            //    Console.WriteLine($"Body: {body}");
                            //}
                        }
                    },

                    // =========================
                    // TOKEN INVÁLIDO (401)
                    // =========================
                    OnChallenge = async context =>
                    {

                        var endpoint = context.HttpContext.GetEndpoint();

                        // Verificar si el endpoint tiene [Authorize]
                        var hasAuthorize = endpoint?.Metadata?.GetMetadata<IAuthorizeData>() != null;

                        if (!hasAuthorize)
                        {
                            // 👉 Dejar comportamiento por defecto
                            return;
                        }

                        context.HandleResponse();

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var response = new ApiResponse<string>
                        {
                            Success = false,
                            Message = "Token inválido o no proporcionado"
                        };

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    },

                    // =========================
                    // SIN PERMISOS (403)
                    // =========================
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var response = new ApiResponse<string>
                        {
                            Success = false,
                            Message = "Acceso denegado: Rol insuficiente"
                        };

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    },

                    // =========================
                    // ERROR DE AUTENTICACIÓN
                    // =========================
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"JWT ERROR: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },

                    // =========================
                    // TOKEN VÁLIDO
                    // =========================
                    //OnTokenValidated = context =>
                    //{

                    //    var endpoint = context.HttpContext.GetEndpoint();

                    //    // Verificar si el endpoint tiene [Authorize]
                    //    var hasAuthorize = endpoint?.Metadata?.GetMetadata<IAuthorizeData>() != null;

                    //    if (!hasAuthorize)
                    //    {
                    //        // 👉 Dejar comportamiento por defecto
                    //        return Task.CompletedTask;
                    //    }

                    //    Console.WriteLine("TOKEN VÁLIDO");
                    //    return Task.CompletedTask;
                    //}
                };
            });

            // =========================
            // AUTHORIZATION (IMPORTANTE)
            // =========================
            services.AddAuthorization(); // 👈 SIN FallbackPolicy

            return services;
        }
    }
    //public static class DependencyInjection
    //{
    //    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
    //    {
    //        //BD
    //        services.AddSingleton<DapperContext>(sp =>
    //        {
    //            var configuration = sp.GetRequiredService<IConfiguration>();
    //            var connectionString = configuration.GetConnectionString("AirsoftBD");
    //            return new DapperContext(connectionString);
    //        });


    //        services.AddCors(options =>
    //         {
    //             options.AddPolicy("AllowAll", policy =>
    //             {
    //                 policy.AllowAnyOrigin()   // Permite cualquier origen
    //                       .AllowAnyMethod()   // Permite cualquier método (GET, POST, etc.)
    //                       .AllowAnyHeader();  // Permite cualquier header
    //             });
    //         });


    //        // Repositories
    //        services.AddScoped<IPersonaRepository, PersonaRepository>();
    //        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    //        services.AddScoped<IRolRepository, RolRepository>();
    //        services.AddScoped<IMenuPaginaRepository, MenuPaginaRepository>();
    //        services.AddScoped<IDatosRepository, DatosRepository>();
    //        services.AddScoped<IPersonaCorreoRepository, PersonaCorreoRepository>();
    //        services.AddScoped<IPersonaTelefonoRepository, PersonaTelefonoRepository>();
    //        services.AddScoped<IPaisRepository, PaisRepository>();
    //        services.AddScoped<IUnitOfWork, UnitOfWork>();  

    //        // Automapper
    //        services.AddAutoMapper(cfg =>
    //        {
    //            cfg.AddProfile<MappingProfile>();
    //        });

    //        services.AddHttpContextAccessor();

    //        // Services
    //        services.AddScoped<IPersonaService, PersonaService>();
    //        services.AddScoped<IUsuarioService, UsuarioService>();
    //        services.AddScoped<IJwtService, JwtService>();
    //        services.AddScoped<IAuthService, AuthService>();
    //        services.AddScoped<IUserContextService, UserContextService>();
    //        services.AddScoped<IMenuPaginaService, MenuPaginaService>();
    //        services.AddScoped<IPersonaCorreoServices, PersonaCorreoServices>();
    //        services.AddScoped<IPersonaTelefonoService, PersonaTelefonoService>();
    //        services.AddScoped<IDatosService, DatosService>();



    //        // JWT
    //        var jwtSettings = config.GetSection("Jwt");
    //        var keyString = jwtSettings["Key"];
    //        if (string.IsNullOrEmpty(keyString))
    //        {
    //            throw new InvalidOperationException("La clave JWT no está configurada correctamente en la configuración.");
    //        }
    //        var key = Encoding.UTF8.GetBytes(keyString.Trim());
    //        services.AddAuthentication(opt =>
    //        {
    //            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //        })
    //        .AddJwtBearer(opt =>
    //        {
    //            opt.TokenValidationParameters = new TokenValidationParameters
    //            {
    //                ValidateIssuer = true,
    //                ValidateAudience = true,
    //                ValidateLifetime = true,
    //                ValidateIssuerSigningKey = true,
    //                ValidIssuer = jwtSettings["Issuer"],
    //                ValidAudience = jwtSettings["Audience"],
    //                IssuerSigningKey = new SymmetricSecurityKey(key),

    //                RoleClaimType = EnumClaims.UsuarioRol // 👈 ahora sí [Authorize(Roles="Admin")] funciona

    //            };

    //            // Para depurar errores de JWT
    //            opt.Events = new JwtBearerEvents
    //            {
    //                OnMessageReceived = context =>
    //                {
    //                    // Intentar leer del header estándar
    //                    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

    //                    if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
    //                    {
    //                        context.Token = authHeader.Substring("Bearer ".Length).Trim();
    //                    }
    //                    else
    //                    {
    //                        // Si viene en un header personalizado
    //                        context.Token = context.Request.Headers["X-Token"].FirstOrDefault();
    //                    }

    //                    Console.WriteLine($"TOKEN RECIBIDO: {context.Token}");
    //                    return Task.CompletedTask;
    //                },

    //                OnChallenge = async context =>
    //                {
    //                    context.HandleResponse();

    //                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    //                    context.Response.ContentType = "application/json";

    //                    var response = new ApiResponse<string>
    //                    {
    //                        Success = false,
    //                        Message = "Token inválido o no proporcionado"
    //                    };

    //                    var result = JsonSerializer.Serialize(response);
    //                    await context.Response.WriteAsync(result);
    //                },
    //                OnForbidden = async context =>
    //                {
    //                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
    //                    context.Response.ContentType = "application/json";

    //                    var response = new ApiResponse<string>
    //                    {
    //                        Success = false,
    //                        Message = "Acceso denegado: Rol insuficiente"
    //                    };

    //                    var result = JsonSerializer.Serialize(response);
    //                    await context.Response.WriteAsync(result);
    //                },


    //                //OnMessageReceived = context =>
    //                //{
    //                //    Console.WriteLine($"TOKEN RECIBIDO: {context.Token}");
    //                //    return Task.CompletedTask;
    //                //},
    //                OnAuthenticationFailed = context =>
    //                {
    //                    Console.WriteLine($"JWT error: {context.Exception}");
    //                    return Task.CompletedTask;
    //                }
    //            };
    //        });

    //        services.AddAuthorization();



    //        return services;
    //    }
    //}
}

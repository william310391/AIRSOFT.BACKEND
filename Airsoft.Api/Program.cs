using Airsoft.Api.Configurations;
using Airsoft.Api.Hubs;
using Airsoft.Application.Middlewares;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// =========================
// SERVICES
// =========================
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

// =========================
// SCALAR
// =========================
app.MapScalarApiReference(option =>
{
    option.Title = "Airsoft API Reference";
    option.DarkMode = true;
});

// =========================
// MIDDLEWARE GLOBAL
// =========================
app.UseMiddleware<ExceptionMiddleware>();

// =========================
// HTTPS
// =========================
app.UseHttpsRedirection();

// =========================
// ROUTING
// =========================
app.UseRouting();

// =========================
// CORS
// =========================
app.UseCors("AllowAll");

// =========================
// AUTH
// =========================
app.UseAuthentication();
app.UseAuthorization();

// =========================
// OPENAPI DEV
// =========================
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// =========================
// ENDPOINTS
// =========================
app.MapControllers();

/**
 * IMPORTANTE:
 * Ruta final:
 * https://localhost:xxxx/hubs/chat
 */
app.MapHub<ChatHub>("/hub/chat");

app.Run();
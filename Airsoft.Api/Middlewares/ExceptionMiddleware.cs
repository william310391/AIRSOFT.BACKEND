using Airsoft.Application.DTOs.Response;
using Airsoft.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Airsoft.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiResponseExceptions ex) // Captura tu excepción personalizada
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.StatusCode;

                var result = JsonSerializer.Serialize(ex.Response);
                await context.Response.WriteAsync(result);
            }
            catch (Exception ex) // Captura cualquier otra excepción
            {
                _logger.LogError(ex, "Error no controlado");


                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Ocurrió un error interno",
                };

                var result = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(result);
            }
        }
    }



}

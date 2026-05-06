using Airsoft.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Airsoft.Application.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiResponseExceptions ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    success = ex.Response.Success,
                    message = ex.Response.Message,
                    data = ex.Response.Data is Exception error
                        ? error.Message
                        : ex.Response.Data
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response)
                );
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    success = false,
                    message = "Error interno del servidor",
                    error = ex.Message
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response)
                );
            }
        }


        //    public async Task InvokeAsync(HttpContext context)
        //    {
        //        try
        //        {
        //            await _next(context);
        //        }
        //        catch (ApiResponseExceptions ex)
        //        {
        //            context.Response.StatusCode = ex.StatusCode;
        //            context.Response.ContentType = "application/json";

        //            await context.Response.WriteAsync(
        //                JsonSerializer.Serialize(ex.Response)
        //            );
        //        }
        //        catch (Exception)
        //        {
        //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //            context.Response.ContentType = "application/json";

        //            await context.Response.WriteAsync(
        //                JsonSerializer.Serialize(new
        //                {
        //                    success = false,
        //                    message = "Error interno del servidor"
        //                })
        //            );
        //        }
        //    }
        }
    }

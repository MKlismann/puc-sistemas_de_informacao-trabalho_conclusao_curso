using ExampleProjectsCommomResources.Domain.Models.ApplicationModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ExampleProjectsCommomResources.Middlewares
{
    public class GlobalErrorInterceptorMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorInterceptorMiddleware(RequestDelegate next)
        {
            this._next = next ?? throw new ArgumentException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var error = new Error(exception);

                await WriteResponseAxync(context, error);
            }
        }

        private async Task WriteResponseAxync(HttpContext context, Error error)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }

    public static class GlobalErrorInterceptorMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalErrorInterceptorMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<GlobalErrorInterceptorMiddleware>();
    }
}

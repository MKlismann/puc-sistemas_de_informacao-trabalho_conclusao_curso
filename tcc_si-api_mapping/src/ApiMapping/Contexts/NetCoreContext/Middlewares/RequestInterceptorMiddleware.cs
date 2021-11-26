using ApiMapping.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ApiMapping.Contexts.NetCoreContext.Middlewares
{
    public class RequestInterceptorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _consumedApiName;

        public RequestInterceptorMiddleware(
            RequestDelegate next, 
            string consumedApiName)
        {
            this._next = next ?? throw new ArgumentException(nameof(next));
            _consumedApiName = consumedApiName;
        }

        public async Task Invoke(
            HttpContext context, 
            IApiMappingCoreOperationsService apiMappingService)
        {
            apiMappingService.CreateOrUpdateApiDependency(
                context.Request, 
                _consumedApiName);          

            await _next(context);
        }
    }

    public static class RequestInterceptorMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestInterceptorMiddleware(
            this IApplicationBuilder builder, 
            string consumedApiName)
        {
            builder.UseMiddleware<RequestInterceptorMiddleware>(consumedApiName);

            return builder;
        }
    }
}

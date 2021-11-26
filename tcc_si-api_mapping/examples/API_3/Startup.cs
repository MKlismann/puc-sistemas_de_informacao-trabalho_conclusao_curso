using API_3.DependecyInjection;
using API_3.Domain.Constants;
using ApiMapping.Contexts.NetCoreContext.DependencyInjection;
using ApiMapping.Contexts.NetCoreContext.Middlewares;
using ExampleProjectsCommomResources.DependecyInjection;
using ExampleProjectsCommomResources.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API_3
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true)
                    .AddHttpContextAccessor();

            services
                .AddApiMappingDependencies(Configuration) //TODO: IMPORTAÇÃO NECESSÁRIA
                .AddSwaggerDependencies(
                    ApplicationConstants.ApplicationVersion,
                    ApplicationConstants.ApplicationName,
                    ApplicationConstants.ApplicationDescription)
                .AddAPI_2RestApiDependencies(Configuration)
                .AddAPI_4RestApiDependencies(Configuration)
                .AddAPI_5RestApiDependencies(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            app.UseRequestInterceptorMiddleware(ApplicationConstants.ApplicationIdentifierValue)
               .UseGlobalErrorInterceptorMiddleware()
               .UseApiMappingDependencies( //TODO: IMPORTAÇÃO NECESSÁRIA
                    services,
                    ApplicationConstants.ApplicationIdentifierValue,
                    ApplicationConstants.ApplicationDescription);

            app.UseHttpsRedirection()
               .UseRouting()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllers();
               });

            app.UseSwaggerDependencies(ApplicationConstants.ApplicationIdentifierValue);
        }
    }
}

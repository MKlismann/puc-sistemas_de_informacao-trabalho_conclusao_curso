using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace ExampleProjectsCommomResources.DependecyInjection
{
    public static class SwaggerDependencies
    {
        public static IServiceCollection AddSwaggerDependencies(
            this IServiceCollection services, 
            string applicationVersion,
            string applicationName,
            string applicationDescription)
        {
            services.AddSwaggerGen(swaggerConfiguration =>
            {
                swaggerConfiguration.SwaggerDoc(
                    applicationVersion,
                    new OpenApiInfo
                    {
                        Version = applicationVersion,
                        Title = applicationName,
                        Description = applicationDescription
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swaggerConfiguration.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDependencies(this IApplicationBuilder app, string applicationIdentifierValue)
        {
            app.UseSwagger();
            app.UseSwaggerUI(swaggerConfiguration =>
            {
                swaggerConfiguration.SwaggerEndpoint("/swagger/v1/swagger.json", applicationIdentifierValue);
                swaggerConfiguration.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}

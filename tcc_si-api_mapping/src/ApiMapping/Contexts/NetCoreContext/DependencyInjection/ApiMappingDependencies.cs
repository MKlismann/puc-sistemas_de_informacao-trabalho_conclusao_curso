using ApiMapping.Contexts.NetCoreContext.Infraestructure.Repositories.Data;
using ApiMapping.Contexts.NetCoreContext.Infraestructure.Repositories.Data.Contexts;
using ApiMapping.Contexts.NetCoreContext.Services;
using ApiMapping.Domain.Constants;
using ApiMapping.Domain.Entities.Infraestructure.Repositories.Data;
using ApiMapping.Domain.Interfaces.Infraestructure.Repositories.Data;
using ApiMapping.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApiMapping.Contexts.NetCoreContext.DependencyInjection
{
    public static class ApiMappingDependencies
    {
        public static IServiceCollection AddApiMappingDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            ConfigureDatabaseDependencies(
                services, 
                configuration);
            ConfigureServicesAndRepositoriesDependencies(services);

            return services;
        }

        public static IApplicationBuilder UseApiMappingDependencies(
            this IApplicationBuilder builder,
            IServiceProvider services,
            string apiName,
            string apiDescription)
        {
            if (string.IsNullOrWhiteSpace(apiName))
                throw new Exception("Invalid ApiName");

            var apiMappingService = services.GetService<IApiMappingCoreOperationsService>();
            apiMappingService.CreateOrUpdateApi(
                new ApiEntity
                {
                    Name = apiName,
                    Description = apiDescription
                });

            return builder;
        }

        private static void ConfigureServicesAndRepositoriesDependencies(IServiceCollection services)
        {
            services
                .AddScoped<IApiMappingCoreOperationsService, ApiMappingCoreOperationsService>()
                .AddScoped<IApiMappingReportsOperationsService, ApiMappingReportsOperationsService>()
                .AddScoped<IApiMappingCustomRequestContextService, ApiMappingCustomRequestContextService>()
                .AddScoped<IApiMappingRepository, ApiMappingRepository>();
        }

        private static void ConfigureDatabaseDependencies(IServiceCollection services, IConfiguration configuration)
        {
            var apiMappingDatabaseConnectionString = configuration.GetConnectionString(AppSettingsConstants.ApiMappingDatabaseConnectionString);
            if (string.IsNullOrWhiteSpace(apiMappingDatabaseConnectionString))
                throw new Exception("ApiMappingDatabaseConnectionString not found in appSettings.json.");

            services.AddDbContext<ApiMappingDatabaseContext>(options => options.UseSqlServer(apiMappingDatabaseConnectionString));
        }
    }
}

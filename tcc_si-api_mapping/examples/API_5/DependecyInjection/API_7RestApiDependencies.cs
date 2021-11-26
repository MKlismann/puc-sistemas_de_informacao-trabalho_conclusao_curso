using API_5.Domain.Constants;
using API_5.Domain.Interfaces.Infraestructure.Repositories.Rest;
using API_5.Infraestructure.Repositories.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API_5.DependecyInjection
{
    public static class API_7RestApiDependencies
    {
        public static IServiceCollection AddAPI_7RestApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAPI_7RestApiRepository, API_7RestApiRepository>();

            services.AddHttpClient(API_7RestApiRepository.HttpClientName, configureClient =>
            {
                configureClient.BaseAddress = new Uri(configuration.GetValue<string>(AppSettingsConstants.API_7RestApiUrl));
                configureClient.DefaultRequestHeaders.Add( //TODO: IMPORTAÇÃO NECESSÁRIA
                    ApiMapping.Domain.Constants.HeadersConstants.ConsumerApiNameHeaderKey,
                    ApplicationConstants.ApplicationIdentifierValue);
            });

            return services;
        }
    }
}

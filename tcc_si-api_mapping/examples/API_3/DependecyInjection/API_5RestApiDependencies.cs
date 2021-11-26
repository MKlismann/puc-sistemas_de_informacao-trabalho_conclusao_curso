using API_3.Domain.Constants;
using API_3.Domain.Interfaces.Infraestructure.Repositories.Rest;
using API_3.Infraestructure.Repositories.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API_3.DependecyInjection
{
    public static class API_5RestApiDependencies
    {
        public static IServiceCollection AddAPI_5RestApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAPI_5RestApiRepository, API_5RestApiRepository>();

            services.AddHttpClient(API_5RestApiRepository.HttpClientName, configureClient =>
            {
                configureClient.BaseAddress = new Uri(configuration.GetValue<string>(AppSettingsConstants.API_5RestApiUrl));
                configureClient.DefaultRequestHeaders.Add( //TODO: IMPORTAÇÃO NECESSÁRIA
                    ApiMapping.Domain.Constants.HeadersConstants.ConsumerApiNameHeaderKey,
                    ApplicationConstants.ApplicationIdentifierValue);
            });

            return services;
        }
    }
}

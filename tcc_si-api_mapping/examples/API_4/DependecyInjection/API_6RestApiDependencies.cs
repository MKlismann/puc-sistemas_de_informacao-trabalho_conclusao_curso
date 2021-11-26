using API_4.Domain.Constants;
using API_4.Domain.Interfaces.Infraestructure.Repositories.Rest;
using API_4.Infraestructure.Repositories.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API_4.DependecyInjection
{
    public static class API_6RestApiDependencies
    {
        public static IServiceCollection AddAPI_6RestApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAPI_6RestApiRepository, API_6RestApiRepository>();

            services.AddHttpClient(API_6RestApiRepository.HttpClientName, configureClient =>
            {
                configureClient.BaseAddress = new Uri(configuration.GetValue<string>(AppSettingsConstants.API_6RestApiUrl));
                configureClient.DefaultRequestHeaders.Add( //TODO: IMPORTAÇÃO NECESSÁRIA
                    ApiMapping.Domain.Constants.HeadersConstants.ConsumerApiNameHeaderKey,
                    ApplicationConstants.ApplicationIdentifierValue);
            });

            return services;
        }
    }
}

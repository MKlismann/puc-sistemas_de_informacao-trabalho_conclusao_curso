using API_3.Domain.Constants;
using API_3.Domain.Interfaces.Infraestructure.Repositories.Rest;
using API_3.Infraestructure.Repositories.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API_3.DependecyInjection
{
    public static class API_4RestApiDependencies
    {
        public static IServiceCollection AddAPI_4RestApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAPI_4RestApiRepository, API_4RestApiRepository>();

            services.AddHttpClient(API_4RestApiRepository.HttpClientName, configureClient =>
            {
                configureClient.BaseAddress = new Uri(configuration.GetValue<string>(AppSettingsConstants.API_4RestApiUrl));
                configureClient.DefaultRequestHeaders.Add( //TODO: IMPORTAÇÃO NECESSÁRIA
                    ApiMapping.Domain.Constants.HeadersConstants.ConsumerApiNameHeaderKey,
                    ApplicationConstants.ApplicationIdentifierValue);
            });

            return services;
        }
    }
}

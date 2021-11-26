using API_3.Domain.Interfaces.Infraestructure.Repositories.Rest;
using ApiMapping.Domain.Constants;
using ApiMapping.Domain.Interfaces.Services;
using ExampleProjectsCommomResources.Domain.Entities;
using ExampleProjectsCommomResources.Domain.Models.ApplicationModels;
using ExampleProjectsCommomResources.Infraestructure.Repositories.Rest.Base;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_3.Infraestructure.Repositories.Rest
{
    public class API_5RestApiRepository : BaseRestRepository, IAPI_5RestApiRepository
    {
        public const string HttpClientName = "API_5RestApiRepository";
        private IApiMappingCustomRequestContextService _apiMappingCustomRequestContextService;
        public API_5RestApiRepository(
            IHttpClientFactory httpClientFactory, IApiMappingCustomRequestContextService apiMappingCustomRequestContextService)
            : base(
                  httpClientFactory.CreateClient(HttpClientName),
                  apiMappingCustomRequestContextService)
        {
            _apiMappingCustomRequestContextService = apiMappingCustomRequestContextService;
        }

        public async Task<Result<string>> API_5_Resource_1()
        {
            const string thisConsumedResourceIdentifier = "api_5/resource_1";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumedApiResourceNameHeaderKey, thisConsumedResourceIdentifier);

            var endpoint = $"api_5/resource_1";

            (var operationFailed, var errorData, var successResponse) = await base.GetAsync<GenericEntity>(endpoint);
            if (operationFailed)
            {
                return Result<string>.Fail(errorData);
            }

            return Result<string>.Success(successResponse.Result);
        }
    }
}

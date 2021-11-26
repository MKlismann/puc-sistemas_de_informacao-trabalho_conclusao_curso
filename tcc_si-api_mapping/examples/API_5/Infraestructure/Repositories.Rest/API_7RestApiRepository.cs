using API_5.Domain.Interfaces.Infraestructure.Repositories.Rest;
using ApiMapping.Domain.Constants;
using ApiMapping.Domain.Interfaces.Services;
using ExampleProjectsCommomResources.Domain.Entities;
using ExampleProjectsCommomResources.Domain.Models.ApplicationModels;
using ExampleProjectsCommomResources.Infraestructure.Repositories.Rest.Base;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_5.Infraestructure.Repositories.Rest
{
    public class API_7RestApiRepository : BaseRestRepository, IAPI_7RestApiRepository
    {
        public const string HttpClientName = "API_7RestApiRepository";
        private IApiMappingCustomRequestContextService _apiMappingCustomRequestContextService;
        public API_7RestApiRepository(
            IHttpClientFactory httpClientFactory, IApiMappingCustomRequestContextService apiMappingCustomRequestContextService)
            : base(
                  httpClientFactory.CreateClient(HttpClientName),
                  apiMappingCustomRequestContextService)
        {
            _apiMappingCustomRequestContextService = apiMappingCustomRequestContextService;
        }

        public async Task<Result<string>> API_7_Resource_1()
        {
            const string thisConsumedResourceIdentifier = "api_7/resource_1";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumedApiResourceNameHeaderKey, thisConsumedResourceIdentifier);

            var endpoint = $"api_7/resource_1";

            (var operationFailed, var errorData, var successResponse) = await base.GetAsync<GenericEntity>(endpoint);
            if (operationFailed)
            {
                return Result<string>.Fail(errorData);
            }

            return Result<string>.Success(successResponse.Result);
        }
    }
}

using API_2.Domain.Interfaces.Infraestructure.Repositories.Rest;
using ApiMapping.Domain.Constants;
using ApiMapping.Domain.Interfaces.Services;
using ExampleProjectsCommomResources.Domain.Entities;
using ExampleProjectsCommomResources.Domain.Models.ApplicationModels;
using ExampleProjectsCommomResources.Infraestructure.Repositories.Rest.Base;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_2.Infraestructure.Repositories.Rest
{
    public class API_2RestApiRepository : BaseRestRepository, IAPI_2RestApiRepository
    {
        public const string HttpClientName = "API_2RestApiRepository";
        private IApiMappingCustomRequestContextService _apiMappingCustomRequestContextService;
        public API_2RestApiRepository(
            IHttpClientFactory httpClientFactory, IApiMappingCustomRequestContextService apiMappingCustomRequestContextService)
            : base(
                  httpClientFactory.CreateClient(HttpClientName),
                  apiMappingCustomRequestContextService)
        {
            _apiMappingCustomRequestContextService = apiMappingCustomRequestContextService;
        }

        public async Task<Result<string>> API_2_Resource_2()
        {
            const string thisConsumedResourceIdentifier = "api_2/resource_2";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumedApiResourceNameHeaderKey, thisConsumedResourceIdentifier);

            var endpoint = $"api_2/resource_2";

            (var operationFailed, var errorData, var successResponse) = await base.GetAsync<GenericEntity>(endpoint);
            if (operationFailed)
            {
                return Result<string>.Fail(errorData);
            }

            return Result<string>.Success(successResponse.Result);
        }
    }
}

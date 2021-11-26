using API_3.Domain.Interfaces.Infraestructure.Repositories.Rest;
using ApiMapping.Domain.Constants;
using ApiMapping.Domain.Interfaces.Services;
using ExampleProjectsCommomResources.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class API_3Controller : ControllerBase
    {
        private IApiMappingCustomRequestContextService _apiMappingCustomRequestContextService;
        private IAPI_4RestApiRepository _API_4RestApiRepository;
        private IAPI_5RestApiRepository _API_5RestApiRepository;
        private IAPI_2RestApiRepository _API_2RestApiRepository;

        public API_3Controller(IApiMappingCustomRequestContextService apiMappingCustomRequestContextService,
            IAPI_4RestApiRepository API_4RestApiRepository,
            IAPI_5RestApiRepository API_5RestApiRepository,
            IAPI_2RestApiRepository API_2RestApiRepository)
        {
            _apiMappingCustomRequestContextService = apiMappingCustomRequestContextService;
            _API_4RestApiRepository = API_4RestApiRepository;
            _API_5RestApiRepository = API_5RestApiRepository;
            _API_2RestApiRepository = API_2RestApiRepository;
        }


        [HttpGet("resource_1")]
        [ProducesResponseType(typeof(GenericEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> API_3_Resource_1()
        {
            const string thisResourceIdentifier = "api_3/resource_1";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            (var operationFailed, var errorData, var successData) = await _API_4RestApiRepository.API_4_Resource_1();
            if (operationFailed)
            {
                return StatusCode(StatusCodes.Status400BadRequest, errorData);
            }

            var successDataResponse = new GenericEntity($"{successData} [+] Someone data from {thisResourceIdentifier}");

            return StatusCode(StatusCodes.Status200OK, successDataResponse);
        }

        [HttpGet("resource_2")]
        [ProducesResponseType(typeof(GenericEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> API_3_Resource_2()
        {
            const string thisResourceIdentifier = "api_3/resource_2";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            (var operationFailedApi_5_Resource_1, var errorDataApi_5_Resource_1, var successDataApi_5_Resource_1) = await _API_5RestApiRepository.API_5_Resource_1();
            if (operationFailedApi_5_Resource_1)
            {
                return StatusCode(StatusCodes.Status400BadRequest, errorDataApi_5_Resource_1);
            }

            (var operationFailedApi_2_Resource_2, var errorDataApi_2_Resource_2, var successDataApi_2_Resource_2) = await _API_2RestApiRepository.API_2_Resource_2();
            if (operationFailedApi_2_Resource_2)
            {
                return StatusCode(StatusCodes.Status400BadRequest, errorDataApi_2_Resource_2);
            }

            var successDataResponse = new GenericEntity($"{successDataApi_2_Resource_2} [+] {successDataApi_5_Resource_1} [+] Someone data from {thisResourceIdentifier}");

            return StatusCode(StatusCodes.Status200OK, successDataResponse);
        }
    }
}

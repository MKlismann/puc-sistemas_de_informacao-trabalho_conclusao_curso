using API_5.Domain.Interfaces.Infraestructure.Repositories.Rest;
using ApiMapping.Domain.Constants;
using ApiMapping.Domain.Interfaces.Services;
using ExampleProjectsCommomResources.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class API_5Controller : ControllerBase
    {
        private IApiMappingCustomRequestContextService _apiMappingCustomRequestContextService;
        private IAPI_7RestApiRepository _API_7RestApiRepository;

        public API_5Controller(IApiMappingCustomRequestContextService apiMappingCustomRequestContextService, IAPI_7RestApiRepository API_7RestApiRepository)
        {
            _apiMappingCustomRequestContextService = apiMappingCustomRequestContextService;
            _API_7RestApiRepository = API_7RestApiRepository;
        }


        [HttpGet("resource_1")]
        [ProducesResponseType(typeof(GenericEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> API_5_Resource_1()
        {
            const string thisResourceIdentifier = "api_5/resource_1";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            (var operationFailed, var errorData, var successData) = await _API_7RestApiRepository.API_7_Resource_1();
            if (operationFailed)
            {
                return StatusCode(StatusCodes.Status400BadRequest, errorData);
            }

            var successDataResponse = new GenericEntity($"{successData} [+] Someone data from {thisResourceIdentifier}");

            return StatusCode(StatusCodes.Status200OK, successDataResponse);
        }

        [HttpGet("resource_2")]
        [ProducesResponseType(typeof(GenericEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> API_5_Resource_2()
        {
            const string thisResourceIdentifier = "api_5/resource_2";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            var successData = new GenericEntity($"Someone data from {thisResourceIdentifier}");

            return StatusCode(StatusCodes.Status200OK, successData);
        }
    }
}

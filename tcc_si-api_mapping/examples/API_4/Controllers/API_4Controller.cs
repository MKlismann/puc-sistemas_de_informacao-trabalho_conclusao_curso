using API_4.Domain.Interfaces.Infraestructure.Repositories.Rest;
using ApiMapping.Domain.Constants;
using ApiMapping.Domain.Interfaces.Services;
using ExampleProjectsCommomResources.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class API_4Controller : ControllerBase
    {
        private IApiMappingCustomRequestContextService _apiMappingCustomRequestContextService;
        private IAPI_6RestApiRepository _API_6RestApiRepository;

        public API_4Controller(IApiMappingCustomRequestContextService apiMappingCustomRequestContextService, IAPI_6RestApiRepository API_6RestApiRepository)
        {
            _apiMappingCustomRequestContextService = apiMappingCustomRequestContextService;
            _API_6RestApiRepository = API_6RestApiRepository;
        }


        [HttpGet("resource_1")]
        [ProducesResponseType(typeof(GenericEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> API_4_Resource_1()
        {
            const string thisResourceIdentifier = "api_4/resource_1";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            (var operationFailed, var errorData, var successData) = await _API_6RestApiRepository.API_6_Resource_1();
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
        public async Task<IActionResult> API_4_Resource_2()
        {
            const string thisResourceIdentifier = "api_4/resource_2";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            var successData = new GenericEntity($"Someone data from {thisResourceIdentifier}");

            return StatusCode(StatusCodes.Status200OK, successData);
        }
    }
}

using ApiMapping.Domain.Constants;
using ApiMapping.Domain.Interfaces.Services;
using ExampleProjectsCommomResources.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class API_7Controller : ControllerBase
    {
        private IApiMappingCustomRequestContextService _apiMappingCustomRequestContextService;

        public API_7Controller(IApiMappingCustomRequestContextService apiMappingCustomRequestContextService)
        {
            _apiMappingCustomRequestContextService = apiMappingCustomRequestContextService;
        }


        [HttpGet("resource_1")]
        [ProducesResponseType(typeof(GenericEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> API_7_Resource_1()
        {
            const string thisResourceIdentifier = "api_7/resource_1";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            var successData = new GenericEntity($"Someone data from {thisResourceIdentifier}");

            return StatusCode(StatusCodes.Status200OK, successData);
        }

        [HttpGet("resource_2")]
        [ProducesResponseType(typeof(GenericEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> API_7_Resource_2()
        {
            const string thisResourceIdentifier = "api_7/resource_2";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            var successData = new GenericEntity($"Someone data from {thisResourceIdentifier}");

            return StatusCode(StatusCodes.Status200OK, successData);
        }

        [HttpGet("resource_3")]
        [ProducesResponseType(typeof(GenericEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> API_7_Resource_3()
        {
            const string thisResourceIdentifier = "api_7/resource_3";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            var successData = new GenericEntity($"Someone data from {thisResourceIdentifier}");

            return StatusCode(StatusCodes.Status200OK, successData);
        }

        [HttpGet("resource_4")]
        [ProducesResponseType(typeof(GenericEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> API_7_Resource_4()
        {
            const string thisResourceIdentifier = "api_7/resource_4";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            var successData = new GenericEntity($"Someone data from {thisResourceIdentifier}");

            return StatusCode(StatusCodes.Status200OK, successData);
        }

        [HttpGet("resource_5")]
        [ProducesResponseType(typeof(GenericEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> API_7_Resource_5()
        {
            const string thisResourceIdentifier = "api_7/resource_5";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            var successData = new GenericEntity($"Someone data from {thisResourceIdentifier}");

            return StatusCode(StatusCodes.Status200OK, successData);
        }
    }
}

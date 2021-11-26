using ApiMapping.Domain.Constants;
using ApiMapping.Domain.Interfaces.Services;
using ExampleProjectsCommomResources.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class API_1Controller : ControllerBase
    {
        private IApiMappingCustomRequestContextService _apiMappingCustomRequestContextService;

        public API_1Controller(IApiMappingCustomRequestContextService apiMappingCustomRequestContextService)
        {
            _apiMappingCustomRequestContextService = apiMappingCustomRequestContextService;
        }


        [HttpGet("resource_1")]
        [ProducesResponseType(typeof(GenericEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> API_1_Resource_1()
        {
            const string thisResourceIdentifier = "api_1/resource_1";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            var successData = new GenericEntity($"Someone data from {thisResourceIdentifier}");

            return StatusCode(StatusCodes.Status200OK, successData);
        }

        [HttpGet("resource_2")]
        [ProducesResponseType(typeof(GenericEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> API_1_Resource_2()
        {
            const string thisResourceIdentifier = "api_1/resource_2";
            _apiMappingCustomRequestContextService.AddOrUpdateCustomHeader(HeadersConstants.ConsumerApiResourceNameHeaderKey, thisResourceIdentifier);

            var successData = new GenericEntity($"Someone data from {thisResourceIdentifier}");

            return StatusCode(StatusCodes.Status200OK, successData);
        }
    }
}

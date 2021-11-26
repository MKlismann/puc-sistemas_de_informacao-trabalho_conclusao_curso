using ApiMapping.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace ApiMapping.Contexts.NetCoreContext.Services
{
    public class ApiMappingCustomRequestContextService : IApiMappingCustomRequestContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ApiMappingCustomRequestContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddOrUpdateCustomHeader(string name, string value)
        {
            string header = _httpContextAccessor.HttpContext.Request.Headers[name];
            if (string.IsNullOrWhiteSpace(header))
                _httpContextAccessor.HttpContext.Request.Headers.Add(name, value);
            else
                _httpContextAccessor.HttpContext.Request.Headers[name] = value;
        }
        public string GetHeaderValue(string name)
        {
            return _httpContextAccessor.HttpContext.Request.Headers[name];
        }
    }
}

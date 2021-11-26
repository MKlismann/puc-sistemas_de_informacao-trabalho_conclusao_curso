using Microsoft.AspNetCore.Http;

namespace ApiMapping.Domain.Interfaces.Services
{
    public interface IApiMappingCustomRequestContextService
    {
        void AddOrUpdateCustomHeader(string name, string value);
        string GetHeaderValue(string name);
    }
}

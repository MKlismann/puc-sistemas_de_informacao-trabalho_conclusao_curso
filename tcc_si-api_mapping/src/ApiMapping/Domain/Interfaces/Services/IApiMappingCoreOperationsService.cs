using ApiMapping.Domain.Entities.Infraestructure.Repositories.Data;
using Microsoft.AspNetCore.Http;

namespace ApiMapping.Domain.Interfaces.Services
{
    public interface IApiMappingCoreOperationsService
    {
        void CreateOrUpdateApi(ApiEntity apiEntity);
        void CreateOrUpdateApiDependency(
            HttpRequest httpRequest, 
            string consumedApiName);
    }
}

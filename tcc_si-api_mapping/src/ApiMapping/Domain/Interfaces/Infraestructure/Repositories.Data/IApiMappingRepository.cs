using ApiMapping.Domain.Entities.Infraestructure.Repositories.Data;
using System.Collections.Generic;

namespace ApiMapping.Domain.Interfaces.Infraestructure.Repositories.Data
{
    public interface IApiMappingRepository
    {
        ApiEntity GetApiEntity(string apiName);
        void CreateApiEntity(ApiEntity apiEntity);
        void UpdateApiEntity(ApiEntity apiEntity);        
        void CreateApiDependency(ApiDependencyEntity apiDependency);
        void UpdateApiDependency(ApiDependencyEntity apiDependency);
        ApiResourceDependencyEntity GetResourceDependency(string consumerApiResource, string consumedApiResource);
        void UpdateResourcesDependency(ApiResourceDependencyEntity apiResourceDependencyEntity);
        void CreateResourcesDependency(ApiResourceDependencyEntity apiResourceDependencyEntity);
        void UpdateApiResource(ApiResourceEntity apiResourceEntity);
        void CreateApiResource(ApiResourceEntity apiResourceEntity);
        IList<ApiEntity> ListApis();
        IList<ApiDependencyEntity> GetApiDependencies(string apiName);
        IList<ApiResourceEntity> GetResourcesFromApi(string apiName);
        IList<ApiResourceDependencyEntity> GetResourceDependencies(string resource, string apiName);
    }
}

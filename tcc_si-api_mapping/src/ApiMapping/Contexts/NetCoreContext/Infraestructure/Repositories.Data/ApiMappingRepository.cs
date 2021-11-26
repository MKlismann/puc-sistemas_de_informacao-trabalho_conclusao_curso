using ApiMapping.Contexts.NetCoreContext.Infraestructure.Repositories.Data.Contexts;
using ApiMapping.Domain.Entities.Infraestructure.Repositories.Data;
using ApiMapping.Domain.Interfaces.Infraestructure.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiMapping.Contexts.NetCoreContext.Infraestructure.Repositories.Data
{
    public class ApiMappingRepository : IApiMappingRepository
    {
        private ApiMappingDatabaseContext _apiMappingContext;

        public ApiMappingRepository(ApiMappingDatabaseContext apiMappingContext)
        {
            _apiMappingContext = apiMappingContext;
        }

        public IList<ApiEntity> ListApis()
        {
            return _apiMappingContext.Apis.ToList();
        }

        public ApiEntity GetApiEntity(string apiName)
        {
            var apis = _apiMappingContext.Apis;

            return (from api in apis
                    where api.Name == apiName
                    select api).SingleOrDefault();
        }

        public void CreateApiEntity(ApiEntity apiEntity)
        {
            apiEntity.Created = DateTime.Now;
            _apiMappingContext.Add<ApiEntity>(apiEntity);

            _apiMappingContext.SaveChanges();
        }

        public void UpdateApiEntity(ApiEntity apiEntity)
        {
            var apiFound = _apiMappingContext.Apis.Single<ApiEntity>(record => record.Name == apiEntity.Name);
            apiFound.Description = apiEntity.Description;
            apiFound.Updated = DateTime.Now;

            _apiMappingContext.SaveChanges();
        }

        public IList<ApiDependencyEntity> GetApiDependencies(string consumerApiName)
        {
            var dependencies = _apiMappingContext.ApisDependencies;

            return (from dependency in dependencies
                    where dependency.Consumer == consumerApiName
                    select dependency).ToList();
        }

        public void CreateApiDependency(ApiDependencyEntity apiDependency)
        {
            apiDependency.Created = DateTime.Now;
            _apiMappingContext.Add<ApiDependencyEntity>(apiDependency);

            _apiMappingContext.SaveChanges();
        }

        public void UpdateApiDependency(ApiDependencyEntity apiDependency)
        {
            var dependencyEntity = _apiMappingContext.ApisDependencies.Single<ApiDependencyEntity>(
                record =>
                record.Consumer == apiDependency.Consumer
                && record.Consumed == apiDependency.Consumed);

            dependencyEntity.Updated = DateTime.Now;

            _apiMappingContext.SaveChanges();
        }

        public ApiResourceEntity GetApiResource(string apiName, string resource)
        {
            var apis = _apiMappingContext.ApisResources;

            return (from api in apis
                    where api.Api_Name == apiName && api.Resource == resource
                    select api).SingleOrDefault();
        }

        public ApiResourceDependencyEntity GetResourceDependency(string consumerApiResource, string consumedApiResource)
        {
            var apis = _apiMappingContext.ApisResourcesDependencies;

            return (from api in apis
                    where api.Consumer_Resource == consumerApiResource
                    && api.Consumed_Resource == consumedApiResource
                    select api).SingleOrDefault();
        }

        public void UpdateResourcesDependency(ApiResourceDependencyEntity apiResourceDependencyEntity)
        {
            var dependencyEntity = _apiMappingContext.ApisResourcesDependencies.Single<ApiResourceDependencyEntity>(
                record =>
                record.Consumed_Api == apiResourceDependencyEntity.Consumed_Api
                && record.Consumed_Resource == apiResourceDependencyEntity.Consumed_Resource
                && record.Consumer_Api == apiResourceDependencyEntity.Consumer_Api
                && record.Consumer_Resource == apiResourceDependencyEntity.Consumer_Resource);

            dependencyEntity.Updated = DateTime.Now;

            _apiMappingContext.SaveChanges();
        }

        public void CreateResourcesDependency(ApiResourceDependencyEntity apiResourceDependencyEntity)
        {
            apiResourceDependencyEntity.Created = DateTime.Now;
            _apiMappingContext.Add<ApiResourceDependencyEntity>(apiResourceDependencyEntity);

            _apiMappingContext.SaveChanges();
        }

        public void UpdateApiResource(ApiResourceEntity apiResourceEntity)
        {
            var dependencyEntity = _apiMappingContext.ApisResources.Single<ApiResourceEntity>(
                record =>
                record.Api_Name == apiResourceEntity.Api_Name
                && record.Resource == apiResourceEntity.Resource);

            dependencyEntity.Updated = DateTime.Now;

            _apiMappingContext.SaveChanges();
        }

        public void CreateApiResource(ApiResourceEntity apiResourceEntity)
        {
            apiResourceEntity.Created = DateTime.Now;
            _apiMappingContext.Add<ApiResourceEntity>(apiResourceEntity);

            _apiMappingContext.SaveChanges();
        }

        public IList<ApiResourceEntity> GetResourcesFromApi(string apiName)
        {
            var resources = _apiMappingContext.ApisResources;

            return (from resource in resources
                    where resource.Api_Name == apiName
                    select resource).ToList();
        }

        public IList<ApiResourceDependencyEntity> GetResourceDependencies(string resource, string apiName)
        {
            var resourcesDependencies = _apiMappingContext.ApisResourcesDependencies;

            return (from resourceDependency in resourcesDependencies
                    where resourceDependency.Consumer_Resource == resource
                    && resourceDependency.Consumer_Api == apiName
                    select resourceDependency).ToList();
        }
    }
}

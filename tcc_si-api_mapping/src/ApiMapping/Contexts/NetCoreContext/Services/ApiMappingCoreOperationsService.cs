using ApiMapping.Domain.Constants;
using ApiMapping.Domain.Entities.Infraestructure.Repositories.Data;
using ApiMapping.Domain.Interfaces.Infraestructure.Repositories.Data;
using ApiMapping.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace ApiMapping.Contexts.NetCoreContext.Services
{
    public class ApiMappingCoreOperationsService : IApiMappingCoreOperationsService
    {
        private IApiMappingRepository _apiMappingCoreOperationsRepository;

        public ApiMappingCoreOperationsService(IApiMappingRepository apiMappingCoreOperationsRepository)
        {
            _apiMappingCoreOperationsRepository = apiMappingCoreOperationsRepository;
        }

        public void CreateOrUpdateApi(ApiEntity apiEntity)
        {
            if (apiEntity == null)
                throw new Exception("ApiEntity required to create or update Api.");

            var apiAlreadyExisting = GetApiEntity(apiEntity.Name);
            if (apiAlreadyExisting == null)
                _apiMappingCoreOperationsRepository.CreateApiEntity(apiEntity);
            else
                _apiMappingCoreOperationsRepository.UpdateApiEntity(apiEntity);
        }

        private ApiEntity GetApiEntity(string apiName)
        {
            if (string.IsNullOrWhiteSpace(apiName))
                throw new Exception("API Name required.");

            return _apiMappingCoreOperationsRepository.GetApiEntity(apiName);
        }
        public void CreateOrUpdateApiDependency(
            HttpRequest request, 
            string consumedApiName)
        {
            if (request == null)
                throw new Exception("HttpRequest data required.");

            if (string.IsNullOrWhiteSpace(consumedApiName))
                throw new Exception("Consumed API Name required.");

            var requestHeaders = request.Headers.ToDictionary(item => item.Key, item => item.Value.ToString());

            var consumerApiHeader = requestHeaders.SingleOrDefault(header => header.Key == HeadersConstants.ConsumerApiNameHeaderKey);
            if (!string.IsNullOrWhiteSpace(consumerApiHeader.Value))
            {
                var consumerApiName = consumerApiHeader.Value;

                RegisterDependency(
                    consumedApiName,
                    consumerApiName);

                var consumedApiResourceHeader = requestHeaders.SingleOrDefault(header => header.Key == HeadersConstants.ConsumedApiResourceNameHeaderKey);
                var consumerApiResourceHeader = requestHeaders.SingleOrDefault(header => header.Key == HeadersConstants.ConsumerApiResourceNameHeaderKey);

                if (!string.IsNullOrWhiteSpace(consumedApiResourceHeader.Value))
                    CreateOrUpdateApiResource(consumedApiName, consumedApiResourceHeader.Value);
                
                if (!string.IsNullOrWhiteSpace(consumerApiResourceHeader.Value))
                    CreateOrUpdateApiResource(consumerApiName, consumerApiResourceHeader.Value);

                if (!string.IsNullOrWhiteSpace(consumedApiResourceHeader.Value)
                    && !string.IsNullOrWhiteSpace(consumerApiResourceHeader.Value))
                {
                    RegisterResourcesDependency(consumedApiName,
                    consumerApiName,
                    consumedApiResourceHeader.Value,
                    consumerApiResourceHeader.Value);
                }
            }
        }

        private void RegisterResourcesDependency(string consumedApiName, string consumerApiName, string consumedApiResource, string consumerApiResource)
        {
            var alreadyExistingDependency = _apiMappingCoreOperationsRepository.GetResourceDependency(consumerApiResource, consumedApiResource);
            if (alreadyExistingDependency != null)
            {
                _apiMappingCoreOperationsRepository.UpdateResourcesDependency(
                   new ApiResourceDependencyEntity
                   {
                       Consumed_Api = consumedApiName,
                       Consumed_Resource = consumedApiResource,
                       Consumer_Api = consumerApiName,
                       Consumer_Resource = consumerApiResource
                   });
            }
            else
            {
                _apiMappingCoreOperationsRepository.CreateResourcesDependency(
                    new ApiResourceDependencyEntity
                    {
                        Consumed_Api = consumedApiName,
                        Consumed_Resource = consumedApiResource,
                        Consumer_Api = consumerApiName,
                        Consumer_Resource = consumerApiResource
                    });
            }
        }

        private void CreateOrUpdateApiResource(string apiName, string resourceIdentifier)
        {
            var apiResources = _apiMappingCoreOperationsRepository.GetResourcesFromApi(apiName);
            if (apiResources != null
                && apiResources.Any(resource =>
                    resource.Resource == resourceIdentifier
                    && resource.Api_Name == apiName))
            {
                _apiMappingCoreOperationsRepository.UpdateApiResource(
                   new ApiResourceEntity
                   {
                       Api_Name = apiName,
                       Resource = resourceIdentifier
                   });
            }
            else
            {
                _apiMappingCoreOperationsRepository.CreateApiResource(
                    new ApiResourceEntity
                    {
                        Api_Name = apiName,
                        Resource = resourceIdentifier
                    });
            }
        }
        private void RegisterDependency(
            string consumedApiName,
            string consumerApiName)
        {
            var consumerApiEntity = GetApiEntity(consumerApiName);
            if (consumerApiEntity == null)
                CreateOrUpdateApi(new ApiEntity { Name = consumerApiName });

            var consumedApiEntity = GetApiEntity(consumedApiName);
            if (consumedApiEntity == null)
                CreateOrUpdateApi(new ApiEntity { Name = consumedApiName });            

            var consumerApiDependencies = _apiMappingCoreOperationsRepository.GetApiDependencies(consumerApiName);
            if (consumerApiDependencies != null
                && consumerApiDependencies.Any(dependency =>
                    dependency.Consumed == consumedApiName
                    && dependency.Consumer == consumerApiName))
            {
                _apiMappingCoreOperationsRepository.UpdateApiDependency(
                   new ApiDependencyEntity
                   {
                       Consumed = consumedApiName,
                       Consumer = consumerApiName
                   });
            }
            else
            {
                _apiMappingCoreOperationsRepository.CreateApiDependency(
                    new ApiDependencyEntity
                    {
                        Consumed = consumedApiName,
                        Consumer = consumerApiName
                    });
            }
        }
    }
}

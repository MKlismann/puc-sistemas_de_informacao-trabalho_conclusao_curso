using ApiMapping.Domain.Entities.GraphContext;
using ApiMapping.Domain.Entities.Infraestructure.Repositories.Data;
using ApiMapping.Domain.Interfaces.Infraestructure.Repositories.Data;
using ApiMapping.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiMapping.Contexts.NetCoreContext.Services
{
    public class ApiMappingReportsOperationsService : IApiMappingReportsOperationsService
    {
        private IApiMappingRepository _apiMappingRepository;

        public ApiMappingReportsOperationsService(IApiMappingRepository apiMappingRepository)
        {
            _apiMappingRepository = apiMappingRepository;
        }

        public IList<Vertex> BuildCompleteGraphFromDatabase()
        {
            var graph = new List<Vertex>();

            var apis = _apiMappingRepository.ListApis();
            if (apis != null
                && apis.Count > 0)
            {
                foreach (var api in apis)
                {
                    var vertex = new Vertex(
                        api.Name,
                        api.Description,
                        api.Created,
                        api.Updated);

                    MapEachResourceOfEachApi(vertex);

                    graph.Add(vertex);
                }

                MapEachDirectDependencyOfEachApi(graph);

                MapEachResourceDependencyFromEachApi(graph);
            }

            return graph;
        }

        private void MapEachResourceOfEachApi(Vertex vertex)
        {
            var resourcesFromThisApi = _apiMappingRepository.GetResourcesFromApi(vertex.Name);
            if (resourcesFromThisApi != null
                && resourcesFromThisApi.Count > 0)
            {
                foreach (var resource in resourcesFromThisApi)
                {
                    var resourceDependency = new Resource(
                        vertex,
                        resource.Resource,
                        resource.Created,
                        resource.Updated);

                    vertex.AddResource(resourceDependency);
                }
            }
        }

        private void MapEachDirectDependencyOfEachApi(List<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                var vertexDependencies = _apiMappingRepository.GetApiDependencies(vertex.Name);
                if (vertexDependencies != null
                    && vertexDependencies.Count > 0)
                {
                    foreach (var vertexDependency in vertexDependencies)
                    {
                        var consumerApi = graph.Single(api => api.Name == vertexDependency.Consumer);
                        var consumedApi = graph.Single(api => api.Name == vertexDependency.Consumed);

                        var edge = new Edge(
                            consumerApi,
                            consumedApi,
                            vertexDependency.Created,
                            vertexDependency.Updated);

                        vertex.AddEdge(edge);
                    }
                }
            }
        }

        private void MapEachResourceDependencyFromEachApi(List<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                foreach (var resource in vertex.ResourcesFromThisApi)
                {
                    var dependenciesFromThisResource = _apiMappingRepository.GetResourceDependencies(resource.Identifier, vertex.Name);
                    MapResourceDependencies(graph, resource, resource.Identifier, vertex.Name, dependenciesFromThisResource);
                }
            }
        }

        private void MapResourceDependencies(
            IList<Vertex> graph,
            Resource resource,
            string resourceIdentifier,
            string apiName,
            IList<ApiResourceDependencyEntity> apiResourceDependencyEntity)
        {
            if (apiResourceDependencyEntity == null
                || apiResourceDependencyEntity.Count == 0)
            {
                return;
            }
            else
            {
                if (apiResourceDependencyEntity != null
                        && apiResourceDependencyEntity.Count > 0)
                {
                    foreach (var dependencyFromThisResource in apiResourceDependencyEntity)
                    {
                        var consumedResourceDependency = new Resource(
                            graph.Single(api => api.Name == dependencyFromThisResource.Consumed_Api),
                            dependencyFromThisResource.Consumed_Resource,
                            dependencyFromThisResource.Created,
                            dependencyFromThisResource.Updated);
                        resource.AddConsumedResource(consumedResourceDependency);

                        var dependenciesFromThisResource = _apiMappingRepository.GetResourceDependencies(
                            dependencyFromThisResource.Consumed_Resource,
                            dependencyFromThisResource.Consumed_Api);

                        MapResourceDependencies(
                            graph,
                            consumedResourceDependency,
                            dependencyFromThisResource.Consumed_Resource,
                            dependencyFromThisResource.Consumed_Api,
                            dependenciesFromThisResource);
                    }
                }
            }
        }

        public IList<Vertex> GetApisWithoutDependencies()
        {
            var graph = BuildCompleteGraphFromDatabase();

            var result = graph.Where(api => !api.Edges.Any()).ToList();

            return result;
        }

        public IList<Vertex> GetsSelfDependentApis()
        {
            var graph = BuildCompleteGraphFromDatabase();

            var result = graph.Where(api => api.Edges.Where(dependency => dependency.Consumed.Name == api.Name).Any()).ToList();

            return result;
        }

        public IList<Vertex> GetApisWithMoreThanAllowedDirectDependencies(ushort maximumNumberOfAllowedDependencies)
        {
            if (maximumNumberOfAllowedDependencies == 0)
                throw new Exception("MaximumNumberOfAllowedDependencies must be greather than 0.");

            var graph = BuildCompleteGraphFromDatabase();

            var result = graph.Where(api => api.Edges.Count > maximumNumberOfAllowedDependencies).ToList();

            return result;
        }

        public IList<Edge> GetOutdatedApiDependenciesBasedOnDate(DateTime earliestUpdateDate)
        {
            if (earliestUpdateDate > DateTime.Now)
                throw new Exception("EarliestUpdateDate must be earlier than today's date.");

            var graph = BuildCompleteGraphFromDatabase();

            var result = new List<Edge>();

            foreach (var vertex in graph)
            {
                var edges = vertex.Edges.Where(dependency => (dependency.Created < earliestUpdateDate && dependency.Updated == null) || (dependency.Updated < earliestUpdateDate)).ToList();
                result.AddRange(edges);
            }

            return result;
        }
    }
}

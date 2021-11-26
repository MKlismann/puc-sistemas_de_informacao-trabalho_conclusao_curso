using ApiMapping.Contexts.NetCoreContext.DependencyInjection;
using ApiMapping.Domain.Entities.GraphContext;
using ApiMapping.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportGeneratorApplication.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReportGeneratorApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");

                var configuration = builder.Build();

                var serviceProvider = new ServiceCollection()
                    .AddApiMappingDependencies(configuration)
                    .BuildServiceProvider();

                InterfaceConsoleApplication.WriteMessage("Generating API Dependencies report....", true, 0);

                var apiMappingReportsOperationsService = serviceProvider.GetService<IApiMappingReportsOperationsService>();

                var apisWithoutDependencies = apiMappingReportsOperationsService.GetApisWithoutDependencies();

                var selfDependentApis = apiMappingReportsOperationsService.GetsSelfDependentApis();

                ushort maximumNumberOfAllowedDirectDependencies = 2;
                var apisWithMoreThanAllowedDependencies = apiMappingReportsOperationsService.GetApisWithMoreThanAllowedDirectDependencies(maximumNumberOfAllowedDirectDependencies);

                var earliestUpdateDate = Convert.ToDateTime("2021/10/01 11:35:02.653");
                var withOutdatedDependencies = apiMappingReportsOperationsService.GetOutdatedApiDependenciesBasedOnDate(earliestUpdateDate);

                //Example of how to obtain the graph for custom processing
                var graph = apiMappingReportsOperationsService.BuildCompleteGraphFromDatabase();

                string apiName = "API_3";
                string resource = "api_3/resource_2";
                var completeDependencyMapFromResourceFromApi = graph.Single(api => api.Name == apiName)
                    .ResourcesFromThisApi.Single(resourceFromThisApi => resourceFromThisApi.Identifier == resource);

                InterfaceConsoleApplication.InsertBlankLine();
                InterfaceConsoleApplication.WriteMessage("APIs that have no dependencies (have no dependencies with other APIs, or have been registered but have not yet consumed their dependencies)", false, 1);
                foreach (var api in apisWithoutDependencies)
                {
                    var apiDescription = string.IsNullOrWhiteSpace(api.Description) ? " - 'API without description'" : $" - {api.Description}";
                    InterfaceConsoleApplication.WriteMessage($"{api.Name}{apiDescription}", false, 2);
                }

                InterfaceConsoleApplication.InsertBlankLine();
                InterfaceConsoleApplication.WriteMessage("APIs that depend on themselves (for whatever reason)", false, 1);
                foreach (var api in selfDependentApis)
                {
                    var apiDescription = string.IsNullOrWhiteSpace(api.Description) ? " - 'API without description'" : $" - {api.Description}";
                    InterfaceConsoleApplication.WriteMessage($"{api.Name}{apiDescription}", false, 2);
                }

                InterfaceConsoleApplication.InsertBlankLine();
                InterfaceConsoleApplication.WriteMessage($"APIs with many dependencies (more than allowed: {maximumNumberOfAllowedDirectDependencies})", false, 1);
                foreach (var api in apisWithMoreThanAllowedDependencies)
                {
                    var apiDescription = string.IsNullOrWhiteSpace(api.Description) ? " - 'API without description'" : $" - {api.Description}";
                    InterfaceConsoleApplication.WriteMessage($"{api.Name}{apiDescription}", false, 2);
                    foreach (var dependency in api.Edges)
                    {
                        var dependencyDescription = string.IsNullOrWhiteSpace(dependency.Consumed.Description) ? string.Empty : $" - {dependency.Consumed.Description}";
                        InterfaceConsoleApplication.WriteMessage($"{dependency.Consumed.Name}{dependencyDescription}", false, 3);
                    }
                }

                InterfaceConsoleApplication.InsertBlankLine();
                InterfaceConsoleApplication.WriteMessage("Outdated dependencies", false, 1);
                foreach (var dependency in withOutdatedDependencies)
                {
                    DateTime outdatedDate = dependency.Created;
                    if (dependency.Updated != null)
                        outdatedDate = dependency.Updated.Value;

                    InterfaceConsoleApplication.WriteMessage($"{dependency.Consumer.Name} -> {dependency.Consumed.Name} - Last use: {outdatedDate}", false, 2);
                }


                InterfaceConsoleApplication.InsertBlankLine();
                InterfaceConsoleApplication.WriteMessage($"Complete dependency map from resource '{resource}', from API '{apiName}'", false, 1);
                GoThroughResourceDependencies(completeDependencyMapFromResourceFromApi);


                InterfaceConsoleApplication.InsertBlankLine();
                InterfaceConsoleApplication.WriteMessage("Report generated.", true, 0);
            }
            catch (Exception exception)
            {
                InterfaceConsoleApplication.WriteMessage($"Error generating API Dependencies report: {exception.Message}.", true, 1);
            }
            finally
            {
                InterfaceConsoleApplication.InsertBlankLine();
                InterfaceConsoleApplication.WriteMessage("Press any key to exit...", false, 0);
                Console.ReadKey();
            }
        }

        private static void GoThroughResourceDependencies(Resource resource)
        {
            var message = $"-> {resource.FromApi.Name} ({resource.Identifier})";
            InterfaceConsoleApplication.WriteMessage(message, false, 2);

            if (resource.ConsumedResourcesByThisResource == null
                || resource.ConsumedResourcesByThisResource.Count == 0)
            { 
                return;
            }
            else
            {                
                foreach (var dependency in resource.ConsumedResourcesByThisResource)
                {
                    GoThroughResourceDependencies(dependency);
                }                
            }
        }
    }
}

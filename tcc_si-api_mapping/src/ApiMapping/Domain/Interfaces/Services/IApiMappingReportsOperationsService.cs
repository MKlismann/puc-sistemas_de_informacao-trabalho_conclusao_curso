using ApiMapping.Domain.Entities.GraphContext;
using System;
using System.Collections.Generic;

namespace ApiMapping.Domain.Interfaces.Services
{
    public interface IApiMappingReportsOperationsService
    {
        IList<Vertex> BuildCompleteGraphFromDatabase();
        IList<Vertex> GetApisWithoutDependencies();
        IList<Vertex> GetsSelfDependentApis();
        IList<Vertex> GetApisWithMoreThanAllowedDirectDependencies(ushort maximumNumberOfAllowedDirectDependencies);
        IList<Edge> GetOutdatedApiDependenciesBasedOnDate(DateTime earliestUpdateDate);
    }
}

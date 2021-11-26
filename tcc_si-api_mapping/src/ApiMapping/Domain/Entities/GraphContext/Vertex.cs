using System;
using System.Collections.Generic;

namespace ApiMapping.Domain.Entities.GraphContext
{
    public class Vertex
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Updated { get; private set; }
        public IList<Edge> Edges { get; private set; }
        public IList<Resource> ResourcesFromThisApi { get; private set; }

        public Vertex(
            string name, 
            string about, 
            DateTime created, 
            DateTime? updated)
        {
            Name = name;
            Description = about;
            Created = created;
            Updated = updated;
            Edges = new List<Edge>();
            ResourcesFromThisApi = new List<Resource>();
        }

        public void AddEdge(Edge edge)
        {
            if (Edges == null)
                Edges = new List<Edge>();

            Edges.Add(edge);
        }

        public void AddResource(Resource resource)
        {
            if (ResourcesFromThisApi == null)
                ResourcesFromThisApi = new List<Resource>();

            ResourcesFromThisApi.Add(resource);
        }
    }
}

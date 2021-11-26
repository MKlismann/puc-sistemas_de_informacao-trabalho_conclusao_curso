using System;
using System.Collections.Generic;

namespace ApiMapping.Domain.Entities.GraphContext
{
    public class Resource
    {
        public string Identifier { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Updated { get; private set; }
        public Vertex FromApi { get; private set; }
        public IList<Resource> ConsumedResourcesByThisResource { get; private set; }

        public Resource(
            Vertex fromApi,
            string identifier,
            DateTime created,
            DateTime? updated)
        {
            FromApi = fromApi;
            Identifier = identifier;
            Created = created;
            Updated = updated;
            ConsumedResourcesByThisResource = new List<Resource>();
        }

        public void AddConsumedResource(Resource consumedResource)
        {
            if (ConsumedResourcesByThisResource == null)
                ConsumedResourcesByThisResource = new List<Resource>();

            ConsumedResourcesByThisResource.Add(consumedResource);
        }
    }
}

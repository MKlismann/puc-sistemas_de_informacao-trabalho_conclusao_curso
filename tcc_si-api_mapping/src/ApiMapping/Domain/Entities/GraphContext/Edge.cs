using System;

namespace ApiMapping.Domain.Entities.GraphContext
{
    public class Edge
    {
        public Vertex Consumer { get; private set; }
        public Vertex Consumed { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Updated { get; private set; }

        public Edge( 
            Vertex consumer, 
            Vertex consumed, 
            DateTime created, 
            DateTime? updated)
        {
            Consumer = consumer;
            Consumed = consumed;
            Created = created;
            Updated = updated;
        }
    }
}

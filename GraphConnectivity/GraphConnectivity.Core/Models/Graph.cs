using System.Collections.Generic;
using System.Linq;

namespace GraphConnectivity.Core.Models
{
    internal class Graph<T>
    {
        public class Vertex<TValue>
        {
            public TValue Value { get; set; }
            public List<Edge<T,T>> AdjacentEdges { get; set; }

            public Vertex(TValue value)
            {
                Value = value;
            }
        }
        public class Edge<TFrom,TTo>
        {
            public Vertex<TFrom> From{ get; set; }
            public Vertex<TTo> To { get; set; }
            public int Value { get; set; }

            public Edge(Vertex<TFrom> from, Vertex<TTo> to, int value)
            {
                From = from;
                To = to;
                Value = value;
            }
        }

        public List<Vertex<T>> Vertices { get; set; }

        public Graph()
        {
            Vertices = new List<Vertex<T>>();
        }

        public override string ToString()
        {
            return Vertices.Aggregate("digraph{", (current1, vertex) => vertex.AdjacentEdges.Aggregate(current1, (current, edge) => current + vertex.Value.ToString() + " -> " + edge.To.Value.ToString()));
        }
    }
}

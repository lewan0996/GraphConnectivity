using System.Collections.Generic;

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
            var graphString = "digraph{";

            foreach (var vertex in Vertices)
            {
                foreach (var edge in vertex.AdjacentEdges)
                {
                    graphString += vertex.Value + " -> " + edge.To.Value;
                }
            }
            return graphString;
        }
    }
}

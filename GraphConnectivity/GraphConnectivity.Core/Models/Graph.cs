using System.Collections.Generic;
using System.Linq;

namespace GraphConnectivity.Core.Models
{
    public class Graph<T>
    {
        public class Vertex<TValue>
        {
            public TValue Value { get; set; }
            public List<Edge<T, T>> AdjacentEdges { get; set; }

            public Vertex(TValue value)
            {
                Value = value;
                AdjacentEdges = new List<Edge<T, T>>();
            }
        }
        public class Edge<TFrom, TTo>
        {
            public Vertex<TFrom> From { get; set; }
            public Vertex<TTo> To { get; set; }
            public int Value { get; set; }

            public Edge(Vertex<TFrom> from, Vertex<TTo> to, int value = 0)
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

        public void AddVertex(T value)
        {
            Vertex<T> vertex = new Vertex<T>(value);
            Vertices.Add(vertex);
        }

        public void RemoveVertexByValue(T value)
        {
            var vertexToRemove = Vertices.FirstOrDefault(v => v.Value.Equals(value));
            Vertices.Remove(vertexToRemove);

            foreach (var vertex in Vertices)
            {
                /*foreach (var edge in vertex.AdjacentEdges)
                {
                    if (edge.To == vertexToRemove)
                    {
                        vertex.AdjacentEdges.Remove(edge);
                    }
                }*/
                vertex.AdjacentEdges.RemoveAll(e => e.To.Value.Equals(value));
            }
        }

        public void AddEdge(Vertex<T> from, Vertex<T> to)
        {
            var edgeToAdd = new Edge<T, T>(from, to);
            from.AdjacentEdges.Add(edgeToAdd);
        }

        public void AddEdgeByValues(T from, T to)
        {
            var vertexFrom = Vertices.FirstOrDefault(v => v.Value.Equals(from));
            var vertexTo = Vertices.FirstOrDefault(v => v.Value.Equals(to));
            
            AddEdge(vertexFrom, vertexTo);
        }

        public void RemoveEdgeByValues(T from, T to)
        {
            var verticeFrom = Vertices.FirstOrDefault(v => v.Value.Equals(from));
            var edgeToRemove = verticeFrom.AdjacentEdges.FirstOrDefault(e => e.To.Value.Equals(to));

            verticeFrom.AdjacentEdges.Remove(edgeToRemove);
        }
        
        public bool CalculateConnectivity()
        {
            return Vertices.Count % 2 == 0;
        }

        public bool CalculateStrongConnectivity()
        {
            return !CalculateConnectivity();
        }
    }
}

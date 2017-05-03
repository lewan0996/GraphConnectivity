using System;
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
            public bool Visited { get; set; }
            public int Pre { get; set; }
            public int Post { get; set; }

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
            public bool IsRedundant { get; set; }

            public Edge(Vertex<TFrom> from, Vertex<TTo> to, int value = 0)
            {
                From = from;
                To = to;
                Value = value;
            }
        }

        public List<Vertex<T>> Vertices { get; set; }
        private int _counter;

        public Graph()
        {
            Vertices = new List<Vertex<T>>();
        }

        public Vertex<T> AddVertex(T value)
        {
            Vertex<T> vertex = new Vertex<T>(value);
            Vertices.Add(vertex);
            return vertex;
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

        private static void AddEdge(Vertex<T> from, Vertex<T> to)
        {
            if (from != null && to != null)
            {
                var edgeToAdd = new Edge<T, T>(from, to);
                from.AdjacentEdges.Add(edgeToAdd);
            }
        }

        public void AddEdgeByValues(T from, T to)
        {
            var vertexFrom = Vertices.FirstOrDefault(v => v.Value.Equals(from));
            var vertexTo = Vertices.FirstOrDefault(v => v.Value.Equals(to));

            if (vertexFrom == null)
            {
                vertexFrom = AddVertex(from);
            }
            if (vertexTo == null)
            {
                vertexTo = AddVertex(to);
            }

            AddEdge(vertexFrom, vertexTo);
        }

        public void RemoveEdgeByValues(T from, T to)
        {
            var verticeFrom = Vertices.FirstOrDefault(v => v.Value.Equals(from));
            if (verticeFrom == null)
            {
                return;
            }
            var edgeToRemove = verticeFrom.AdjacentEdges.FirstOrDefault(e => e.To.Value.Equals(to));

            verticeFrom.AdjacentEdges.Remove(edgeToRemove);
        }

        public int CalculateConnectivity()
        {
            _counter = 1;
            var componentCounter = 0;
            foreach (var vertex in Vertices)
            {
                vertex.Visited = false;
            }
            DuplicateAndReverseEdges();
            foreach(var vertex in Vertices)
            {
                if (!vertex.Visited)
                {
                    componentCounter++;
                    Explore(vertex);
                }
            }
            RemoveAllEdges(e => e.IsRedundant);
            return componentCounter;
        }

        private void DuplicateAndReverseEdges()
        {
            foreach (var vertex in Vertices)
            {
                for (var index = 0; index < vertex.AdjacentEdges.Count; index++)
                {
                    var edge = vertex.AdjacentEdges[index];
                    if (!edge.IsRedundant)
                    {
                        edge.To.AdjacentEdges.Add(new Edge<T, T>(edge.To, vertex) {IsRedundant = true});
                    }
                }
            }
        }

        private void RemoveAllEdges(Predicate<Edge<T, T>> match)
        {
            foreach (var vertex in Vertices)
            {
                vertex.AdjacentEdges.RemoveAll(match);
            }
        }

        private void Reverse()
        {
            DuplicateAndReverseEdges();
            RemoveAllEdges(e => !e.IsRedundant);

            foreach (var vertex in Vertices)
            {
                foreach (var edge in vertex.AdjacentEdges)
                {
                    edge.IsRedundant = false;
                }
            }
        }
        private void Explore(Vertex<T> v)
        {
            v.Visited = true;
            v.Pre = _counter;
            _counter++;

            foreach (var edge in v.AdjacentEdges)
            {
                if (!edge.To.Visited)
                {
                    Explore(edge.To);
                }
            }

            v.Post = _counter;
            _counter++;
        }

        public int CalculateStrongConnectivity()
        {
            Reverse();
            _counter = 1;
            foreach (var vertex in Vertices)
            {
                vertex.Visited = false;
            }

            foreach (var vertex in Vertices)
            {
                if (!vertex.Visited)
                {
                    Explore(vertex);
                }
            }

            Reverse();
            Vertices.Sort((v1, v2) => v2.Post.CompareTo(v1.Post));

            var componentCounter = 0;
            _counter = 1;
            foreach (var vertex in Vertices)
            {
                vertex.Visited = false;
            }

            foreach (var vertex in Vertices)
            {
                if (!vertex.Visited)
                {
                    componentCounter++;
                    Explore(vertex);
                }
            }
            return componentCounter;
        }

        public void Clear()
        {
            Vertices.Clear();
        }
    }
}

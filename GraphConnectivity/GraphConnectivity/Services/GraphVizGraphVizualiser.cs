using GraphConnectivity.Core.Models;
using GraphConnectivity.Core.Services;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;

namespace GraphConnectivity.Services
{
    internal class GraphVizGraphVizualiser : IGraphVizualiser
    {
        public byte[] VisualiseGraph<T>(Graph<T> graph)
        {
            var getStartProcessQuery = new GetStartProcessQuery();
            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);

            // GraphGeneration can be injected via the IGraphGeneration interface

            var wrapper = new GraphGeneration(getStartProcessQuery,
                getProcessStartInfoQuery,
                registerLayoutPluginCommand);

            return wrapper.GenerateGraph(GenerateGraphVizString(graph), Enums.GraphReturnType.Png);
        }

        private string GenerateGraphVizString<T>(Graph<T> graph)
        {
            var graphString = "digraph{";

            foreach (var vertex in graph.Vertices)
            {
                //if (vertex.AdjacentEdges.Count == 0)
                //{
                    graphString += $"{vertex.Value}[style=\"filled\" fillcolor=\"#{vertex.Color:X6}\"];";
                    //continue;
                    //  }
                foreach (var edge in vertex.AdjacentEdges)
                {
                    graphString += vertex.Value + " -> " + edge.To.Value + ";";
                }
            }
            graphString += "}";
            return graphString;
            //return "digraph{a[color=blue];b[color=red];a->b[color=\"#00FF00\"]}";
        }
    }
}

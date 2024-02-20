using PathFinder.Depth_first_search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Depth_first_search
{
    public class DfsMethod
    {
        static Graph<string> graph = new Graph<string>();

        public static void InitializeGraph()
        {
          

            // Add nodes for a 10x10 grid
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    graph.AddNode($"{i},{j}");
                }
            }

            // Add edges for adjacent nodes
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    if (i < 10)
                        graph.AddEdge($"{i},{j}", $"{i + 1},{j}"); // Right neighbor
                    if (j < 10)
                        graph.AddEdge($"{i},{j}", $"{i},{j + 1}"); // Bottom neighbor
                }
            }

          
            DfsNode<string> n = DFS<string>(graph.Nodes.Find(x => x.Data == "1,1"), graph.Nodes.Find(x => x.Data == "10,10"));

            List<DfsNode<string>> path = TrackPath<string>(n, graph.Nodes.Find(x => x.Data == "1,1"));

            foreach (DfsNode<string> pathNode in path)
            {
                Console.WriteLine(pathNode.Data);
            }

            Console.ReadLine();
        }
        private static DfsNode<T> DFS<T>(DfsNode<T> start, DfsNode<T> goal)
        {
            Stack<DfsEdge<T>> edges = new Stack<DfsEdge<T>>();
            edges.Push(new DfsEdge<T>(start, start));

            while (edges.Count > 0)
            {
                DfsEdge<T> edge = edges.Pop();

                if (!edge.To.Discovered)
                {
                    edge.To.Discovered = true;
                    edge.To.Parent = edge.From;
                }
                if (edge.To == goal)
                {
                    return edge.To;
                }

                foreach (DfsEdge<T> e in edge.To.Edges)
                {
                    if (!e.To.Discovered)
                    {
                        edges.Push(e);
                    }
                }
            }

            return null;
        }

        private static List<DfsNode<T>> TrackPath<T>(DfsNode<T> node, DfsNode<T> start)
        {
            List<DfsNode<T>> path = new List<DfsNode<T>>();

            while (!node.Equals(start))
            {
                path.Add(node);
                node = node.Parent;
            }

            path.Add(start);

            path.Reverse();

            return path;
        }


    }
}


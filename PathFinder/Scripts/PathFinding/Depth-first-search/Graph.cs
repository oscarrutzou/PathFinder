using PathFinder.Depth_first_search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Depth_first_search
{
    class Graph<T>
    {
        public List<DfsNode<T>> Nodes { get; private set; } = new List<DfsNode<T>>();

        public void AddNode(T data)
        {
            Nodes.Add(new DfsNode<T>(data));
        }

        public void AddDirectionalEdge(T from, T to)
        {
            DfsNode<T> fromNode = Nodes.Find(x => x.Data.Equals(from));

            DfsNode<T> toNode = Nodes.Find(x => x.Data.Equals(to));

            if (!fromNode.Equals(default(T)) && !toNode.Equals(default(T)))
            {
                fromNode.AddEdge(toNode);
            }
            else
            {
                Console.WriteLine("Node not found!");
            }
        }

        public void AddEdge(T from, T to)
        {
            DfsNode<T> fromNode = Nodes.Find(x => x.Data.Equals(from));

            DfsNode<T> toNode = Nodes.Find(x => x.Data.Equals(to));

            if (!fromNode.Equals(default(T)) && !toNode.Equals(default(T)))
            {
                fromNode.AddEdge(toNode);
                toNode.AddEdge(fromNode);
            }
            else
            {
                Console.WriteLine("Node not found!");
            }



        }
    }
}

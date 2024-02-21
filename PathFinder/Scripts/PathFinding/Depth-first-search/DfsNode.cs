using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Depth_first_search
{
     class DfsNode<T>
    {
        public T Data { get; private set; }

        public List<DfsEdge<T>> Edges { get; private set; } = new List<DfsEdge<T>>();

        public bool Discovered { get; set; } = false;

        public DfsNode<T> Parent { get; set; }

        public DfsNode(T data)
        {
            this.Data = data;
        }

        public void AddEdge(DfsNode<T> other)
        {
            Edges.Add(new DfsEdge<T>(this, other));
        }
    }
}

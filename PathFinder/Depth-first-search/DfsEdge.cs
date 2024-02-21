using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Depth_first_search
{
    internal class DfsEdge<T>
    {
        public DfsNode<T> From { get; private set; }

        public DfsNode<T> To { get; private set; }

        public DfsEdge(DfsNode<T> from, DfsNode<T> to)
        {
            this.From = from;
            this.To = to;
        }
    }
}

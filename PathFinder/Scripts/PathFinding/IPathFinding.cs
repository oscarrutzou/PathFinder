using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PathFinder
{
    interface IPathFinding
    {
        public void Initialize(Grid grid);
        public List<Cell> FindPath(Point start, Point goal);
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PathFinder
{
    public class DFS: IPathFinding
    {
        private Grid grid;
        private Color pathColor = Color.Red;
        private Color searchedColor = Color.Pink;

        public void Initialize(Grid grid)
        {
            this.grid = grid;
        }

        public List<Cell> FindPath(Point startPoint, Point goalPoint)
        {
            ResetCells();

            Cell start = grid.GetCellFromPoint(startPoint);
            Cell goal = grid.GetCellFromPoint(goalPoint);
            Stack<Cell> stack = new Stack<Cell>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                Cell current = stack.Pop();
                current.Discovered = true;

                if (current == goal)
                {
                    return TrackPath(start, current);
                }

                foreach (Cell neighbor in current.Edges)
                {
                    if (!neighbor.Discovered && neighbor.isValid)
                    {
                        stack.Push(neighbor);
                        neighbor.Parent = current;
                        neighbor.color = searchedColor;
                    }
                }
            }

            return null; //Cant find path
        }
        private void ResetCells()
        {
            foreach (Cell cell in grid.Cells.Values)
            {
                cell.color = cell.baseColor;
                cell.Discovered = false;
                cell.Parent = null;
            }
        }

        private List<Cell> TrackPath(Cell start, Cell goal)
        {
            List<Cell> path = new List<Cell>();
            Cell current = goal;

            while (current != start)
            {
                path.Add(current);
                current = current.Parent;
            }

            path.Add(start);
            path.Reverse();

            foreach (Cell cell in path)
            {
                cell.color = pathColor;
            }

            return path;
        }

    }

}


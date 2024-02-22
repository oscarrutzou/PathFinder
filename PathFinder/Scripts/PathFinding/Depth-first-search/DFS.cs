using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PathFinder
{
    public class DFS
    {
        private Grid grid;
        private Color pathColor = DebugVariables.pathColor;
        private Color searchedColor = DebugVariables.searchedColor;

        public void Initialize(Grid grid)
        {
            this.grid = grid;
        }

        public List<Cell> FindPath(Point startPoint, Point goalPoint)
        {
            ResetCells(); //Set

            Cell start = grid.GetCellFromPoint(startPoint);
            Cell goal = grid.GetCellFromPoint(goalPoint);
            Stack<Cell> stack = new Stack<Cell>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                Cell current = stack.Pop(); //Get the new current cell
                current.Discovered = true; //Since we have checked it, the variable is true

                if (current == goal)
                {
                    //Return a list after going though the cells parents
                    return TrackPath(start, current); //Color the path red
                }

                //Edges is limited to only cells that are valid at the start of the class
                //But if there is any change in a cell, we still need to check if the neighbor isValid
                foreach (Cell neighbor in current.Edges)
                {
                    if (!neighbor.Discovered && neighbor.isValid)
                    {
                        stack.Push(neighbor);
                        neighbor.Parent = current;
                        neighbor.color = searchedColor; //To show path
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


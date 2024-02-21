using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
/*
    1. Placer start noden på den åbne liste
    2. Gentag følgende
        a) Find noden med den laveste F værdi på den åbne liste (den valgte node)
        b) Flyt noden til den lukkede liste
        c) Gør følgende for alle nabonoder til den valgte node
            • Hvis noden ikke er walkable eller er på den lukkede liste, ignoreres den
            • Hvis den ikke er på den åbne liste tilføjes den til listen. Den valgte node gøres til parent og F,G og H værdierne beregnes
            • Hvis noden allerede er på listen kontrolleres det om stien gennem den nuværende node er bedre, ved at sammenligne G
            værdierne. Hvis G er lavere er stien bedre. Hvis dette er tilfældet ændres nodens parent til den nuværende node, G,H og F
            scores genberegnes
        d) Søgningen stoppes når
            • Målet er tilføjet til den lukkede liste dvs. at en sti blev fundet
            • Den åbne liste er tom dvs. at der ikke blev fundet en sti
    3. Stien findes ved at arbejde sig tilbage fra målet gennem parent noder til startknuden er nået
    */
    public class Astar: IPathFinding
    {
        private Dictionary<Point, Cell> cells;
        private Grid grid;
        private int gridDem;
        private HashSet<Cell> open;
        private HashSet<Cell> closed;
        private Color pathColor = Color.Red;
        private Color searchedColor = Color.Pink;

        public void Initialize(Grid grid)
        {
            this.cells = grid.Cells; // Assign existing grid
            this.grid = grid;
            gridDem = Cell.demension;
        }


        public List<Cell> FindPath(Point start, Point goal)
        {
            ResetCells();

            open = new HashSet<Cell>();
            closed = new HashSet<Cell>();
            if (!cells.ContainsKey(start) || !cells.ContainsKey(goal))
            {
                return null;
            }

            open.Add(cells[start]);

            while (open.Count > 0)
            {
                Cell curCell = open.First();
                foreach (Cell cell in open)
                {
                    //
                    if (cell.F < curCell.F || cell.F == curCell.F && cell.H < curCell.H)
                    {
                        curCell = cell;
                    }
                }
                open.Remove(curCell);
                closed.Add(curCell); //Check is complete

                if (curCell.gridPosition.X == goal.X && curCell.gridPosition.Y == goal.Y)
                {
                    //path found!
                    return RetracePath(cells[start], cells[goal]);
                }


                List<Cell> neighbors = GetNeighbors(curCell.gridPosition);

                foreach (Cell neighbor in neighbors)
                {
                    if (closed.Contains(neighbor)) continue;
                    int newMovementCostToNeighbor = curCell.G + curCell.cost + GetDistance(curCell.gridPosition, neighbor.gridPosition);
                    if (newMovementCostToNeighbor < neighbor.G || !open.Contains(neighbor))
                    {
                        neighbor.G = newMovementCostToNeighbor;
                        //calulate h using manhatten principle
                        neighbor.H = ((Math.Abs(neighbor.gridPosition.X - goal.X) + Math.Abs(goal.Y - neighbor.gridPosition.Y)) * 10);
                        /*
                        Can also use the Euclidean distance to get the H
                        float first = Math.Abs(end.gridPosition.X - next.gridPosition.X);
                        float second = Math.Abs(end.gridPosition.Y - next.gridPosition.Y);
                        float priority = newCost + (float)Math.Sqrt(Math.Pow(first, 2) + Math.Pow(second, 2)); // Euclidean distance
                        */

                        neighbor.Parent = curCell;

                        if (!open.Contains(neighbor))
                        {
                            open.Add(neighbor);
                        }
                    }
                }
            }

            return null;
        }


        private List<Cell> RetracePath(Cell startPoint, Cell endPoint)
        {
            List<Cell> path = new List<Cell>();
            Cell currentNode = endPoint;

            while (currentNode != startPoint)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Add(startPoint);
            path.Reverse();

            foreach (Cell cell in path)
            {
                cell.color = pathColor;
            }

            return path;
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


        private int GetDistance(Point neighborgridPosition, Point endPoint)
        {
            int dstX = Math.Abs(neighborgridPosition.X - endPoint.X);
            int dstY = Math.Abs(neighborgridPosition.Y - endPoint.Y);

            if (dstX > dstY)
            {
                return 14 * dstY + 10 * (dstX - dstY);
            }
            return 14 * dstX + 10 * (dstY - dstX);
        }

        private List<Cell> GetNeighbors(Point point)
        {
            List<Cell> temp = new List<Cell>();
            List<(int dx, int dy)> directions = new List<(int, int)>
            {
                (-1, 0), // Left
                (1, 0),  // Right
                (0, 1),  // Down
                (0, -1), // Up
                (-1, 1), // Left + Down
                (-1, -1), // Left + Up
                (1, 1), // Right + Down
                (1, -1) // Right + Up
            };
            foreach (var direction in directions)
            {
                int nx = point.X + direction.dx;
                int ny = point.Y + direction.dy;
                Point newPoint = new Point(nx, ny);

                //If the direction isn't in the grid or the point doesn't exist in the grid
                if (!(nx >= 0 && nx < gridDem && ny >= 0 && ny < gridDem) || !cells.ContainsKey(newPoint)) continue;

                if (!cells[newPoint].isValid) continue;

                //Check if the cell is diagonally adjacent
                if (Math.Abs(point.X - nx) == 1 && Math.Abs(point.Y - ny) == 1)
                {
                    // Check the cells directly to each side
                    Point sidePoint1 = new Point(point.X, ny);
                    Point sidePoint2 = new Point(nx, point.Y);

                    if (!cells.ContainsKey(sidePoint1) || !cells.ContainsKey(sidePoint2)) continue;

                    if (!cells[sidePoint1].isValid || !cells[sidePoint2].isValid) continue;
                }

                temp.Add(cells[newPoint]);
                cells[newPoint].color = searchedColor;
            }
            return temp;
        }

    }
}

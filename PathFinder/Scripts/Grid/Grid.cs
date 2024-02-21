using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class Grid
    {

        public int width { get; private set; } = 19;
        public int height { get; private set; } = 9;

        public Vector2 startPostion { get; private set; }
               
        //Point = grid number
        public Dictionary<Point, Cell> Cells { get; private set; } = new Dictionary<Point, Cell>();

        private Color colorGrass = new Color(60, 80, 120, 100);
        private Color colorRoad = new Color(110, 70, 133, 100);
        private Color colorEmpty = new Color(0, 0, 0, 0);
        private Decoration map;
        private int mapW = 23;
        private int mapH = 13;
        private bool isCentered = true;
        private int demension = Cell.demension;

        public Astar astar = new Astar();
        public DFS dfs = new DFS();

        public void LoadGrid(Vector2 startpos)
        {
            
            if (isCentered)
            {
                startpos = new Vector2(startpos.X - mapW * demension / 2, startpos.Y - mapH * demension / 2);
            }

            map = new Decoration(TextureNames.Map, startpos);
            map.layerDepth = 0f;
            SceneData.gameObjectsToAdd.Add(map);

            this.startPostion = startpos + new Vector2(2 * demension, demension);

            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Point point = new Point(x, y);
                    Cell newCell = new Cell(this, point);
                    CheckCell(newCell, point); //Change cost and is_Valid
                    Cells.Add(point, newCell);
                    SceneData.gameObjectsToAdd.Add(newCell);
                }
            }
            
            foreach(Cell cell in Cells.Values)
    {
                Point point = cell.gridPosition;
                Point[] directions = new Point[]
                {
                    new Point(point.X - 1, point.Y), // Left
                    new Point(point.X + 1, point.Y), // Right
                    new Point(point.X, point.Y - 1), // Top
                    new Point(point.X, point.Y + 1)  // Bottom
                };

                foreach (Point direction in directions)
                {
                    if (Cells.ContainsKey(direction) && Cells[direction].isValid)
                    {
                        cell.AddEdge(Cells[direction]);
                    }
                }
            }

            astar.Initialize(this);
            dfs.Initialize(this);
            ShowHideGrid();
        }


        private void CheckCell(Cell cell, Point point)
        {        
            if (grassPositions.Contains(point))
            {
                cell.color = colorGrass;
                cell.baseColor = cell.color;
                cell.cost = 10;

            }else if (roadPostions.Contains(point))
            {
                cell.color = colorRoad;
                cell.baseColor = cell.color;
            }
            else
            {
                cell.color = colorEmpty;
                cell.baseColor = cell.color;
                cell.isValid = false;
            }
        }

        public void ChangeCellToEmpty(Point point)
        {
            Cell cell = Cells[point];
            cell.color = colorEmpty;
            cell.baseColor= cell.color;
            cell.isValid = false;
        }

        public Cell GetCell(Vector2 pos)
        {
            if (pos.X < startPostion.X || pos.Y < startPostion.Y)
            {
                return null; // Position is negative, otherwise it will make a invisable tile in the debug, since it cast to int, then it gets rounded to 0 and results in row and column
            }

            int gridX = (int)((pos.X - startPostion.X) / Cell.demension);
            int gridY = (int)((pos.Y - startPostion.Y) / Cell.demension);

            if (0 <= gridX && gridX < width && 0 <= gridY && gridY < height)
            {
                return Cells[new Point(gridX, gridY)];
            }

            return null; // Position is out of bounds
        }

        public Vector2 PosFromGridPos(Point point) => Cells[point].position;

        public Cell GetCellFromPoint(Point point) => GetCell(PosFromGridPos(point));

        public void ShowHideGrid()
        {
            foreach (Cell cell in Cells.Values)
            {
                cell.isVisible = !cell.isVisible;
            }
        }

        //Kunne have brugt en måde på at gemme vores grid, som jeg "Oscar" har gjordt før.
        //Vidste dog ikke om vi havde nok tid, og derfor har jeg lavet en copi af hvordan vores grid skulle se ud inde på mit projekt hvor jeg har et grid med save load system.
        //Herefter tog jeg alle positionerne som var anderledes slags, for at vælge hvilke positioner skal have hvilke værdier.
        //I forhold til at dette er et simpelt projekt, ville jeg ikke bruge det extra tid på at lave en bedre måde, da det ville tage for lang tid.
        #region Hard coded positions
        HashSet<Point> grassPositions = new HashSet<Point>
        {
            new Point(0, 6),
            new Point(1, 6),
            new Point(0, 7),
            new Point(2, 7),
            new Point(3, 7),
            new Point(4, 7),
            new Point(5, 6),
            new Point(4, 4),
            new Point(5, 4),
            new Point(6, 4),
            new Point(8, 4),
            new Point(9, 4),
            new Point(10, 4),
            new Point(11, 4),
            new Point(9, 6),
            new Point(11, 5),
            new Point(9, 0),
            new Point(10, 0),
            new Point(11, 0),
            new Point(9, 1),
            new Point(10, 1),
            new Point(11, 1),
            new Point(9, 2),
            new Point(10, 2),
            new Point(11, 2),
            new Point(9, 3),
            new Point(11, 3),
            new Point(18, 0),
            new Point(17, 1),
            new Point(17, 2),
            new Point(17, 3),
            new Point(17, 4),
            new Point(17, 5),
            new Point(17, 6),
            new Point(17, 7),
            new Point(18, 1),
            new Point(18, 2),
            new Point(18, 3),
            new Point(18, 4),
            new Point(18, 6),
            new Point(18, 7),
            new Point(18, 8)
        };

        HashSet<Point> roadPostions = new HashSet<Point>
        {
            new Point(2, 6),
            new Point(3, 6),
            new Point(4, 6),
            new Point(4, 5),
            new Point(5, 5),
            new Point(6, 5),
            new Point(7, 5),
            new Point(8, 5),
            new Point(9, 5),
            new Point(10, 5),
            new Point(10, 6),
            new Point(10, 7),
            new Point(10, 8),
            new Point(11, 8),
            new Point(12, 8),
            new Point(13, 8),
            new Point(14, 8),
            new Point(15, 8),
            new Point(16, 0),
            new Point(16, 1),
            new Point(16, 2),
            new Point(16, 3),
            new Point(16, 4),
            new Point(16, 5),
            new Point(16, 6),
            new Point(16, 7),
            new Point(16, 8),
            new Point(12, 0),
            new Point(13, 0),
            new Point(14, 0),
            new Point(15, 0),
            new Point(12, 1),
            new Point(12, 2),
            new Point(12, 3),
            new Point(12, 4),
            new Point(12, 5),
            new Point(12, 6),
            new Point(11, 6)
        };
        #endregion
    }
}

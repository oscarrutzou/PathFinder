using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace PathFinder
{
    public class Cell: GameObject
    {

        public int G;

        public int H;
        public int cost = 1;
        public int F => G + H;
        public Cell Parent { get; set; }

        public static int demension = 64;
        public static int scaleSize = 4;
        public bool isValid = true;

        public Point gridPosition;
        private Grid grid;
        public Color baseColor;

        //DFS
        public List<Cell> Edges { get; private set; } = new List<Cell>();
        public bool Discovered { get; set; } = false;

        public Cell(Grid grid, Point point)
        {            
            this.grid = grid;
            this.gridPosition = point;
            this.position = grid.startPostion + new Vector2(gridPosition.X * demension, gridPosition.Y * demension);
            texture = GlobalTextures.textures[TextureNames.Tile16x16];
            scale = scaleSize;
            layerDepth = 0.3f;
        }

        public void Reset()
        {
            Parent = null;
            G = 0;
            H = 0;
        }

        public override void Draw()
        {
            if (!isVisible) return;
            base.Draw();

            DrawDebugCollisionBox(Color.Black);
        }

        /// <summary>
        /// For DFS
        /// </summary>
        /// <param name="other"></param>
        public void AddEdge(Cell other)
        {
            Edges.Add(other);
        }
    }
}

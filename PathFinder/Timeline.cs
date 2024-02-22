using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathFinder
{
    public class Timeline
    {

        private Thread timeLine;
        private Grid grid;
        private Decoration snake;
        private Decoration key1, key2;
        public Player player;
        public List<Cell> path;
        public bool pathFindingWithAstar = true; //False == DFS

        public void StartThread(Grid grid)
        {
            this.grid = grid;
            timeLine = new Thread(TimeLine) { IsBackground = true };
            timeLine.Start();
        }

        private void TimeLine()
        {
            SpawnWizard(new Point(10, 8));
            Thread.Sleep(2000);
            MoveToPoint(new Point(2, 6));
            player.onGoalReached = () => { };

            while (player.onGoalReached != null) //Prevent high CPU usage
                Thread.Sleep(100);

            Thread.Sleep(2000);
            MoveToPoint(new Point(16, 8));
            player.onGoalReached = () => { };

            // wait till player.onGoalReached == null

        }

        private void SpawnWizard(Point point)
        {
            player = new Player(grid, point);
            SceneData.gameObjectsToAdd.Add(player);
        }

        private void MoveToPoint(Point point)
        {
            // We give our path finding a couple of tries to find one, since we have had some occasions where it was buggy
            // and only found the path after some tries.
            for (int i = 0; i < 3; i++)
            {
                if (path != null && path.Count > 0) break;

                if (pathFindingWithAstar)
                {
                    path = grid.astar.FindPath(player.gridPosition, point);
                }
                else
                {
                    path = grid.dfs.FindPath(player.gridPosition, point);
                }
            }
            if (path == null) throw new Exception("Failed to find a path");

            player.SetPlayerPath(path);
        }

        private void PickUpKey()
        {

        }

        private void PickUpPotion()
        {

        }

        private void SpawnSnake()
        {
            grid.ChangeCellToEmpty(new Point(15, 8));
            snake = new Decoration(AnimNames.Snake, grid.PosFromGridPos(new Point(15, 8)) - new Vector2(0, 10));
            snake.spriteEffects = SpriteEffects.FlipHorizontally;
            snake.animation.shouldPlay = false;
            //snake.animation.isLooping = false;
            //snake.animation.frameRate = 6f;
            SceneData.gameObjectsToAdd.Add(snake);
        }
        private void KeyPosition()
        {
            Random random = new Random();
            List<Cell> cells = SceneData.cells.Where(x => x.isValid && x.cost > 1).ToList();
            Cell cell1 = cells[random.Next(cells.Count - 1)];
            cells.Remove(cell1);
            Cell cell2 = cells[random.Next(cells.Count - 1)];
            cells.Remove(cell2);

            key1 = new Decoration(TextureNames.Key, cell1.position);
            SceneData.gameObjectsToAdd.Add(key1);

            key2 = new Decoration(TextureNames.Key, cell2.position);
            SceneData.gameObjectsToAdd.Add(key2);


        }

        //  Choose.Algoritme

        //  SpawnWizard

        //    Wait

        //    Move.Wizard

        //    Pick.Up.Key

        //    Wait

        //    Move.Wizard

        //    Pick.Up.Potion

        //    Wait

        //    Move.Wizard

        //    Pick.Up.Key

        //    Wait

        //    Move.To.Castle

        //    Wait

        //    Move.To.Portal

        //    Wait

        //    You Win

        //    Wait

        //    Restart

    }
}





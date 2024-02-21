using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    internal class Timeline
    {

        private Thread timeLine;
        private Grid grid;
        private Decoration snake;
        public void StartThread(Grid grid)
        {
            this.grid = grid;
            timeLine = new Thread(TimeLine) { IsBackground = true };
            timeLine.Start();
        }

        private void TimeLine()
        {
            Thread.Sleep(2000);
            grid.ChangeCellToEmpty(new Point(15, 8));
            snake = new Decoration(AnimNames.Snake, grid.PosFromGridPos(new Point(15, 8)) - new Vector2(0, 10), 4);
            snake.spriteEffects = SpriteEffects.FlipHorizontally;
            snake.animation.shouldPlay = false;
            //snake.animation.isLooping = false;
            //snake.animation.frameRate = 6f;
            SceneData.gameObjectsToAdd.Add(snake);

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

        private void SpawnWizard(Point point)
        {

        }

        private void MoveWizard(Point point)
        {
            MoveWizard(point);
        }

        private void PickUpKey()
        {

        }

        private void PickUpPotion()
        {

        }


    }
}





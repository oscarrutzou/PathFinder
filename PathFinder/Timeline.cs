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
        private Cell keyCell1, keyCell2;
        private Decoration chest;

        private DialogueBox dialogueBox;

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
            SpawnDialogueBox();
            SpawnChest();

            SpawnWizard(new Point(10, 8));
            ChangeDialogueText("Erik is drunk again and has accidentally teleported himself away from home. \nHelp him get home safely");
            Thread.Sleep(2000);
            //Skriv her Spawned wizard

            // Potion
            MoveToPoint(new Point(2, 6));
            player.onGoalReached = () => { };
            WaitForGoalReached(2);
            ChangeDialogueText("Erik has picked up another Beer, help him find a dumpster for the empty can");
            

            // Deposit in checst
            MoveToPoint(new Point(16, 8));
            player.onGoalReached = () => { chest.animation.shouldPlay = true; };
            WaitForGoalReached(0);
            ChangeDialogueText("Uhm, I guess that works too");
            Thread.Sleep(2000);

            // Back to start
            MoveToPoint(new Point(10,8));
            player.onGoalReached = () => { };
            ChangeDialogueText("Erik is once again wandering around aimlessly. It looks like he lost his keys");
            WaitForGoalReached(2);

            // Spawn snake
            SpawnSnake();

            // Spawn keys
            SpawnKeys();

            //Walk to key 1
            MoveToPoint(keyCell1.gridPosition);
            player.onGoalReached = () => { key1.isRemoved = true; };
            WaitForGoalReached(2);
            ChangeDialogueText("Erik has found a key. I bet he managed to lose his spare key too though");

            //Walk to key 2
            MoveToPoint(keyCell2.gridPosition);
            player.onGoalReached = () => { key2.isRemoved = true; };
            WaitForGoalReached(2);
            ChangeDialogueText("It looks like Erik has somehow managed to retrieve his spare key aswell");

            // Walk to tower
            MoveToPoint(new Point(2, 6));
            player.onGoalReached = () => { };
            WaitForGoalReached(2);

            Thread.Sleep(2000);
            Application.Restart();
        }

        private void WaitForGoalReached(int timeSec)
        {
            while (player.onGoalReached != null) //Prevent high CPU usage
                Thread.Sleep(100);
            Thread.Sleep(timeSec * 1000);
        }

        private void SpawnWizard(Point point)
        {
            player = new Player(grid, point);
            SceneData.gameObjectsToAdd.Add(player);
        }
        private void SpawnDialogueBox()
        {
            dialogueBox = new DialogueBox(GameWorld.Instance.uiCam.BottomCenter + new Vector2(0, -100), TextureNames.DialogueBox, "");
            dialogueBox.scale = 1;
            SceneData.gameObjectsToAdd.Add(dialogueBox);
        }
        private void ChangeDialogueText(string text)
        {
            dialogueBox.text = text;

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




        private void SpawnChest()
        {
            chest = new Decoration(AnimNames.ChestOpen, grid.PosFromGridPos(new Point(17, 8)));
            chest.animation.shouldPlay = false;
            SceneData.gameObjectsToAdd.Add(chest);
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
        private void SpawnKeys()
        {
            Random random = new Random();
            List<Cell> cells = SceneData.cells.Where(x => x.isValid && x.cost > 1).ToList(); 

            keyCell1 = cells[random.Next(cells.Count - 1)];
            cells.Remove(keyCell1);
            keyCell2 = cells[random.Next(cells.Count - 1)];
            cells.Remove(keyCell2);

            key1 = new Decoration(TextureNames.Key, keyCell1.position);
            SceneData.gameObjectsToAdd.Add(key1);

            key2 = new Decoration(TextureNames.Key, keyCell2.position);
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





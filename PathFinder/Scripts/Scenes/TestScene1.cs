using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class TestScene1 : Scene
    {
        Button btn;
        public Grid grid;
        private Decoration chest;
        private Decoration snake;
        private Decoration key1, key2;
        private bool initAfterObjectsAdded;

        public override void Initialize()
        {
            btn = new Button(GameWorld.Instance.uiCam.BottomCenter, TextureNames.StaticButton, "Hallo\nthis is a test", () => { });
            btn.SetCollisionBox(30, 20);
            SceneData.gameObjectsToAdd.Add(btn);
            grid = new Grid();
            grid.LoadGrid(Vector2.Zero);

            PlaceDecorationsTest();

        }

        private void PlaceDecorationsTest()
        {
            chest = new Decoration(AnimNames.ChestOpen, grid.PosFromGridPos(new Point(17, 8)), 4);
            chest.animation.shouldPlay = false;
            SceneData.gameObjectsToAdd.Add(chest);

            grid.ChangeCellToEmpty(new Point(15, 8));
            snake = new Decoration(AnimNames.Snake, grid.PosFromGridPos(new Point(15, 8)) - new Vector2(0, 10), 4);
            snake.spriteEffects = SpriteEffects.FlipHorizontally;                
            snake.animation.shouldPlay = false;
            //snake.animation.isLooping = false;
            //snake.animation.frameRate = 6f;
            SceneData.gameObjectsToAdd.Add(snake);

            //Test til punkt 10,1 på grid
            key1 = new Decoration(TextureNames.Key, grid.PosFromGridPos(new Point(10, 1)), 4);
            SceneData.gameObjectsToAdd.Add(key1);

            key2 = new Decoration(TextureNames.Key, grid.PosFromGridPos(new Point(17, 3)), 4);
            SceneData.gameObjectsToAdd.Add(key2);
        }

        public override void DrawOnScreen()
        {
            base.DrawOnScreen();

            if (InputManager.debugStats) DebugVariables.DrawDebug();

        }

        public override void Update()
        {
            base.Update();
            if (!initAfterObjectsAdded)
            {
                initAfterObjectsAdded = true;
                InputManager.astar.Initialize(grid);
                InputManager.dfs.Initialize(grid);
            }

        }
    }
}

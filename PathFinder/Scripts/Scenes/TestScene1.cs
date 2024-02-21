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
        private Cell cell1, cell2;
        private Decoration key1, key2;
        private bool initAfterObjectsAdded;

        Timeline timeLine = new Timeline();
        public override void Initialize()
        {
            btn = new Button(GameWorld.Instance.uiCam.BottomCenter, TextureNames.StaticButton, "Hallo\nthis is a test", () => { });
            btn.SetCollisionBox(30, 20);
            SceneData.gameObjectsToAdd.Add(btn);
            grid = new Grid();
            grid.LoadGrid(Vector2.Zero);

            PlaceDecorationsTest();
            timeLine.StartThread(grid);
        }

        private void PlaceDecorationsTest()
        {
            chest = new Decoration(AnimNames.ChestOpen, grid.PosFromGridPos(new Point(17, 8)), 4);
            chest.animation.shouldPlay = false;
            SceneData.gameObjectsToAdd.Add(chest);

           

            //Test til punkt 10,1 på grid
            
        }

        private void KeyPosition()
        {
            Random random = new Random();
            List<Cell> cells = SceneData.cells.Where(x => x.isValid && x.cost >1).ToList();
            Cell cell1 = cells[random.Next(cells.Count-1)];
            cells.Remove(cell1);
            Cell cell2 = cells[random.Next(cells.Count - 1)];
            cells.Remove(cell2);

            key1 = new Decoration(TextureNames.Key, cell1.position, 4);
            SceneData.gameObjectsToAdd.Add(key1);

            key2 = new Decoration(TextureNames.Key, cell2.position, 4);
            SceneData.gameObjectsToAdd.Add(key2);


        }

        public override void DrawOnScreen()
        {
            base.DrawOnScreen();

            if (InputManager.debugStats) DebugVariables.DrawDebug();

        }
        bool init;
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

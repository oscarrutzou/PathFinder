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
        public Grid grid;

        public Timeline timeLine = new Timeline();
        private Button astarBtn, dfsBtn;
        private DialogueBox dialogueBox;

        private Player player;
        private string currentLocation;
        private bool hasKey1;
        private bool hasKey2;
        private bool visitedTower;
        private bool visitedChest;

        private bool initAfterObjectsAdded;
        public override void Initialize()
        {
            grid = new Grid();
            grid.LoadGrid(Vector2.Zero);

            UIAndDecorations();
            timeLine.StartThread(grid);
        }

        private void UIAndDecorations()
        {
            astarBtn = new Button(GameWorld.Instance.uiCam.BottomLeft + new Vector2(75, -50), TextureNames.StaticButton, "Select A*", () => { timeLine.pathFindingWithAstar = true; });
            astarBtn.SetCollisionBox(60, 30);
            astarBtn.scale = 2;
            dfsBtn = new Button(GameWorld.Instance.uiCam.BottomLeft + new Vector2(225, -50), TextureNames.StaticButton, "Select DFS", () => { timeLine.pathFindingWithAstar = false; });
            dfsBtn.SetCollisionBox(60, 30);
            dfsBtn.scale = 2;
            SceneData.gameObjectsToAdd.Add(astarBtn);
            SceneData.gameObjectsToAdd.Add(dfsBtn);



            
        }


        public override void DrawOnScreen()
        {
            base.DrawOnScreen();

            DebugVariables.DrawDebug();
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

            //DialogueBoxManager.UpdateDialogueBoxMessages(player, dialogueBox, currentLocation, hasKey1, hasKey2, visitedTower, visitedChest);

        }
    }
}

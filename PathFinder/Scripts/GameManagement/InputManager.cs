﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace PathFinder
{
    public static class InputManager
    {
        #region Variables
        public static KeyboardState keyboardState;
        public static KeyboardState previousKeyboardState;
        public static MouseState mouseState;
        // Prevents multiple click when clicking a button
        public static MouseState previousMouseState;

        public static Vector2 mousePositionInWorld;
        public static Vector2 mousePositionOnScreen;
        public static bool mouseClicked;
        public static bool mouseRightClicked;

        public static bool buildMode;
        public static bool mouseOutOfBounds;
        public static bool debugStats;

        private static int gameSpeedIndex = 1;

        private static List<float> gameSpeed = new List<float>()
        {
            { 0.5f},
            { 1f},
            { 2f},
            { 3f},
            //{ 10f}, //5 og hurtigere får wizard til at bugge.
        };

        public static Cell start, goal;
        public static Astar astar = new Astar();
        public static DFS dfs = new DFS();
        public static List<Cell> path = new List<Cell>();
        #endregion

        /// <summary>
        /// Gets called in GameWorld, at the start of the update
        /// </summary>
        public static void HandleInput()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            //Sets the mouse position
            mousePositionOnScreen = GetMousePositionOnUI();
            mousePositionInWorld = GetMousePositionInWorld();

            HandleKeyboardInput();
            HandleMouseInput();

            previousMouseState = mouseState;
            previousKeyboardState = keyboardState;
        }

        private static void HandleKeyboardInput()
        {
            // Check if the player presses the escape key
            if (keyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape))
            {
                GameWorld.Instance.Exit();
            }

            if (keyboardState.IsKeyDown(Keys.Tab) && !previousKeyboardState.IsKeyDown(Keys.Tab))
            {
                debugStats = !debugStats;
                if (GameWorld.Instance.currentScene is TestScene1 scene)
                {
                    scene.grid.ShowHideGrid();
                }
            }

            //if (keyboardState.IsKeyDown(Keys.O) && !previousKeyboardState.IsKeyDown(Keys.O))
            //{
            //    if (start != null && goal != null)
            //    {
            //        if (GameWorld.Instance.currentScene is TestScene1 scene)
            //        {
            //            path = astar.FindPath(start.gridPosition, goal.gridPosition);
            //            //path = dfs.FindPath(start.gridPosition, goal.gridPosition);
            //        }
            //    }
            //}

            //MoveCam();
            ChangeGameSpeed();
        }

        private static void ChangeGameSpeed()
        {
            if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left))
            {
                if (gameSpeedIndex >= 1)
                {
                    gameSpeedIndex -= 1;
                    GameWorld.Instance.gameSpeed = gameSpeed[gameSpeedIndex];
                }
            }

            if (keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))
            {
                if (gameSpeedIndex < gameSpeed.Count - 1)
                {
                    gameSpeedIndex += 1;
                    GameWorld.Instance.gameSpeed = gameSpeed[gameSpeedIndex];
                }
            }
        }

        private static void HandleMouseInput()
        {
            mouseClicked = (Mouse.GetState().LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton == ButtonState.Released);
            mouseRightClicked = (Mouse.GetState().RightButton == ButtonState.Pressed) && (previousMouseState.RightButton == ButtonState.Released);

            if (mouseClicked)
            {
                CheckButtons();
                
 
            }

            //if (mouseClicked || mouseRightClicked)
            //{
            //    if (GameWorld.Instance.currentScene is TestScene1 scene)
            //    {
            //        Cell cell = scene.grid.GetCell(GetMousePositionInWorld());
            //        if (cell == null || !cell.isValid) return;
            //        if (cell == start || cell == goal) return; //Shouldnt be able to pick the start or goal for a search.
                    

            //        if (mouseClicked)
            //        {
            //            start = cell;    
            //        }else if (mouseRightClicked)
            //        {
            //            goal = cell;
            //        }
            //    }
            //}
            
        }


        private static void CheckButtons()
        {
            if (SceneData.guis.Count == 0) return;

            foreach (Gui gui in SceneData.guis)
            {
                if (gui is Button button)
                {
                    if (button.IsMouseOver() && button.isVisible)
                    {
                        button.onClick?.Invoke();
                        return;  // Return early if a button was clicked
                    }
                }
            }
        }

        private static bool IsMouseOver(GameObject gameObject)
        {
            return gameObject.collisionBox.Contains(InputManager.mousePositionInWorld.ToPoint());
        }

        private static Vector2 GetMousePositionInWorld()
        {
            Vector2 pos = new Vector2(mouseState.X, mouseState.Y);
            Matrix invMatrix = Matrix.Invert(GameWorld.Instance.worldCam.GetMatrix());
            return Vector2.Transform(pos, invMatrix);
        }

        private static Vector2 GetMousePositionOnUI()
        {
            Vector2 pos = new Vector2(mouseState.X, mouseState.Y);
            Matrix invMatrix = Matrix.Invert(GameWorld.Instance.uiCam.GetMatrix());
            Vector2 returnValue = Vector2.Transform(pos, invMatrix);
            mouseOutOfBounds = (returnValue.X < 0 || returnValue.Y < 0 || returnValue.X > GameWorld.Instance.gfxManager.PreferredBackBufferWidth || returnValue.Y > GameWorld.Instance.gfxManager.PreferredBackBufferHeight);
            return returnValue;
        }


        private static void MoveCam()
        {
            // Handle camera movement based on keyboard input //-- look at
            Vector2 moveDirection = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W))
                moveDirection.Y = -1;
            if (keyboardState.IsKeyDown(Keys.S))
                moveDirection.Y = 1;
            if (keyboardState.IsKeyDown(Keys.A))
                moveDirection.X = -1;
            if (keyboardState.IsKeyDown(Keys.D))
                moveDirection.X = 1;

            GameWorld.Instance.worldCam.Move(moveDirection * 5); // Control camera speed //-- look at
        }

    }
}

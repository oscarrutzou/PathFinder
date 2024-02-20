using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using System.Linq;

namespace PathFinder
{
    public static class DebugVariables
    {
        private static Vector2 pos;

        public static Color selectedGridColor = Color.Green;
        public static Color debugNonWalkableTilesColor = Color.DeepPink;

        public static void DrawDebug()
        {
            pos = new Vector2(10, 10);
            GameWorld gameWorld = GameWorld.Instance;
            //DrawString($"GameSpeed: {GameWorld.Instance.gameSpeed}");

            if (gameWorld.currentScene is TestScene1 scene)
            {
                DrawString($"Mouse over grid pos: {scene.grid.GetTile(InputManager.mousePositionInWorld)?.gridPosition}");
                DrawString("");
                if (InputManager.start != null)
                {
                    DrawString($"Start pos: {InputManager.start.gridPosition}");

                }
                if (InputManager.goal != null)
                {
                    DrawString($"Goal pos: {InputManager.goal.gridPosition}");
                }
            }
        }

        private static void DrawString(string text)
        {
            GameWorld.Instance.spriteBatch.DrawString(GlobalTextures.defaultFont, text, pos, Color.Black);
            Vector2 size = GlobalTextures.defaultFont.MeasureString(text);
            pos.Y += size.Y;
        }
    }
}

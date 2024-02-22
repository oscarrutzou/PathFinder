using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System.Linq;

namespace PathFinder
{
    public static class DebugVariables
    {
        private static Vector2 pos;

        public static Color pathColor = Color.Green;
        public static Color searchedColor = Color.Red;

        public static void DrawDebug()
        {
            pos = new Vector2(10, 10);
            GameWorld gameWorld = GameWorld.Instance;
            DrawString($"Game speed: {GameWorld.Instance.gameSpeed}");
            if (gameWorld.currentScene is TestScene1 scene1)
            { 
                DrawString("Selected pathFinding algorithm: " + (scene1.timeLine.pathFindingWithAstar == true ? "Astar": "DFS"));
            }
            if (!InputManager.debugStats) return;

            DrawString("");

            if (gameWorld.currentScene is TestScene1 scene)
            {
                DrawString($"Mouse over grid pos: {scene.grid.GetCell(InputManager.mousePositionInWorld)?.gridPosition}");

                //DrawString($"{scene.grid.GetCell(InputManager.mousePositionInWorld)?.position}");
                //DrawString($"{scene.grid.GetCell(InputManager.mousePositionInWorld)?.positionCentered}");
                DrawString("");
                if (scene.timeLine.player != null)
                {
                    DrawString($"Player position in Vector2: {scene.timeLine.player.position}");
                    DrawString($"Player grid position: {scene.timeLine.player.gridPosition}");
                }
            }

            if (InputManager.path != null && InputManager.path.Count > 0)
            {
                for (int i = 0; i < InputManager.path.Count; i++)
                {
                    DrawString($"    {i+1}. {InputManager.path[i]}");
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

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PathFinder
{
    public enum TextureNames
    {
        Pixel,
        GrassTile,
        RoadTile,
    }

    // Dictionary of all textures
    public static class GlobalTextures
    {
        public static Dictionary<TextureNames, Texture2D> textures { get; private set; }

        public static void LoadContent()
        {
            ContentManager content = GameWorld.Instance.Content;
            // Load all textures
            textures = new Dictionary<TextureNames, Texture2D>
            {
                //{TextureNames.TestTile, content.Load<Texture2D>("World\\TestTile") },

            };
        }
    }
}

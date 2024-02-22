using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PathFinder
{
    public class DialogueBox: Gui
    {
        public DialogueBox(Vector2 position, TextureNames textureName, string text)
        {
            this.text = text;
            this.position = position;
            texture = GlobalTextures.textures[textureName];
            isCentered = true;
            font = GlobalTextures.defaultFontBigger;
        }

        public override void Draw()
        {
            base.Draw();
            DrawTextCentered();
        }
    }
}

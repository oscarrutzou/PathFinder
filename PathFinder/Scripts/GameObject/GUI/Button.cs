using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace PathFinder
{
    public class Button: Gui
    {
        public Button(Vector2 pos, TextureNames textureName, string text, Action onClick)
        {
            this.position = pos;
            texture = GlobalTextures.textures[textureName];
            this.text = text;
            this.onClick = onClick;
            isCentered = true;
        }

        public Button(Vector2 pos, AnimNames animNames, string text, Action onClick)
        {
            this.position = pos;
            animation = GlobalAnimations.animations[animNames];
            this.text = text;
            this.onClick = onClick;
            isCentered = true;
        }

        public bool IsMouseOver()
        {
            return collisionBox.Contains(InputManager.mousePositionOnScreen.ToPoint());
        }

        public override void Draw()
        {
            base.Draw();
            DrawTextCentered();
            DrawDebugCollisionBox(Color.Black);
        }

    }
}

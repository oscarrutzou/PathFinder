using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PathFinder.Scripts.GameObject.GUI
{
    public class DialogueBox
    {
        private SpriteFont font;
        private string text;
        private Vector2 position;
        private Texture2D dialogueBoxTexture;
        private bool isVisible = true;
        private string currentScenario;

        public DialogueBox(SpriteFont font, string text, Vector2 position)
        {
            this.font = font;
            this.text = text;
            this.position = position;
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                isVisible = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(dialogueBoxTexture, position, Color.White);
                spriteBatch.DrawString(font, text, GameWorld.Instance.uiCam.BottomCenter, Color.Black);
            }
        }

        // Method to update the current scenario
        public void UpdateScenario(string newScenario)
        {
            currentScenario = newScenario;
        }
    }
}

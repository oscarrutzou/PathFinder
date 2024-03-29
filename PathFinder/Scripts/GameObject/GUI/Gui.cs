﻿using PathFinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace PathFinder
{
    public class Gui : GameObject
    {
        public string text;
        public Action onClick;
        public Color textColor = Color.Black;
        internal SpriteFont font;
        public Gui()
        {
            layerDepth = 0.99f;
            font = GlobalTextures.defaultFont;
        }

        /// <summary>
        /// Can take and divide the text and center each part of the text.
        /// </summary>
        internal void DrawTextCentered()
        {
            // If the text is not visible or null, we don't need to do anything
            if (!isVisible || string.IsNullOrEmpty(text)) return;

            // Split the text into lines based on the newline character '\n'
            string[] lines = text.Split('\n');

            // Create an array to hold the size of each line
            Vector2[] lineSizes = new Vector2[lines.Length];

            // Measure the size of each line and store it in the array
            for (int i = 0; i < lines.Length; i++)
            {
                lineSizes[i] = font.MeasureString(lines[i]);
            }

            // Find the size of the longest line by comparing the width (X value) of each line. Vector2.Zero is the seed in the Aggregate method
            Vector2 maxSize = lineSizes.Aggregate(Vector2.Zero, (max, current) => (current.X > max.X) ? current : max);

            // Calculate the total height of the text block
            float totalHeight = lines.Length * font.LineSpacing;

            // Calculate the position to center the text based on the size of the longest line and total height
            Vector2 textPosition = position - new Vector2(maxSize.X / 2, totalHeight / 2);

            // Draw each line of the text
            for (int i = 0; i < lines.Length; i++)
            {
                // Calculate the position of the line, centering it horizontally and adjusting vertically based on the line number
                Vector2 linePosition = textPosition + new Vector2((maxSize.X - lineSizes[i].X) / 2, i * font.LineSpacing);

                // Draw the line of text
                GameWorld.Instance.spriteBatch.DrawString(font,
                                                          lines[i],
                                                          linePosition,
                                                          textColor,
                                                          0,
                                                          Vector2.Zero,
                                                          1,
                                                          SpriteEffects.None,
                                                          1);
            }
        }
    }
}

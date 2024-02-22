using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class Decoration: GameObject
    {
        public bool bounce;
        private Vector2 basePosition;
        private Vector2 targetPosition;
        public Vector2 targetPosOffset = new Vector2(0, -10);
        float lerpTime = 0;
        float lerpDuration = 1;

        public Decoration(TextureNames textureName, Vector2 pos)
        {
            texture = GlobalTextures.textures[textureName];
            ChangePos(pos);
            layerDepth = 0.5f;
        }
        public Decoration(AnimNames animNames, Vector2 pos)
        {
            animation = GlobalAnimations.animations[animNames];
            ChangePos(pos);
            layerDepth = 0.5f;
        }

        public Decoration(TextureNames textureName, Vector2 pos, bool isCentered)
        {
            texture = GlobalTextures.textures[textureName];
            ChangePos(pos);
            this.isCentered = isCentered;
            layerDepth = 0.5f;

        }
        public Decoration(AnimNames animNames, Vector2 pos, bool isCentered)
        {
            animation = GlobalAnimations.animations[animNames];
            position = pos;
            this.isCentered = isCentered;
            layerDepth = 0.5f;
        }

        public override void Update()
        {
            if (!bounce || !isVisible) return;
            targetPosition = basePosition + targetPosOffset;
            // Calculate the elapsed time since the last frame
            //Not taking the speed of the game into account, since it would look weird
            float elapsed = (float)GameWorld.Instance.gameTime.ElapsedGameTime.TotalSeconds;
            lerpTime += elapsed / lerpDuration;

            if (lerpTime > 2) // Reset after reaching target and returning
            {
                lerpTime -= 2; // Reset lerpTime to start the next cycle
            }
            else if (lerpTime > 1) // Return to base position after reaching target
            {
                position = Vector2.Lerp(targetPosition, basePosition, lerpTime - 1);
            }
            else // Move towards target position
            {
                position = Vector2.Lerp(basePosition, targetPosition, lerpTime);
            }
        }


        public void ChangePos(Vector2 pos)
        {
            this.position = pos;
            this.basePosition = pos;
        }
    }
}

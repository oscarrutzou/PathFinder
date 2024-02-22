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
        public Decoration(TextureNames textureName, Vector2 pos)
        {
            texture = GlobalTextures.textures[textureName];
            position = pos;
            layerDepth = 0.5f;
        }
        public Decoration(AnimNames animNames, Vector2 pos)
        {
            animation = GlobalAnimations.animations[animNames];
            position = pos;
            layerDepth = 0.5f;
        }

        public Decoration(TextureNames textureName, Vector2 pos, bool isCentered)
        {
            texture = GlobalTextures.textures[textureName];
            position = pos;
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
    }
}

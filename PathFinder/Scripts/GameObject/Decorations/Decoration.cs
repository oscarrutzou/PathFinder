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
        public Decoration(TextureNames textureName, Vector2 pos, int scale)
        {
            texture = GlobalTextures.textures[textureName];
            position = pos;
            this.scale = scale;
            layerDepth = 0.5f;
        }
        public Decoration(AnimNames animNames, Vector2 pos, int scale)
        {
            animation = GlobalAnimations.animations[animNames];
            position = pos;
            this.scale = scale;
            layerDepth = 0.5f;
        }

        public Decoration(TextureNames textureName, Vector2 pos, int scale, bool isCentered)
        {
            texture = GlobalTextures.textures[textureName];
            position = pos;
            this.scale = scale;
            this.isCentered = isCentered;
            layerDepth = 0.5f;

        }
        public Decoration(AnimNames animNames, Vector2 pos, int scale, bool isCentered)
        {
            animation = GlobalAnimations.animations[animNames];
            position = pos;
            this.scale = scale;
            this.isCentered = isCentered;
            layerDepth = 0.5f;
        }
    }
}

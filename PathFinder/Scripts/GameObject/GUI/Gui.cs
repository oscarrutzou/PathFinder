using PathFinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
namespace PathFinder
{
    public class Gui : GameObject
    {
        public string text;
        public Action onClick;
        public Color textColor = Color.Black;

        public Gui()
        {
            layerDepth = 0.99f;
        }
    }
}

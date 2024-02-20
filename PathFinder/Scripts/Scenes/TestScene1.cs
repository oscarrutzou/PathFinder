using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class TestScene1 : Scene
    {
        Button btn;

        public override void Initialize()
        {
            btn = new Button(GameWorld.Instance.uiCam.Center, TextureNames.StaticButton, "Hallo\nthis is a test", () => { btn.color = Color.DeepPink; });
            SceneData.gameObjectsToAdd.Add(btn);
        }

        public override void DrawOnScreen()
        {
            base.DrawOnScreen();

            if (InputManager.debugStats) DebugVariables.DrawDebug();

        }

        public override void Update()
        {
            base.Update();
            

        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace PathFinder
{
    public enum AnimNames
    {
        TestAnim,
        WizardUp,
        WizardDown,
        WizardLeft,
        WizardRight,
        Snake,
        ChestOpen,

        ButtonPress,
    }

    public static class GlobalAnimations
    {
        // Dictionary of all animations
        public static Dictionary<AnimNames, Animation> animations { get; private set; }

        public static void LoadContent()
        {
            animations = new Dictionary<AnimNames, Animation>();
            //Can upload sprite sheets, left to right
            //LoadSpriteSheet(AnimNames.FighterSlash, "Persons\\Worker\\FighterSlash", 32);
            LoadSpriteSheet(AnimNames.ChestOpen, "World\\chest", 16);
            LoadSpriteSheet(AnimNames.WizardUp, "Persons\\wizardUp", 32);
            LoadSpriteSheet(AnimNames.WizardDown, "Persons\\wizardDown", 32);
            LoadSpriteSheet(AnimNames.WizardLeft, "Persons\\wizardLeft", 32);
            LoadSpriteSheet(AnimNames.WizardRight, "Persons\\wizardRight", 32);
            LoadSpriteSheet(AnimNames.Snake, "Persons\\snake", 16);

            //How to use. Each animation should be called _0, then _1 and so on, on each texuture.
            //Remember the path should show everything and just delete the number. But keep the "_".
            //LoadIndividualFramesAnimationT(AnimNames.FighterDead, "Persons\\Worker\\FigtherTestDead_", 2);
            LoadIndividualFramesAnimationT(AnimNames.ButtonPress, "UI\\MediumBtn_", 4);
        }

        private static void LoadSpriteSheet(AnimNames animName, string path, int dem)
        {
            AnimationSpriteSheet spriteSheet = new AnimationSpriteSheet(
                GameWorld.Instance.Content.Load<Texture2D>(path),
                dem,
                animName);

            animations.Add(animName, spriteSheet);
        }

        private static void LoadIndividualFramesAnimationT(AnimNames animationName, string path, int framesInAnim)
        {
            // Load all frames in the animation
            List<Texture2D> animList = new List<Texture2D>();
            for (int i = 0; i < framesInAnim; i++)
            {
                animList.Add(GameWorld.Instance.Content.Load<Texture2D>(path + i));
            }

            AnimationIndividualFrames anim = new AnimationIndividualFrames(animList, animationName);
            animations.Add(animationName, anim);
        }
    }
}

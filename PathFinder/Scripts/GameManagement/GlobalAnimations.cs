using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace PathFinder
{
    public enum AnimNames
    {
        TestAnim,
        PlayerUp,
        PlayerDown,
        PlayerLeft,
        PlayerRight,
        SnakeIdle,
    }

    public static class GlobalAnimations
    {
        // Dictionary of all animations
        private static Dictionary<AnimNames, List<Texture2D>> animations = new Dictionary<AnimNames, List<Texture2D>>();
        public static Dictionary<AnimNames, Animation> animationsTest { get; private set; }

        public static void LoadContent()
        {
            animationsTest = new Dictionary<AnimNames, Animation>();
            //Can upload sprite sheets, left to right
            //LoadSpriteSheet(AnimNames.FighterSlash, "Persons\\Worker\\FighterSlash", 32);
            LoadSpriteSheet(AnimNames.PlayerUp, "Animations\\wizardUp", 32);
            LoadSpriteSheet(AnimNames.PlayerDown, "Animations\\wizardDown", 32);
            LoadSpriteSheet(AnimNames.PlayerLeft, "Animations\\wizardLeft", 32);
            LoadSpriteSheet(AnimNames.PlayerRight, "Animations\\wizardRight", 32);
            LoadSpriteSheet(AnimNames.SnakeIdle, "Animations\\snake", 16);

            //How to use. Each animation should be called _0, then _1 and so on, on each texuture.
            //Remember the path should show everything and just delete the number. But keep the "_".
            //LoadIndividualFramesAnimationT(AnimNames.FighterDead, "Persons\\Worker\\FigtherTestDead_", 2);
        }

        private static void LoadSpriteSheet(AnimNames animName, string path, int dem)
        {
            AnimationSpriteSheet spriteSheet = new AnimationSpriteSheet(
                GameWorld.Instance.Content.Load<Texture2D>(path),
                dem,
                animName);

            animationsTest.Add(animName, spriteSheet);
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
            animationsTest.Add(animationName, anim);
        }

        //public static AnimationSpriteSheet SetAnimation(AnimNames name)
        //{
        //    return new AnimationSpriteSheet()
        //}

        private static void LoadIndividualFramesAnimation(AnimNames animationName, string path, int framesInAnim)
        {
            // Load all frames in the animation
            List<Texture2D> animList = new List<Texture2D>();
            for (int i = 0; i < framesInAnim; i++)
            {
                animList.Add(GameWorld.Instance.Content.Load<Texture2D>(path + i));
            }
            animations[animationName] = animList;
        }

        //Should be used when the object want to set its animation, then it makes a new animation. Maybe another way
        //public static AnimationIndividualFrames SetAnimation(AnimNames name)
        //{
        //    return new AnimationIndividualFrames(animations[name], name);
        //}

    }
}

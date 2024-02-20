using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace PathFinder
{
    public class GameWorld : Game
    {
        private static GameWorld instance;

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }
        public GameTime gameTime { get; private set; }
        public SpriteBatch spriteBatch { get; private set; }

        public GraphicsDeviceManager gfxManager;
        public GraphicsDevice gfxDevice => GraphicsDevice;
        /// <summary>
        /// Grid
        /// </summary>
        private int cellCount = 10;

        private int cellSize = 100;

        /// <summary>
        /// Collections
        /// </summary>
        public Dictionary<Point, Cell> Cells { get; private set; } = new Dictionary<Point, Cell>();

        public Dictionary<string, Texture2D> sprites { get; private set; } = new Dictionary<string, Texture2D>();
        public SpriteFont SpriteFont { get; internal set; }

        public GameWorld()
        {
            gfxManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            for (int y = 0; y < cellCount; y++)
            {
                for (int x = 0; x < cellCount; x++)
                {
                    Cells.Add(new Point(x, y), new Cell(new Point(x, y), cellSize, cellSize));
                }
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteFont = Content.Load<SpriteFont>("MyFont");
            DirectoryInfo d = new DirectoryInfo(@"..\..\..\Content");


            FileInfo[] Files = d.GetFiles("*.png");

            foreach (FileInfo file in Files)
            {
                int i = file.Name.IndexOf('.');
                string name = file.Name.Remove(i);
                sprites.Add(name, Content.Load<Texture2D>(name));
            }
            spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (KeyValuePair<Point, Cell> cell in Cells)
            {
                cell.Value.LoadContent();
            }



            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (KeyValuePair<Point, Cell> cell in Cells)
            {
                cell.Value.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();


            foreach (KeyValuePair<Point, Cell> cell in Cells)
            {
                cell.Value.Draw(spriteBatch);
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

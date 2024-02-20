using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace PathFinder
{
    public class Game1 : Game
    {
        private static Game1 instance;

        public static Game1 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game1();
                }
                return instance;
            }
        }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public SpriteFont SpriteFont { get; set; }

        private int cellCount = 10;

        private int cellSize = 100;

        public Dictionary<Point, Grid> Cells { get; private set; } = new Dictionary<Point, Grid>();

        public Dictionary<string, Texture2D> sprites { get; private set; } = new Dictionary<string, Texture2D>();

        private Grid start, goal;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SpriteFont = Content.Load<SpriteFont>("TextFont");
            sprites["Pixel"] = Content.Load<Texture2D>("Pixel");
         

            for (int y = 0; y < cellCount; y++)
            {
                for (int x = 0; x < cellCount; x++)
                {
                    Cells.Add(new Point(x, y), new Grid(new Point(x, y), cellSize, cellSize));
                }
            }

            foreach (KeyValuePair<Point, Grid> cell in Cells)
            {
                cell.Value.LoadContent();
            }

            _graphics.PreferredBackBufferWidth = cellCount * cellSize + 200;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = cellCount * cellSize + 1;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (KeyValuePair<Point, Grid> cell in Cells)
            {
                cell.Value.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here

            foreach (KeyValuePair<Point, Grid> cell in Cells)
            {
                cell.Value.Draw(_spriteBatch);
            }
            base.Draw(gameTime);
            _spriteBatch.End();
        }

        public void OnCellClick(Grid clicked)
        {
            
        }
    }
}

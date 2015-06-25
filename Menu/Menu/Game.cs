using Menu.GameFolder.Components;
using Menu.MenuFolder.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menu
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public KeyboardState keyState;
        public KeyboardState keyStateBefore;

        public Texture2D spritMenuBackground;
        public Texture2D spritAbout;
        public Texture2D spritGameBackground;
        public Texture2D spritCar;



        public SpriteFont font;
        public SpriteFont normalFont;

        public static int width = 1600, height = 900;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();
            ComponentAbout about = new ComponentAbout(this);
            ComponentControls controls = new ComponentControls(this);
            ComponentGame theGame = new ComponentGame(this);
            ComponentGameMenu menu = new ComponentGameMenu(this, about, controls, theGame);
            Components.Add(menu);
            Components.Add(about);
            Components.Add(controls);
            Components.Add(theGame);
            StartUp(theGame);
            StartUp(about);
            StartUp(controls);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>(@"Font");
            normalFont = Content.Load<SpriteFont>(@"NormalFont");
            spritMenuBackground = Content.Load<Texture2D>(@"Sprits/backGround");
            spritAbout = Content.Load<Texture2D>(@"Sprits/About");
            spritGameBackground = Content.Load<Texture2D>(@"Sprits/grass");
            spritCar = Content.Load<Texture2D>(@"Sprits/car");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            keyStateBefore = keyState;
            keyState = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public bool SingleClick(Keys key)
        {
            return keyState.IsKeyDown(key) && keyStateBefore.IsKeyUp(key);
        }

        public void StartUp(GameComponent component)
        {
            component.Enabled = false;
            if (component is DrawableGameComponent)
                ((DrawableGameComponent)component).Visible = false;
        }
    }
}
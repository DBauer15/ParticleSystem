using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParticleSystem
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ParticleSystem pSys;
        ParticleSystem pSys2;
        ParticleSystem pSys3;
        ParticleSystem pSys4;
        ParticleSystem pSys5;

        SpriteFont font;
        KeyboardState oldState;

        RenderTarget2D currentSceneRenderTarget = null;
        RenderTarget2D prevSceneRenderTarget = null;

        bool go = false;

        Texture2D cursor;
        int particleAmount;
        bool reduceVel;

        int frameRate, frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;

        public Game1(int width, int height, int amount, bool reduceVel)
        {
            this.reduceVel = reduceVel;
            particleAmount = amount / 5;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            //graphics.IsFullScreen = true;
            this.IsMouseVisible = false;

            graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
            graphics.ApplyChanges();

            oldState = Keyboard.GetState();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            cursor = Content.Load<Texture2D>("cursor");

            pSys = new ParticleSystem(particleAmount, graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight / 3,reduceVel ,Content.Load<Texture2D>("pixel"), Color.White, new Random(1));
            pSys2 = new ParticleSystem(particleAmount, graphics.PreferredBackBufferWidth / 3, graphics.PreferredBackBufferHeight / 2, reduceVel, Content.Load<Texture2D>("pixel"), Color.White, new Random(2));
            pSys3 = new ParticleSystem(particleAmount, graphics.PreferredBackBufferWidth / 3 * 2, graphics.PreferredBackBufferHeight / 2, reduceVel, Content.Load<Texture2D>("pixel"), Color.White, new Random(3));
            pSys4 = new ParticleSystem(particleAmount, graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 3 * 2, reduceVel, Content.Load<Texture2D>("pixel"), Color.White, new Random(4));
            pSys5 = new ParticleSystem(particleAmount, graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2, reduceVel, Content.Load<Texture2D>("pixel"), Color.White, new Random(5));



            font = Content.Load<SpriteFont>("MainFont");
            currentSceneRenderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
            prevSceneRenderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                go = !go;

            oldState = Keyboard.GetState();

            if (go)
            {
                pSys.Update(Mouse.GetState(), graphics);
                pSys2.Update(Mouse.GetState(), graphics);
                pSys3.Update(Mouse.GetState(), graphics);
                pSys4.Update(Mouse.GetState(), graphics);
                pSys5.Update(Mouse.GetState(), graphics);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            frameCounter++;

            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.SetRenderTarget(currentSceneRenderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();
            spriteBatch.Draw(prevSceneRenderTarget, Vector2.Zero, Color.White*0.85f);
            spriteBatch.Draw(cursor, new Vector2(Mouse.GetState().X - 10, Mouse.GetState().Y - 10), Color.White);
            pSys.Draw(spriteBatch);
            pSys2.Draw(spriteBatch);
            pSys3.Draw(spriteBatch);
            pSys4.Draw(spriteBatch);
            pSys5.Draw(spriteBatch);
            
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.SetRenderTarget(prevSceneRenderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();

            spriteBatch.Draw(currentSceneRenderTarget, Vector2.Zero, Color.Orange);


            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Transparent);


            spriteBatch.Begin();
            spriteBatch.Draw(currentSceneRenderTarget, Vector2.Zero, Color.White);
            spriteBatch.DrawString(font, "Space - Start/Pause\nLMB - Gravity\nRMB - Drop\nWheel - Particle amount ("+pSys.Amount*5+")\n"+frameRate+" fps", new Vector2(20, 20), Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

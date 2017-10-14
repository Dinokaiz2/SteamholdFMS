using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using SteamholdFMS;

namespace SteamholdFMS
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        // Limit: ~11 chars each    
        String titleTextTop = "";
        String titleTextBottom = "";

        // OR

        // Limit: 5 chars
        String titleTextBig = "";

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Timer timer;
        Texture2D shadowMap;
        RenderTarget2D renderTarget;
        KeyboardState keyboardChanges;
        KeyboardState keyboardState;
        OverallScoreCounter scoreCounter;
        SpriteFont titleFont;
        SpriteFont bigTitleFont;
        SoundEffects soundEffects;
        OuterWorks outerWorks;
        GearCounter gearCounter;
        EndgameCounter endgameCounter;
        Texture2D logo;
        Texture2D background;
        RPCounter rpCounter;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            IsMouseVisible = true;
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
            Arduino.Initialize();
            timer = new Timer();
            scoreCounter = new OverallScoreCounter();
            renderTarget = new RenderTarget2D(this.GraphicsDevice, 3840, 2160);
            soundEffects = new SoundEffects();
            outerWorks = new OuterWorks();
            gearCounter = new GearCounter();
            endgameCounter = new EndgameCounter();
            rpCounter = new RPCounter();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Timer.Load(Content);
            OverallScoreCounter.Load(Content);
            SoundEffects.Load(Content);
            OuterWorks.Load(Content);
            GearCounter.Load(Content);
            EndgameCounter.Load(Content);
            RPCounter.Load(Content);
            titleFont = Content.Load<SpriteFont>("titlefont");
            bigTitleFont = Content.Load<SpriteFont>("titlefontsuper");
            logo = Content.Load<Texture2D>("steamhold4");
            background = Content.Load<Texture2D>("background");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                //Arduino.Close();
                Exit();
            }

            // TODO: Add your update logic here
            keyboardChanges = UltraKeyboard.getChanges();
            keyboardState = Keyboard.GetState();
            timer.Update(gameTime, soundEffects);
            soundEffects.Play();
            outerWorks.Update(gameTime, keyboardChanges, keyboardState);
            gearCounter.Update(gameTime, keyboardChanges, keyboardState);
            endgameCounter.Update(gameTime, keyboardChanges, keyboardState);
            bool redBreach = outerWorks.redBreach;
            bool blueBreach = outerWorks.blueBreach;
            bool redMidCapture = gearCounter.redMidCapture;
            bool blueMidCapture = gearCounter.blueMidCapture;
            bool redCapture = gearCounter.redMidCapture && endgameCounter.redAllChallenged;
            bool blueCapture = gearCounter.blueMidCapture && endgameCounter.blueAllChallenged;

            int redBreachPoints;
            int blueBreachPoints;
            int redCapturePoints;
            int blueCapturePoints;
            if (redBreach)
            {
                redBreachPoints = 20;
            } else
            {
                redBreachPoints = 0;
            }

            if (blueBreach)
            {
                blueBreachPoints = 20;
            }
            else
            {
                blueBreachPoints = 0;
            }

            scoreCounter.Update(outerWorks.blueScore + gearCounter.redScore + endgameCounter.redScore + blueBreachPoints,
                                outerWorks.redScore + gearCounter.blueScore + endgameCounter.blueScore + redBreachPoints);

            rpCounter.Update(blueBreach, redBreach, redMidCapture, blueMidCapture, redCapture, blueCapture);
            char[] package = outerWorks.package;
            if (gearCounter.redGears >= 6)
            {
                package[6] = '4';
            } else
            {
                package[6] = '0';
            }
            if (gearCounter.blueGears >= 6)
            {
                package[12] = '4';
            }
            else
            {
                package[12] = '0';
            }
            Arduino.Send(package);
            if (keyboardChanges.IsKeyDown(Keys.OemPipe))
            {
                outerWorks.Reset();
                gearCounter.Reset();
                endgameCounter.Reset();
                timer.StartMatch(gameTime);
            }
            if ((keyboardChanges.IsKeyDown(Keys.OemOpenBrackets) ||
                keyboardChanges.IsKeyDown(Keys.OemCloseBrackets)) &&
                !timer.InMatch)
            {
                outerWorks.Reset();
                gearCounter.Reset();
                endgameCounter.Reset();
                timer.ResetClean();
            }
            if (keyboardChanges.IsKeyDown(Keys.Enter))
            {
                outerWorks.Reset();
                gearCounter.Reset();
                endgameCounter.Reset();
                timer.Reset(soundEffects);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(new Color(0, 100, 0));
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            spriteBatch.Draw(background, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(logo, new Vector2(1200, 420), null, Color.White, 0, Vector2.Zero, 1.9f, SpriteEffects.None, 0);
            scoreCounter.Draw(spriteBatch);
            scoreCounter.DrawText(spriteBatch);
            outerWorks.Draw(spriteBatch);
            gearCounter.Draw(spriteBatch);
            endgameCounter.Draw(spriteBatch);
            rpCounter.Draw(spriteBatch);
            timer.Draw(spriteBatch);
            if (titleTextTop != "" || titleTextBottom != "")
            {
                DrawTitleTextSmall(titleTextTop, titleTextBottom);
            }
            else if(titleTextBig != "")
            {
                DrawTitleTextBig(titleTextBig);
            }
            shadowMap = (Texture2D)renderTarget;
            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            shadowMap = (Texture2D)renderTarget;
            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, null);
            GraphicsDevice.Clear(Color.Green);
            if (GraphicsDevice.Adapter.CurrentDisplayMode.AspectRatio > 3840 / 2160)
            {
                spriteBatch.Draw(shadowMap, Vector2.Zero, null, Color.White, 0,
                    Vector2.Zero,
                    new Vector2(0.21f, 0.227f), SpriteEffects.None, 1);
            }
            else
            {
                spriteBatch.Draw(shadowMap, Vector2.Zero, null, Color.White, 0,
                    Vector2.Zero,
                    (float)GraphicsDevice.Adapter.CurrentDisplayMode.Width / 3840f, SpriteEffects.None, 1);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DrawTitleTextSmall(String top, String bottom)
        {
            Vector2 textPosTop = new Vector2((3840 * 0.5f)
                - (titleFont.MeasureString(top).X * 0.5f),
                200
                - (titleFont.MeasureString(top).Y * 0.5f));
            spriteBatch.DrawString(titleFont, top, textPosTop, Color.Black);
            Vector2 textPosBot = new Vector2((3840 * 0.5f)
                - (titleFont.MeasureString(bottom).X * 0.5f),
                400
                - (titleFont.MeasureString(bottom).Y * 0.5f));
            spriteBatch.DrawString(titleFont, bottom, textPosBot, Color.Black);
        }

        public void DrawTitleTextBig(String text)
        {
            Vector2 textPos = new Vector2((3840 * 0.5f)
                - (bigTitleFont.MeasureString(text).X * 0.5f),
                300
                - (bigTitleFont.MeasureString(text).Y * 0.5f));
            spriteBatch.DrawString(bigTitleFont, text, textPos, Color.Black);
        }


    }
}


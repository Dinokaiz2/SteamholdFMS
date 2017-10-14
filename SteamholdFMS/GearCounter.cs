using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace SteamholdFMS
{
    class GearCounter
    {
        GameTime gameTime;
        public int redScore = 0;
        public int blueScore = 0;
        public int redGears = 0;
        public int blueGears = 0;
        public int redAutoGears = 0;
        public int blueAutoGears = 0;

        private static Texture2D redCounterImage;
        private static Texture2D blueCounterImage;
        private static Texture2D gearImage;
        private static SpriteFont scoreFont;

        public bool redMidCapture = false;
        public bool blueMidCapture = false;

        public static void Load(ContentManager content)
        {
            redCounterImage = content.Load<Texture2D>("redgearcounter");
            blueCounterImage = content.Load<Texture2D>("bluegearcounter");
            gearImage = content.Load<Texture2D>("gear");
            scoreFont = content.Load<SpriteFont>("timerfont");
        }

        public void Reset()
        {
            redGears = 0;
            blueGears = 0;
            redAutoGears = 0;
            blueAutoGears = 0;
        }

        public void Update(GameTime gameTime, KeyboardState newKeys, KeyboardState keyboardState)
        {
            this.gameTime = gameTime;
            if (newKeys.IsKeyDown(Keys.G))
            {
                redGears++;
                if (keyboardState.IsKeyDown(Keys.V))
                {
                    redAutoGears++;
                }
            } else if (newKeys.IsKeyDown(Keys.F))
            {
                redGears--;
                if (keyboardState.IsKeyDown(Keys.V))
                {
                    redAutoGears--;
                }
            }

            if (newKeys.IsKeyDown(Keys.J))
            {
                blueGears++;
                if (keyboardState.IsKeyDown(Keys.N))
                {
                    blueAutoGears++;
                }
            }
            else if (newKeys.IsKeyDown(Keys.H))
            {
                blueGears--;
                if (keyboardState.IsKeyDown(Keys.N))
                {
                    blueAutoGears--;
                }
            }

            redScore = (redGears * 5) + (redAutoGears * 5);
            blueScore = (blueGears * 5) + (blueAutoGears * 5);

            if (redGears >= 6)
            {
                redMidCapture = true;
            } else
            {
                redMidCapture = false;
            }
            if (blueGears >= 6)
            {
                blueMidCapture = true;
            }
            else
            {
                blueMidCapture = false;
            }
        }

        //public void DrawText(SpriteBatch spriteBatch)
        //{
        //    Vector2 scorePos = new Vector2(position.X + 425
        //        - (mySuperCoolFont.MeasureString(Score.ToString()).X * 0.5f),
        //        position.Y + 150
        //        - (mySuperCoolFont.MeasureString(Score.ToString()).Y * 0.5f));
        //    spriteBatch.DrawString(mySuperCoolFont, Score.ToString(), scorePos, Color.Black);
        //}

        //public void DrawRedControl(SpriteBatch spriteBatch)
        //{
        //    Vector2 controlPos = new Vector2(position.X - 50
        //        - (mySuperCoolFont.MeasureString(control).X * 0.5f),
        //        position.Y + 150
        //        - (mySuperCoolFont.MeasureString(control).Y * 0.5f));
        //    spriteBatch.DrawString(mySuperCoolFont, control, controlPos, Color.Black);
        //}

        //public void DrawBlueControl(SpriteBatch spriteBatch)
        //{
        //    Vector2 controlPos = new Vector2(position.X + 730
        //        - (mySuperCoolFont.MeasureString(control).X * 0.5f),
        //        position.Y + 150
        //        - (mySuperCoolFont.MeasureString(control).Y * 0.5f));
        //    spriteBatch.DrawString(mySuperCoolFont, control, controlPos, Color.Black);
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 redCounterPosition = new Vector2(310 - (redCounterImage.Width * 0.5f), 1575);
            spriteBatch.Draw(redCounterImage, redCounterPosition, null, Color.White, 0,
                                Vector2.Zero,
                                new Vector2(1.05f, 1), SpriteEffects.None, 0);
            Vector2 blueCounterPosition = new Vector2(3500 - (blueCounterImage.Width * 0.5f), 1575);
            spriteBatch.Draw(blueCounterImage, blueCounterPosition, null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
            Vector2 redTextPos = new Vector2(335
                - (scoreFont.MeasureString(redGears.ToString()).X * 0.5f),
                1780
                - (scoreFont.MeasureString(redGears.ToString()).Y * 0.5f));
            spriteBatch.DrawString(scoreFont, redGears.ToString(), redTextPos, Color.White);
            Vector2 blueTextPos = new Vector2(3510
                - (scoreFont.MeasureString(blueGears.ToString()).X * 0.5f),
                1780
                - (scoreFont.MeasureString(blueGears.ToString()).Y * 0.5f));
            spriteBatch.DrawString(scoreFont, blueGears.ToString(), blueTextPos, Color.White);

            if (redGears >= 6) {
                if (redGears >= 1)
                {
                    spriteBatch.Draw(gearImage, new Vector2(-200 + (gearImage.Width * 0.5f), 980 + (gearImage.Height * 0.5f)), null, Color.White, (float)gameTime.TotalGameTime.TotalSeconds * 3,
                                        new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                        0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 2)
                {
                    spriteBatch.Draw(gearImage, new Vector2(80 + (gearImage.Width * 0.5f), 980 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + -(float)gameTime.TotalGameTime.TotalSeconds * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 3)
                {
                    spriteBatch.Draw(gearImage, new Vector2(80 + (gearImage.Width * 0.5f), 700 + (gearImage.Height * 0.5f)), null, Color.White, (float)gameTime.TotalGameTime.TotalSeconds * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 4)
                {
                    spriteBatch.Draw(gearImage, new Vector2(-200 + (gearImage.Width * 0.5f), 700 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)gameTime.TotalGameTime.TotalSeconds * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 5)
                {
                    spriteBatch.Draw(gearImage, new Vector2(-200 + (gearImage.Width * 0.5f), 420 + (gearImage.Height * 0.5f)), null, Color.White, (float)gameTime.TotalGameTime.TotalSeconds * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 6)
                {
                    spriteBatch.Draw(gearImage, new Vector2(80 + (gearImage.Width * 0.5f), 420 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)gameTime.TotalGameTime.TotalSeconds * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 7)
                {
                    spriteBatch.Draw(gearImage, new Vector2(80 + (gearImage.Width * 0.5f), 140 + (gearImage.Height * 0.5f)), null, Color.White, (float)gameTime.TotalGameTime.TotalSeconds * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 8)
                {
                    spriteBatch.Draw(gearImage, new Vector2(-200 + (gearImage.Width * 0.5f), 140 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)gameTime.TotalGameTime.TotalSeconds * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 9)
                {
                    spriteBatch.Draw(gearImage, new Vector2(-200 + (gearImage.Width * 0.5f), -140 + (gearImage.Height * 0.5f)), null, Color.White, (float)gameTime.TotalGameTime.TotalSeconds * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 10)
                {
                    spriteBatch.Draw(gearImage, new Vector2(80 + (gearImage.Width * 0.5f), -140 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)gameTime.TotalGameTime.TotalSeconds * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
            } else
            {
                if (redGears >= 1)
                {
                    spriteBatch.Draw(gearImage, new Vector2(-200 + (gearImage.Width * 0.5f), 980 + (gearImage.Height * 0.5f)), null, Color.White, (float)0 * 3,
                                        new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                        0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 2)
                {
                    spriteBatch.Draw(gearImage, new Vector2(80 + (gearImage.Width * 0.5f), 980 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + -(float)0 * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 3)
                {
                    spriteBatch.Draw(gearImage, new Vector2(80 + (gearImage.Width * 0.5f), 700 + (gearImage.Height * 0.5f)), null, Color.White, (float)0 * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 4)
                {
                    spriteBatch.Draw(gearImage, new Vector2(-200 + (gearImage.Width * 0.5f), 700 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)0 * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 5)
                {
                    spriteBatch.Draw(gearImage, new Vector2(-200 + (gearImage.Width * 0.5f), 420 + (gearImage.Height * 0.5f)), null, Color.White, (float)0 * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 6)
                {
                    spriteBatch.Draw(gearImage, new Vector2(80 + (gearImage.Width * 0.5f), 420 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)0 * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 7)
                {
                    spriteBatch.Draw(gearImage, new Vector2(80 + (gearImage.Width * 0.5f), 140 + (gearImage.Height * 0.5f)), null, Color.White, (float)0 * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 8)
                {
                    spriteBatch.Draw(gearImage, new Vector2(-200 + (gearImage.Width * 0.5f), 140 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)0 * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 9)
                {
                    spriteBatch.Draw(gearImage, new Vector2(-200 + (gearImage.Width * 0.5f), -140 + (gearImage.Height * 0.5f)), null, Color.White, (float)0 * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (redGears >= 10)
                {
                    spriteBatch.Draw(gearImage, new Vector2(80 + (gearImage.Width * 0.5f), -140 + (gearImage.Height * 0.5f)), null, Color.White, (float)0 * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
            }
            
            if (blueGears >= 6)
            {
                if (blueGears >= 1)
                {
                    spriteBatch.Draw(gearImage, new Vector2(2975 + (gearImage.Width * 0.5f), 980 + (gearImage.Height * 0.5f)), null, Color.White, (float)gameTime.TotalGameTime.TotalSeconds * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 2)
                {
                    spriteBatch.Draw(gearImage, new Vector2(3255 + (gearImage.Width * 0.5f), 980 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + -(float)gameTime.TotalGameTime.TotalSeconds * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 3)
                {
                    spriteBatch.Draw(gearImage, new Vector2(3255 + (gearImage.Width * 0.5f), 700 + (gearImage.Height * 0.5f)), null, Color.White, (float)gameTime.TotalGameTime.TotalSeconds * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 4)
                {
                    spriteBatch.Draw(gearImage, new Vector2(2975 + (gearImage.Width * 0.5f), 700 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)gameTime.TotalGameTime.TotalSeconds * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 5)
                {
                    spriteBatch.Draw(gearImage, new Vector2(2975 + (gearImage.Width * 0.5f), 420 + (gearImage.Height * 0.5f)), null, Color.White, (float)gameTime.TotalGameTime.TotalSeconds * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 6)
                {
                    spriteBatch.Draw(gearImage, new Vector2(3255 + (gearImage.Width * 0.5f), 420 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)gameTime.TotalGameTime.TotalSeconds * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 7)
                {
                    spriteBatch.Draw(gearImage, new Vector2(3255 + (gearImage.Width * 0.5f), 140 + (gearImage.Height * 0.5f)), null, Color.White, (float)gameTime.TotalGameTime.TotalSeconds * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 8)
                {
                    spriteBatch.Draw(gearImage, new Vector2(2975 + (gearImage.Width * 0.5f), 140 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)gameTime.TotalGameTime.TotalSeconds * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 9)
                {
                    spriteBatch.Draw(gearImage, new Vector2(2975 + (gearImage.Width * 0.5f), -140 + (gearImage.Height * 0.5f)), null, Color.White, (float)gameTime.TotalGameTime.TotalSeconds * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 10)
                {
                    spriteBatch.Draw(gearImage, new Vector2(3255 + (gearImage.Width * 0.5f), -140 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)gameTime.TotalGameTime.TotalSeconds * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
            } else
            {
                if (blueGears >= 1)
                {
                    spriteBatch.Draw(gearImage, new Vector2(2975 + (gearImage.Width * 0.5f), 980 + (gearImage.Height * 0.5f)), null, Color.White, (float)0 * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 2)
                {
                    spriteBatch.Draw(gearImage, new Vector2(3255 + (gearImage.Width * 0.5f), 980 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + -(float)0 * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 3)
                {
                    spriteBatch.Draw(gearImage, new Vector2(3255 + (gearImage.Width * 0.5f), 700 + (gearImage.Height * 0.5f)), null, Color.White, (float)0 * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 4)
                {
                    spriteBatch.Draw(gearImage, new Vector2(2975 + (gearImage.Width * 0.5f), 700 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)0 * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 5)
                {
                    spriteBatch.Draw(gearImage, new Vector2(2975 + (gearImage.Width * 0.5f), 420 + (gearImage.Height * 0.5f)), null, Color.White, (float)0 * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 6)
                {
                    spriteBatch.Draw(gearImage, new Vector2(3255 + (gearImage.Width * 0.5f), 420 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)0 * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 7)
                {
                    spriteBatch.Draw(gearImage, new Vector2(3255 + (gearImage.Width * 0.5f), 140 + (gearImage.Height * 0.5f)), null, Color.White, (float)0 * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 8)
                {
                    spriteBatch.Draw(gearImage, new Vector2(2975 + (gearImage.Width * 0.5f), 140 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)0 * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 9)
                {
                    spriteBatch.Draw(gearImage, new Vector2(2975 + (gearImage.Width * 0.5f), -140 + (gearImage.Height * 0.5f)), null, Color.White, (float)0 * 3,
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
                if (blueGears >= 10)
                {
                    spriteBatch.Draw(gearImage, new Vector2(3255 + (gearImage.Width * 0.5f), -140 + (gearImage.Height * 0.5f)), null, Color.White, 0.33f + (-(float)0 * 3),
                                    new Vector2(gearImage.Width * 0.5f, gearImage.Height * 0.5f),
                                    0.4f, SpriteEffects.None, 0);
                }
            }
        }
    }
}

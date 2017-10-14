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
    class OuterWorks
    {
        public enum Positions {
            Red1,
            Red2,
            Red3,
            Red4,
            Red5,
            Blue1,
            Blue2,
            Blue3,
            Blue4,
            Blue5
        };

        protected Vector2 position;
        GameTime gameTime;
        public int redScore = 0;
        public int blueScore = 0;

        public char[] package = new char[13];

        static SpriteFont mySuperCoolFont;

        private static List<Defense> redDefenses = new List<Defense>();
        private static List<Defense> blueDefenses = new List<Defense>();
        private static int redAutoCrossings = 0;
        private static int blueAutoCrossings = 0;
        private static Texture2D image;

        public bool redBreach;
        public bool blueBreach;

        public OuterWorks()
        {
            redDefenses.Add(new Defense(new Vector2(665, 15), Positions.Red1, Keys.D2));
            redDefenses.Add(new Defense(new Vector2(665, 413), Positions.Red2, Keys.W));
            redDefenses.Add(new Defense(new Vector2(665, 813), Positions.Red3, Keys.S));    
            redDefenses.Add(new Defense(new Vector2(665, 1213), Positions.Red4, Keys.X));
            redDefenses.Add(new Defense(new Vector2(665, 1613), Positions.Red5, Keys.LeftAlt));
            blueDefenses.Add(new Defense(new Vector2(2697, 15), Positions.Blue1, Keys.D8));
            blueDefenses.Add(new Defense(new Vector2(2697, 413), Positions.Blue2, Keys.I));
            blueDefenses.Add(new Defense(new Vector2(2697, 813), Positions.Blue3, Keys.K));
            blueDefenses.Add(new Defense(new Vector2(2697, 1213), Positions.Blue4, Keys.OemComma));
            blueDefenses.Add(new Defense(new Vector2(2697, 1613), Positions.Blue5, Keys.RightAlt));
        }

        public static void Load(ContentManager content)
        {
            mySuperCoolFont = content.Load<SpriteFont>("mysupercoolfont");
            image = content.Load<Texture2D>("outerworks");
            Defense.Load(content);
        }

        public void Reset()
        {
            foreach (Defense defense in redDefenses)
            {
                defense.state = Defense.State.Untouched;
            }
            foreach (Defense defense in blueDefenses)
            {
                defense.state = Defense.State.Untouched;
            }
            redAutoCrossings = 0;
            blueAutoCrossings = 0;
        }

        public void Update(GameTime gameTime, KeyboardState newKeys, KeyboardState keyboardState)
        {
            this.gameTime = gameTime;
            foreach(Defense defense in redDefenses)
            {
                defense.Update(gameTime, newKeys, keyboardState, ref redAutoCrossings);
            }
            foreach (Defense defense in blueDefenses)
            {
                defense.Update(gameTime, newKeys, keyboardState, ref blueAutoCrossings);
            }
            redScore = 0;
            foreach (Defense defense in redDefenses)
            {
                if (defense.state == Defense.State.Weakened)
                {
                    redScore += 5;
                } else if (defense.state == Defense.State.Damaged)
                {
                    redScore += 10;
                }
            }
            redScore += redAutoCrossings * 5;
            blueScore = 0;
            foreach (Defense defense in blueDefenses)
            {
                if (defense.state == Defense.State.Weakened)
                {
                    blueScore += 5;
                }
                else if (defense.state == Defense.State.Damaged)
                {
                    blueScore += 10;
                }
            }
            blueScore += blueAutoCrossings * 5;
            package[0] = 't';
            for(int i = 1; i < 6; i++)
            {
                if (redDefenses[i-1].state == Defense.State.Untouched)
                {
                    package[i] = 'n';
                }
                else if (redDefenses[i-1].state == Defense.State.Weakened)
                {
                    package[i] = 'd';
                }
                else if (redDefenses[i-1].state == Defense.State.Damaged)
                {
                    package[i] = 'f';
                }
            }
            for (int i = 1; i < 6; i++)
            {
                if (blueDefenses[i - 1].state == Defense.State.Untouched)
                {
                    package[i+6] = 'n';
                }
                else if (blueDefenses[i - 1].state == Defense.State.Weakened)
                {
                    package[i+6] = 'd';
                }
                else if (blueDefenses[i - 1].state == Defense.State.Damaged)
                {
                    package[i+6] = 'f';
                }
            }
            int damagedDefenses = 0;
            foreach (Defense defense in redDefenses)
            {
                if (defense.state == Defense.State.Damaged)
                {
                    damagedDefenses++;
                }
            }
            if (damagedDefenses >= 4)
            {
                redBreach = true;
            } else
            {
                redBreach = false;
            }
            damagedDefenses = 0;
            foreach (Defense defense in blueDefenses)
            {
                if (defense.state == Defense.State.Damaged)
                {
                    damagedDefenses++;
                }
            }
            if (damagedDefenses >= 4)
            {
                blueBreach = true;
            }
            else
            {
                blueBreach = false;
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
            position = new Vector2((3840 * 0.5f) - (image.Width * 0.5f),
                                    0);
            spriteBatch.Draw(image, position, null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);

            foreach (Defense defense in redDefenses)
            {
                defense.Draw(spriteBatch);
            }
            foreach (Defense defense in blueDefenses)
            {
                defense.Draw(spriteBatch);
            }
        }

        public void DrawTimerControl(SpriteBatch spriteBatch)
        {
            Vector2 controlPos = new Vector2((3840 * 0.5f)
                - (mySuperCoolFont.MeasureString("Start: R + T").X * 0.5f)
                - 400,
                1400
                - (mySuperCoolFont.MeasureString("Start: R + T").Y * 0.5f));
            spriteBatch.DrawString(mySuperCoolFont, "Start: R + T", controlPos, Color.Black);

            controlPos = new Vector2((3840 * 0.5f)
                - (mySuperCoolFont.MeasureString("Fault: V + B").X * 0.5f)
                + 400,
                1400
                - (mySuperCoolFont.MeasureString("Fault: V + B").Y * 0.5f));
            spriteBatch.DrawString(mySuperCoolFont, "Fault: V + B", controlPos, Color.Black);
        }
    }

}

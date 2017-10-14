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
    class Defense
    {
        public enum State
        {
            Untouched,
            Weakened,
            Damaged
        };

        public State state;

        protected Vector2 position;
        GameTime gameTime;
        OuterWorks.Positions defPos;

        private static Texture2D redUntouchedImage;
        private static Texture2D redWeakenedImage;
        private static Texture2D redDamagedImage;
        private static Texture2D blueUntouchedImage;
        private static Texture2D blueWeakenedImage;
        private static Texture2D blueDamagedImage;

        static SpriteFont mySuperCoolFont;
        Keys control;
        
        public Defense(Vector2 position, OuterWorks.Positions defPos, Keys control)
        {
            this.position = position;
            this.control = control;
            this.defPos = defPos;
        }

        public static void Load(ContentManager content)
        {
            mySuperCoolFont = content.Load<SpriteFont>("mysupercoolfont");
            redUntouchedImage = content.Load<Texture2D>("reddefenseuntouched");
            redWeakenedImage = content.Load<Texture2D>("reddefenseweakened");
            redDamagedImage = content.Load<Texture2D>("reddefensedamaged");
            blueUntouchedImage = content.Load<Texture2D>("bluedefenseuntouched");
            blueWeakenedImage = content.Load<Texture2D>("bluedefenseweakened");
            blueDamagedImage = content.Load<Texture2D>("bluedefensedamaged");
        }

        public void Increment()
        {
            if (state == State.Untouched)
            {
                state = State.Weakened;
            } else if (state == State.Weakened)
            {
                state = State.Damaged;
            }
        }
        
        public void Deincrement()
        {
            if (state == State.Weakened)
            {
                state = State.Untouched;
            }
            else if (state == State.Damaged)
            {
                state = State.Weakened;
            }
        }

        public void Reset()
        {
            state = State.Untouched;
        }

        public void Update(GameTime gameTime, KeyboardState newKeys, KeyboardState allKeys, ref int autoCrossings)
        {
            this.gameTime = gameTime;
            if (defPos == OuterWorks.Positions.Red1 ||
                defPos == OuterWorks.Positions.Red2 ||
                defPos == OuterWorks.Positions.Red3 ||
                defPos == OuterWorks.Positions.Red4 ||
                defPos == OuterWorks.Positions.Red5)
            {
                if (newKeys.IsKeyDown(control))
                {
                    if (allKeys.IsKeyDown(Keys.LeftShift))
                    {
                        Deincrement();
                        if (allKeys.IsKeyDown(Keys.LeftControl))
                        {
                            autoCrossings--;
                        }
                    } else
                    {
                        Increment();
                        if (allKeys.IsKeyDown(Keys.LeftControl))
                        {
                            autoCrossings++;
                        }
                    }
                }
            } else
            {
                if (newKeys.IsKeyDown(control))
                {
                    if (allKeys.IsKeyDown(Keys.RightShift))
                    {
                        Deincrement();
                        if (allKeys.IsKeyDown(Keys.RightControl))
                        {
                            autoCrossings--;
                        }
                    } else
                    {
                        Increment();
                        if (allKeys.IsKeyDown(Keys.RightControl))
                        {
                            autoCrossings++;
                        }
                    }
                }
            }
        }

        public void DrawRedControl(SpriteBatch spriteBatch)
        {
            Vector2 controlPos = new Vector2(position.X - 50
                - (mySuperCoolFont.MeasureString(control.ToString()).X * 0.5f),
                position.Y + 150
                - (mySuperCoolFont.MeasureString(control.ToString()).Y * 0.5f));
            spriteBatch.DrawString(mySuperCoolFont, control.ToString(), controlPos, Color.Black);
        }

        public void DrawBlueControl(SpriteBatch spriteBatch)
        {
            Vector2 controlPos = new Vector2(position.X + 730
                - (mySuperCoolFont.MeasureString(control.ToString()).X * 0.5f),
                position.Y + 150
                - (mySuperCoolFont.MeasureString(control.ToString()).Y * 0.5f));
            spriteBatch.DrawString(mySuperCoolFont, control.ToString(), controlPos, Color.Black);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            if (defPos == OuterWorks.Positions.Red1 ||
                defPos == OuterWorks.Positions.Red2 ||
                defPos == OuterWorks.Positions.Red3 ||
                defPos == OuterWorks.Positions.Red4 ||
                defPos == OuterWorks.Positions.Red5)
            {
                if (state == State.Untouched)
                {
                    spriteBatch.Draw(redUntouchedImage, position, null, Color.White, 0,
                                        Vector2.Zero,
                                        1, SpriteEffects.None, 0);
                } else if (state == State.Weakened)
                {
                    spriteBatch.Draw(redWeakenedImage, position, null, Color.White, 0,
                                        Vector2.Zero,
                                        1, SpriteEffects.None, 0);
                } else if (state == State.Damaged)
                {
                    spriteBatch.Draw(redDamagedImage, position, null, Color.White, 0,
                                        Vector2.Zero,
                                        1, SpriteEffects.None, 0);
                }
            } else
            {
                if (state == State.Untouched)
                {
                    spriteBatch.Draw(blueUntouchedImage, position, null, Color.White, 0,
                                        Vector2.Zero,
                                        1, SpriteEffects.None, 0);
                }
                else if (state == State.Weakened)
                {
                    spriteBatch.Draw(blueWeakenedImage, position, null, Color.White, 0,
                                        Vector2.Zero,
                                        1, SpriteEffects.None, 0);
                }
                else if (state == State.Damaged)
                {
                    spriteBatch.Draw(blueDamagedImage, position, null, Color.White, 0,
                                        Vector2.Zero,
                                        1, SpriteEffects.None, 0);
                }
            }
        }
    }

}

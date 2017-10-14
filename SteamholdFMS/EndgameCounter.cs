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
    class EndgameCounter
    {
        public enum EndgameTask
        {
            None,
            Board,
            Climb
        };

        public EndgameTask[] redEndgame = new EndgameTask[3];
        public EndgameTask[] blueEndgame = new EndgameTask[3];

        GameTime gameTime;
        public int redScore = 0;
        public int blueScore = 0;
        
        private static Texture2D redOffImage;
        private static Texture2D redBoardImage;
        private static Texture2D redClimbImage;
        private static Texture2D blueOffImage;
        private static Texture2D blueBoardImage;
        private static Texture2D blueClimbImage;

        public bool redAllChallenged = false;
        public bool blueAllChallenged = false;

        public EndgameCounter()
        {
            redEndgame[0] = EndgameTask.None;
            redEndgame[1] = EndgameTask.None;
            redEndgame[2] = EndgameTask.None;
            blueEndgame[0] = EndgameTask.None;
            blueEndgame[1] = EndgameTask.None;
            blueEndgame[2] = EndgameTask.None;
        }

        public static void Load(ContentManager content)
        {
            redOffImage = content.Load<Texture2D>("redendgameoff");
            redBoardImage = content.Load<Texture2D>("redendgameboard");
            redClimbImage = content.Load<Texture2D>("redendgameclimb");
            blueOffImage = content.Load<Texture2D>("blueendgameoff");
            blueBoardImage = content.Load<Texture2D>("blueendgameboard");
            blueClimbImage = content.Load<Texture2D>("blueendgameclimb");
        }

        public void Update(GameTime gameTime, KeyboardState newKeys, KeyboardState keyboardState)
        {
            int index;
            if (newKeys.IsKeyDown(Keys.E))
            {
                if (keyboardState.IsKeyDown(Keys.D)) {
                    index = Array.IndexOf(redEndgame, EndgameTask.Board);
                    if (index != -1)
                    {
                        redEndgame[index] = EndgameTask.None;
                    }
                } else
                {
                    index = Array.IndexOf(redEndgame, EndgameTask.None);
                    if (index != -1)
                    {
                        redEndgame[index] = EndgameTask.Board;
                    }
                }
            } else if (newKeys.IsKeyDown(Keys.R))
            {
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    index = Array.IndexOf(redEndgame, EndgameTask.Climb);
                    if (index != -1)
                    {
                        redEndgame[index] = EndgameTask.None;
                    }
                }
                else
                {
                    index = Array.IndexOf(redEndgame, EndgameTask.None);
                    if (index != -1)
                    {
                        redEndgame[index] = EndgameTask.Climb;
                    }
                }
            }

            if (newKeys.IsKeyDown(Keys.D6))
            {
                if (keyboardState.IsKeyDown(Keys.Y))
                {
                    index = Array.IndexOf(blueEndgame, EndgameTask.Board);
                    if (index != -1)
                    {
                        blueEndgame[index] = EndgameTask.None;
                    }
                }
                else
                {
                    index = Array.IndexOf(blueEndgame, EndgameTask.None);
                    if (index != -1)
                    {
                        blueEndgame[index] = EndgameTask.Board;
                    }
                }
            }
            else if (newKeys.IsKeyDown(Keys.D7))
            {
                if (keyboardState.IsKeyDown(Keys.Y))
                {
                    index = Array.IndexOf(blueEndgame, EndgameTask.Climb);
                    if (index != -1)
                    {
                        blueEndgame[index] = EndgameTask.None;
                    }
                }
                else
                {
                    index = Array.IndexOf(blueEndgame, EndgameTask.None);
                    if (index != -1)
                    {
                        blueEndgame[index] = EndgameTask.Climb;
                    }
                }
            }

            redScore = 0;
            foreach (EndgameTask task in redEndgame)
            {
                if (task == EndgameTask.Board)
                {
                    redScore += 6;
                } else if (task == EndgameTask.Climb)
                {
                    redScore += 21;
                }
            }
            blueScore = 0;
            foreach (EndgameTask task in blueEndgame)
            {
                if (task == EndgameTask.Board)
                {
                    blueScore += 6;
                }
                else if (task == EndgameTask.Climb)
                {
                    blueScore += 21;
                }
            }
            if (Array.IndexOf(redEndgame, EndgameTask.None) == -1)
            {
                redAllChallenged = true;
            } else
            {
                redAllChallenged = false;
            }
            if (Array.IndexOf(blueEndgame, EndgameTask.None) == -1)
            {
                blueAllChallenged = true;
            }
            else
            {
                blueAllChallenged = false;
            }
        }

        public void Reset()
        {
            redEndgame[0] = EndgameTask.None;
            redEndgame[1] = EndgameTask.None;
            redEndgame[2] = EndgameTask.None;
            blueEndgame[0] = EndgameTask.None;
            blueEndgame[1] = EndgameTask.None;
            blueEndgame[2] = EndgameTask.None;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (redEndgame[0] == EndgameTask.None)
            {
                spriteBatch.Draw(redOffImage, new Vector2(1200, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            } else if (redEndgame[0] == EndgameTask.Board)
            {
                spriteBatch.Draw(redBoardImage, new Vector2(1200, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            } else if (redEndgame[0] == EndgameTask.Climb)
            {
                spriteBatch.Draw(redClimbImage, new Vector2(1200, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            }

            if (redEndgame[1] == EndgameTask.None)
            {
                spriteBatch.Draw(redOffImage, new Vector2(1400, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            } else if (redEndgame[1] == EndgameTask.Board)
            {
                spriteBatch.Draw(redBoardImage, new Vector2(1400, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            } else if (redEndgame[1] == EndgameTask.Climb)
            {
                spriteBatch.Draw(redClimbImage, new Vector2(1400, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            }

            if (redEndgame[2] == EndgameTask.None)
            {
                spriteBatch.Draw(redOffImage, new Vector2(1600, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            } else if (redEndgame[2] == EndgameTask.Board)
            {
                spriteBatch.Draw(redBoardImage, new Vector2(1600, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            } else if (redEndgame[2] == EndgameTask.Climb)
            {
                spriteBatch.Draw(redClimbImage, new Vector2(1600, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            }

            if (blueEndgame[0] == EndgameTask.None)
            {
                spriteBatch.Draw(blueOffImage, new Vector2(2490, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            }
            else if (blueEndgame[0] == EndgameTask.Board)
            {
                spriteBatch.Draw(blueBoardImage, new Vector2(2490, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            }
            else if (blueEndgame[0] == EndgameTask.Climb)
            {
                spriteBatch.Draw(blueClimbImage, new Vector2(2490, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            }

            if (blueEndgame[1] == EndgameTask.None)
            {
                spriteBatch.Draw(blueOffImage, new Vector2(2290, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            }
            else if (blueEndgame[1] == EndgameTask.Board)
            {
                spriteBatch.Draw(blueBoardImage, new Vector2(2290, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            }
            else if (blueEndgame[1] == EndgameTask.Climb)
            {
                spriteBatch.Draw(blueClimbImage, new Vector2(2290, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            }

            if (blueEndgame[2] == EndgameTask.None)
            {
                spriteBatch.Draw(blueOffImage, new Vector2(2090, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            }
            else if (blueEndgame[2] == EndgameTask.Board)
            {
                spriteBatch.Draw(blueBoardImage, new Vector2(2090, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            }
            else if (blueEndgame[2] == EndgameTask.Climb)
            {
                spriteBatch.Draw(blueClimbImage, new Vector2(2090, 1420), null, Color.White, 0,
                                    Vector2.Zero,
                                    new Vector2(1.05f, 1), SpriteEffects.None, 0);
            }
        }
    }
}

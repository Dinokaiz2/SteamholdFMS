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
    class Timer
    {
        private static Texture2D image;
        private static Texture2D greenBarImage;
        private static Texture2D yellowBarImage;
        private static Texture2D redBarImage;
        private static Texture2D background;

        private Vector2 position;
        private float timeMatchStarted;
        private bool inMatch = false;
        public bool InMatch
        {
            get { return inMatch; }
        }
        bool auto = false;
        bool teleop = false;
        bool endgame = false;
        bool matchJustStarted = false;
        static SpriteFont timerFont;
        String timeStr = "0";
        GameTime gameTime;
        float secondsToDisplay;

        public static void Load(ContentManager content)
        {
            timerFont = content.Load<SpriteFont>("timerfont");
            background = content.Load<Texture2D>("timerbackground");
            image = content.Load<Texture2D>("timerbar");
            greenBarImage = content.Load<Texture2D>("greentimer");
            yellowBarImage = content.Load<Texture2D>("yellowtimer");
            redBarImage = content.Load<Texture2D>("redtimer");
        }

        public void Update(GameTime gameTime, SoundEffects soundEffects)
        {
            this.gameTime = gameTime;
            if(inMatch)
            {
                // currently in true seconds since match start counting up
                secondsToDisplay = (float)gameTime.TotalGameTime.TotalSeconds - timeMatchStarted;
                if(secondsToDisplay <= 15)
                {
                    if(matchJustStarted)
                    {
                        soundEffects.addToQueue("autoStart");
                        matchJustStarted = false;
                    }
                    // auto
                    auto = true;
                    teleop = false;
                    endgame = false;
                    secondsToDisplay = 15 - secondsToDisplay;
                } else if(secondsToDisplay <= 119)
                {
                    if(!teleop)
                    {
                        soundEffects.addToQueue("teleopStart");
                    }
                    // teleop
                    auto = false;
                    teleop = true;
                    endgame = false;
                    secondsToDisplay -= 15;
                    secondsToDisplay = 135 - secondsToDisplay;
                } else if(secondsToDisplay <= 150)
                {
                    // endgame
                    if(!endgame)
                    {
                        soundEffects.addToQueue("endgameStart");
                    }
                    auto = false;
                    teleop = false;
                    endgame = true;
                    secondsToDisplay -= 15;
                    secondsToDisplay = 135 - secondsToDisplay;
                } else if(secondsToDisplay > 150)
                {
                    if(endgame)
                    {
                        soundEffects.addToQueue("matchEnd");
                    }
                    // match over
                    auto = false;
                    teleop = false;
                    endgame = false;
                    secondsToDisplay = 0;
                    inMatch = false;
                }
                timeStr = ((int)secondsToDisplay).ToString();
            } else
            {
                timeStr = "0";
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Vector2 origin = new Vector2(image.Width * 0.5f, image.Height * 0.5f);
            position = new Vector2(0, 1995);
            spriteBatch.Draw(image, position, null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
            Vector2 timePos = new Vector2((3840 * 0.5f)
                - (timerFont.MeasureString(timeStr).X * 0.5f),
                1810
                - (timerFont.MeasureString(timeStr).Y * 0.5f));
            spriteBatch.Draw(background, new Vector2((3840 * 0.5f) - (background.Width * 0.5f), 1595), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            if (endgame)
            {
                spriteBatch.Draw(yellowBarImage, new Vector2(15, 2010), null, Color.White, 0, Vector2.Zero, new Vector2((150 - secondsToDisplay) / 150f, 1), SpriteEffects.None, 0);
            } else if (teleop)
            {
                spriteBatch.Draw(greenBarImage, new Vector2(15, 2010), null, Color.White, 0, Vector2.Zero, new Vector2((150 - secondsToDisplay) / 150f, 1), SpriteEffects.None, 0);
            } else if (auto)
            {
                spriteBatch.Draw(greenBarImage, new Vector2(15, 2010), null, Color.White, 0, Vector2.Zero, new Vector2((15 - secondsToDisplay) / 150f, 1), SpriteEffects.None, 0);
            } else if (inMatch)
            {
                spriteBatch.Draw(redBarImage, new Vector2(15, 2010), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }

            if(!auto && !teleop && !endgame)
            {
                if(gameTime.TotalGameTime.TotalMilliseconds % 500 < 250)
                {
                    spriteBatch.DrawString(timerFont, timeStr, timePos, Color.White);
                }
            } else {
                spriteBatch.DrawString(timerFont, timeStr, timePos, Color.White);
            }
        }

        public void Reset(SoundEffects soundEffects)
        {
            inMatch = false;
            soundEffects.addToQueue("faultSound");
        }

        public void ResetClean()
        {
            inMatch = false;
            auto = false;
            teleop = false;
            endgame = false;
        }

        public void StartMatch(GameTime gameTime)
        {
            timeMatchStarted = (float)gameTime.TotalGameTime.TotalSeconds;
            inMatch = true;
            auto = true;
            matchJustStarted = true;
            secondsToDisplay = 15;
        }

        private string secondsToTime(int seconds)
        {
            int minutes = seconds / 60;
            seconds -= minutes * 60;
            return minutes.ToString() + ":" + seconds.ToString().PadLeft(2, '0');
        }
    }


}

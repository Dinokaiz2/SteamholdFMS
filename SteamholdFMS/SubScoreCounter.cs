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
    class SubScoreCounter
    {
        protected String control = "A";

        protected Vector2 position;
        GameTime gameTime;
        private int score = 0;
        public int Score
        {
            get { return score; }
            set { score = MathHelper.Clamp(value, 0, 9999999); }
        }
        static SpriteFont mySuperCoolFont;

        public SubScoreCounter(Vector2 position)
        {
            this.position = position;
        }

        public static void Load(ContentManager content)
        {
            mySuperCoolFont = content.Load<SpriteFont>("mysupercoolfont");
        }

        public void Increment()
        {
            Score++;
        }
        
        public void Deincrement()
        {
            Score--;
        }

        public void Reset()
        {
            Score = 0;
        }

        public void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
        }

        public void DrawText(SpriteBatch spriteBatch)
        {
            Vector2 scorePos = new Vector2(position.X + 425
                - (mySuperCoolFont.MeasureString(Score.ToString()).X * 0.5f),
                position.Y + 150
                - (mySuperCoolFont.MeasureString(Score.ToString()).Y * 0.5f));
            spriteBatch.DrawString(mySuperCoolFont, Score.ToString(), scorePos, Color.Black);
        }

        public void DrawRedControl(SpriteBatch spriteBatch)
        {
            Vector2 controlPos = new Vector2(position.X - 50
                - (mySuperCoolFont.MeasureString(control).X * 0.5f),
                position.Y + 150
                - (mySuperCoolFont.MeasureString(control).Y * 0.5f));
            spriteBatch.DrawString(mySuperCoolFont, control, controlPos, Color.Black);
        }

        public void DrawBlueControl(SpriteBatch spriteBatch)
        {
            Vector2 controlPos = new Vector2(position.X + 730
                - (mySuperCoolFont.MeasureString(control).X * 0.5f),
                position.Y + 150
                - (mySuperCoolFont.MeasureString(control).Y * 0.5f));
            spriteBatch.DrawString(mySuperCoolFont, control, controlPos, Color.Black);
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

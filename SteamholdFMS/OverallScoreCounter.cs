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
    class OverallScoreCounter
    {

        private static Texture2D image;

        int redScore;
        int blueScore;

        static SpriteFont font;

        Vector2 position;

        public static void Load(ContentManager content)
        {
            image = content.Load<Texture2D>("scorecounter");
            font = content.Load<SpriteFont>("timerfont");
        }

        public void Update(int redScore, int blueScore)
        {
            this.redScore = redScore;
            this.blueScore = blueScore;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            position = new Vector2((3840 * 0.5f) - (image.Width * 0.5f), 0);
            spriteBatch.Draw(image, position, null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
        }

        public void DrawText(SpriteBatch spriteBatch)
        {
            Vector2 redScorePos = new Vector2((3840 * 0.5f)
                - (font.MeasureString(redScore.ToString()).X * 0.5f)
                - 350,
                200
                - (font.MeasureString(redScore.ToString()).Y * 0.5f));
            spriteBatch.DrawString(font, redScore.ToString(), redScorePos, Color.White);

            Vector2 blueScorePos = new Vector2((3840 * 0.5f)
                - (font.MeasureString(blueScore.ToString()).X * 0.5f)
                + 350,
                200
                - (font.MeasureString(blueScore.ToString()).Y * 0.5f));
            spriteBatch.DrawString(font, blueScore.ToString(), blueScorePos, Color.White);

        }
    }
}

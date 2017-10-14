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
    class RPCounter
    {

        GameTime gameTime;
        
        private static Texture2D breachOffImage;
        private static Texture2D captureOffImage;
        private static Texture2D breachRedImage;
        private static Texture2D breachBlueImage;
        private static Texture2D captureMidRedImage;
        private static Texture2D captureMidBlueImage;
        private static Texture2D captureRedImage;
        private static Texture2D captureBlueImage;

        bool redBreach = false;
        bool blueBreach = false;
        bool redMidCapture = false;
        bool blueMidCapture = false;
        bool redCapture = false;
        bool blueCapture = false;

        public static void Load(ContentManager content)
        {
            breachOffImage = content.Load<Texture2D>("breachoff");
            captureOffImage = content.Load<Texture2D>("captureoff");
            breachRedImage = content.Load<Texture2D>("breachred");
            breachBlueImage = content.Load<Texture2D>("breachblue");
            captureMidRedImage = content.Load<Texture2D>("capturemidred");
            captureMidBlueImage = content.Load<Texture2D>("capturemidblue");
            captureRedImage = content.Load<Texture2D>("capturered");
            captureBlueImage = content.Load<Texture2D>("captureblue");
        }

        public void Update(bool redBreach, bool blueBreach, bool redMidCapture, bool blueMidCapture, bool redCapture, bool blueCapture)
        {
            this.redBreach = redBreach;
            this.blueBreach = blueBreach;
            this.redMidCapture = redMidCapture;
            this.blueMidCapture = blueMidCapture;
            this.redCapture = redCapture;
            this.blueCapture = blueCapture;
        }

        public void Reset()
        {
            redBreach = false;
            blueBreach = false;
            redMidCapture = false;
            blueMidCapture = false;
            redCapture = false;
            blueCapture = false;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (redBreach)
            {
                spriteBatch.Draw(breachRedImage, new Vector2(1175, 1650), null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
            } else
            {
                spriteBatch.Draw(breachOffImage, new Vector2(1175, 1650), null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
            }

            if (blueBreach)
            {
                spriteBatch.Draw(breachBlueImage, new Vector2(2287, 1650), null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
            } else
            {
                spriteBatch.Draw(breachOffImage, new Vector2(2287, 1650), null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
            }

            if (redCapture)
            {
                spriteBatch.Draw(captureRedImage, new Vector2(1175, 1820), null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
            } else if (redMidCapture)
            {
                spriteBatch.Draw(captureMidRedImage, new Vector2(1175, 1820), null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
            } else
            {
                spriteBatch.Draw(captureOffImage, new Vector2(1175, 1820), null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
            }

            if (blueCapture)
            {
                spriteBatch.Draw(captureBlueImage, new Vector2(2287, 1820), null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
            }
            else if (blueMidCapture)
            {
                spriteBatch.Draw(captureMidBlueImage, new Vector2(2287, 1820), null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(captureOffImage, new Vector2(2287, 1820), null, Color.White, 0,
                                Vector2.Zero,
                                1, SpriteEffects.None, 0);
            }
        }
    }
}

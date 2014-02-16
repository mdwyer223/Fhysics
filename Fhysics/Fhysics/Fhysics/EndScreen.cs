using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fhysics
{
    public class EndScreen
    {
        string endText;
        bool restart;
        SpriteFont font;

        public bool Restart
        {
            get{return restart;}
        }

        public EndScreen(int levels)
        {
            endText = Game1.LossText + "\nYou passed " + levels + " levels in\n" + Game1.Time + "\n\nPress enter to retry";
            font = Game1.GameContent.Load<SpriteFont>("Time");
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                restart = true;
                Game1.State = GameState.PLAYING;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, endText, new Vector2((Game1.DisplayWidth / 2) - 100, 50), Color.White);
        }
    }
}

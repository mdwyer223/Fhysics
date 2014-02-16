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
        SpriteFont font;

        public EndScreen(int levels)
        {
            endText = Game1.LossText + "\nYou passed " + levels + " levels in\n" + Game1.Time;
            font = Game1.GameContent.Load<SpriteFont>("Time");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, endText, Vector2.Zero, Color.White);
        }
    }
}

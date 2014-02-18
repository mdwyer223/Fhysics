using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fhysics
{
    public class Goal : GroundObj
    {
        bool tripped, countUp = false, countDown = true;
        int brightness = 255;
        public bool IsTripped
        {
            get { return tripped; }
            set { tripped = value; }
        }

        public Goal(Rectangle rec) 
            : base(rec) 
        {
            //255, 215, 0
            color = Color.Gold;
        }

        public override void Update(GameTime gameTime, Map data)
        {
            if (countDown)
            {
                brightness-=2;
                if (brightness <= 55)
                {
                    countUp = true;
                    countDown = false;
                }
            }
            else if (countUp)
            {
                brightness += 2;
                if (brightness >= 255)
                {
                    countDown = true;
                    countUp = false;
                }
            }
            
            base.Update(gameTime, data);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rec, 
                new Color(255, 215, 0) * (float)((Math.Abs(brightness) / 255f)));
            //spriteBatch.Draw(dark, new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight),
              //      new Color(255, 255, 255) * (float)((Math.Abs(brightnessValue) / 255f)));
            //base.Draw(spriteBatch);
        }
    }
}

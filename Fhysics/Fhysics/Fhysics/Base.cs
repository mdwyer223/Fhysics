using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fhysics
{
    public class Base
    {
        Texture2D texture;
        Vector2 velo; 
        Rectangle rec;
        Color color;

        public Vector2 Position
        {
            get { return new Vector2(rec.X, rec.Y); }
            set
            {
                rec.X = (int)(value.X + .5f); 
                rec.Y = (int)(value.Y + .5f);
            }
        }

        public Rectangle Rec
        {
            get { return rec; }
            set { rec = value; }
        }

        public Base(Rectangle rec)
        {
            this.rec = rec;
            texture = Game1.GameContent.Load<Texture2D>("white");
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rec, color);
        }


    }
}

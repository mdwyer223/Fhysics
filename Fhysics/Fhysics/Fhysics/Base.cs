﻿using System;
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
        protected Texture2D texture;
        protected Vector2 velo;  
        protected Rectangle rec;
        protected Color color = Color.White;

        public Vector2 Position
        {
            get { return new Vector2(rec.X, rec.Y); }
            set
            {
                rec.X = (int)(value.X + .5f); 
                rec.Y = (int)(value.Y + .5f);
            }
        }

        public Vector2 Center
        {
            get { return new Vector2(Rec.X + (rec.Width / 2), Rec.Y + (rec.Height / 2)); }
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

        public virtual void Update(GameTime gameTime, Map data)
        {
            Position += velo;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rec, color);
        }


    }
}

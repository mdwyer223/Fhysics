using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fhysics
{
    public class Particle : Base 
    {
        protected Vector2 accel;

        public bool OffScreen
        {
            get { return Position.X > Game1.DisplayWidth || Position.Y > Game1.DisplayHeight 
                || Position.Y + rec.Height < 0 || Position.X + rec.Width < 0; }
        }

        public Particle(Rectangle rec, Vector2 accel)
            : base(rec)
        {
            this.accel = accel;
        }

        public Particle(Rectangle rec, Vector2 velo, Color color):
            base(rec)
        {
            this.velo = velo;
            accel = Vector2.Zero;
            this.color = color;
        }

        public override void Update(GameTime gameTime, Map data)
        {
            velo += accel;
            base.Update(gameTime, data);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

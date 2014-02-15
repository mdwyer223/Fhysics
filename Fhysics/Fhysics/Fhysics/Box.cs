using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fhysics
{
    public enum Directions { RIGHT, LEFT, TOP, DOWN, ALL };

    public class Box : Base
    {
        List<Directions> directions;

        private bool isPush;

        public Box(Vector2 pos, List<Directions> d, bool isPush)
            : base(new Rectangle((int)pos.X, (int)pos.Y, 20, 20))
        {
            directions = d;
            this.isPush = isPush;
            if (isPush)
                color = Color.Red;
            else
                color = Color.Blue;
        }

        public override void Update(GameTime gameTime, Map data)
        {
            //check collision, player/boxes
            if (data.Player.Rec.Intersects(this.rec))
            {
                // check direction
                Position += data.Player.Velocity;

                // decided if move
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //might change colors
            //arrow rotation
            base.Draw(spriteBatch);
        }
    }
}

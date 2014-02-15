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

        public Box(Rectangle rec, List<Directions> d)
            : base(rec)
        {
            directions = d;
        }

        public override void Update(GameTime gameTime)
        {
            //check collision, player/boxes
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //might change colors
            //arrow
            base.Draw(spriteBatch);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fhysics
{
    public class Wall : Base
    {
        public Wall(Rectangle rec)
            : base(rec)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Dont draw
        }
    }
}

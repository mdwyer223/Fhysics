using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fhysics
{
    public class Player : Base
    {
        KeyboardState keys, oldKeys;

        public Player(Rectangle rec, Color color)
            :base(rec)
        {
            keys = oldKeys = Keyboard.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            // movement code
            // check for death
        }

    }
}

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

        public Player(Vector2 pos, Color color)
            :base(new Rectangle((int)pos.X, (int)pos.Y, 20,20))
        {
            keys = oldKeys = Keyboard.GetState();
            this.color = color;
        }

        public override void Update(GameTime gameTime)
        {
            // movement code
            // check for death
        }

    }
}

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
        private bool isDead;

        KeyboardState keys, oldKeys;

        public Player(Vector2 pos, Color color)
            : base(new Rectangle((int)pos.X, (int)pos.Y, 20, 20))
        {
            keys = oldKeys = Keyboard.GetState();
            this.color = color;
        }

        public override void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();

            // Movement code
            const int SPEED = 3;
            if (keys.IsKeyDown(Keys.W))
            {
                velo = new Vector2(0, -SPEED);
            }
            else if (keys.IsKeyDown(Keys.A))
            {
                velo = new Vector2(-SPEED, 0);
            }
            else if (keys.IsKeyDown(Keys.S))
            {
                velo = new Vector2(0, SPEED);
            }
            else if (keys.IsKeyDown(Keys.D))
            {
                velo = new Vector2(SPEED, 0);
            }
            else
            {
                //ice
                //vel*= .95f;
                velo = Vector2.Zero;
            }            
            
            // check for death
            // if half over a gap
            // isDead = true
        }

    }
}

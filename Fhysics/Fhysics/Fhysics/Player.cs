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
        private bool isDead, onIce;
        private int level;

        KeyboardState keys, oldKeys;

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        public int Level
        {
            get { return level; }
        }

        public Vector2 Velocity
        {
            get { return velo; }
        }

        public Player(Vector2 pos, Color color)
            : base(new Rectangle((int)pos.X, (int)pos.Y, 20, 20))
        {
            keys = oldKeys = Keyboard.GetState();
            this.color = color;
        }

        public override void Update(GameTime gameTime, Map data)
        {
            keys = Keyboard.GetState();
            if (isDead)
            {
                Game1.State = GameState.END;
                return;
            }
            else
            {
                Game1.LevelPassed = level;
            }
            List<Base> o = data.Data.AllObjects;
            for(int i = 0; i<o.Count;i++)
            {
                if(o[i] != null && o[i].GetType() == typeof(IceStrip))
                {
                    if (o[i].Rec.Intersects(Rec))
                    {
                        onIce = true;
                    }
                    else
                    {
                        onIce = false;
                    }
                }
            }

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
                if (onIce)
                {
                    velo *= .95f;
                }
                else
                {
                    velo = Vector2.Zero;
                }
            }
            
            
            // check for death
            // if half over a gap
            // isDead = true
            
            base.Update(gameTime, data);
            oldKeys = keys;
        }

        public void levelUp()
        {
            level++;
        }
    }
}

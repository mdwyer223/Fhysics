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

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Player(Vector2 pos, Color color)
            : base(new Rectangle((int)pos.X, (int)pos.Y, 20, 20))
        {
            keys = oldKeys = Keyboard.GetState();
            this.color = color;
        }

        public override void Update(GameTime gameTime, Map data)
        {
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

            keys = Keyboard.GetState();

            // Movement code
            const int SPEED = 3;
            if (keys.IsKeyDown(Keys.W) ||
                keys.IsKeyDown(Keys.A) ||
                keys.IsKeyDown(Keys.S) ||
                keys.IsKeyDown(Keys.D))
            {
                if (keys.IsKeyDown(Keys.W) || keys.IsKeyDown(Keys.S))
                {
                    if (keys.IsKeyDown(Keys.W))
                    {
                        velo.Y = -SPEED;
                    }

                    if (keys.IsKeyDown(Keys.S))
                    {
                        velo.Y = SPEED;
                    }
                }
                else
                {
                    if (!onIce)
                        velo.Y = 0;
                }

                if (keys.IsKeyDown(Keys.A) || keys.IsKeyDown(Keys.D))
                {
                    if (keys.IsKeyDown(Keys.A))
                    {
                        velo.X = -SPEED;
                    }
                    if (keys.IsKeyDown(Keys.D))
                    {
                        velo.X = SPEED;
                    }
                }
                else
                {
                    if (!onIce)
                        velo.X = 0;
                }
            }
            else
            {
                if(!onIce)
                    velo = Vector2.Zero;
            }

            Rectangle futureRec = new Rectangle((int)(Rec.X + velo.X), (int)(Rec.Y + velo.Y), Rec.Width, Rec.Height);
            if (futureRec.X < 0 || futureRec.X + futureRec.Width > Game1.DisplayWidth)
            {
                velo.X = 0;
            }
            if (futureRec.Y < 0 || futureRec.Y + futureRec.Height > Game1.DisplayHeight)
            {
                velo.Y = 0;
            }
            if (onIce)
            {
                velo *= .95f;
            }

            foreach (Base obj in data.Data.AllObjects)
                if (obj.GetType() == typeof(Box) &&
                    futureRec.Intersects(obj.Rec))
                {
                    Box box = (Box)obj;
                    if (box.Direcs.Contains(Directions.ALL))
                    {
                        Rectangle innerBoxRec = new Rectangle(box.Rec.X + 2, box.Rec.Y + 2, box.Rec.Width - 4, box.Rec.Height - 4);

                        if (Rec.Intersects(innerBoxRec))
                        {
                            velo = Vector2.Zero;
                        }

                    }
                    else if (box.Direcs.Contains(Directions.NONE) ||
                        box.Direcs.Contains(Directions.TOP) && Rec.Y <= box.Rec.Y + box.Rec.Height - 3 ||
                        box.Direcs.Contains(Directions.DOWN) && Rec.Y + Rec.Height >= box.Rec.Y + 3 ||
                        box.Direcs.Contains(Directions.LEFT) && Rec.X <= box.Rec.X + box.Rec.Width - 3 ||
                        box.Direcs.Contains(Directions.RIGHT) && Rec.X + Rec.Width >= box.Rec.X + 3)
                    {
                        velo = Vector2.Zero;
                    }
                    
                }
            
            base.Update(gameTime, data);
            oldKeys = keys;
        }

        public void levelUp()
        {
            level++;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isDead)
                base.Draw(spriteBatch);
        }
    }
}

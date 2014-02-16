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

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
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
            if (isDead)
                return;

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
                    velo.Y = 0;

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
                    velo.X = 0;
            }
            else
            {
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

            foreach (Base obj in data.Data.AllObjects)
                if (obj.GetType() == typeof(Box) &&
                    futureRec.Intersects(obj.Rec))
                {
                    Box box = (Box)obj;
                    if (box.Direcs.Contains(Directions.ALL))
                    {
                        //Rectangle top, left, bottom, right;
                        //top = new Rectangle(futureRec.X + 3, futureRec.Y - 10, futureRec.Width - 6, 10);
                        //bottom = new Rectangle(futureRec.X + 3, futureRec.Y + futureRec.Height, futureRec.Width - 6, 10);
                        //left = new Rectangle(futureRec.X - 10, futureRec.Y + 3, 10, futureRec.Height - 6);
                        //right = new Rectangle(futureRec.X + futureRec.Height, futureRec.Y + 3, 10, futureRec.Height - 6);
                        Rectangle innerBoxRec = new Rectangle(box.Rec.X + 2, box.Rec.Y + 2, box.Rec.Width - 4, box.Rec.Height - 4);

                        if (futureRec.Intersects(innerBoxRec))
                        {
                            velo = Vector2.Zero;
                        }

                        //if (!box.CanMoveUp || !box.CanMoveDown)
                        //    velo.Y = 0;
                        //if (!box.CanMoveLeft || !box.CanMoveRight)
                        //    velo.X = 0;
                    }
                    else if (box.Direcs.Contains(Directions.NONE) ||
                        box.Direcs.Contains(Directions.TOP) && Rec.Y <= box.Rec.Y + box.Rec.Height ||
                        box.Direcs.Contains(Directions.DOWN) && Rec.Y + Rec.Height > box.Rec.Y ||
                        box.Direcs.Contains(Directions.LEFT) && Rec.X < box.Rec.X + box.Rec.Width ||
                        box.Direcs.Contains(Directions.RIGHT) && Rec.X + Rec.Width >= box.Rec.X)
                    {
                        velo = Vector2.Zero;
                    }
                    
                }

            // check for death
            // if gap dead = true
            // if ice vel *= .95
            
            base.Update(gameTime, data);
            oldKeys = keys;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isDead)
                base.Draw(spriteBatch);
        }

        public void cancelVelo()
        {
            velo = Vector2.Zero;
        }

    }
}

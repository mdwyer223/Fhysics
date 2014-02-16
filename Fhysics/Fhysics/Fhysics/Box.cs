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
    public enum Directions { RIGHT, LEFT, TOP, DOWN, ALL, NONE };

    public class Box : Base
    {
        List<Directions> directions;
        KeyboardState keys, oldKeys;

        Rectangle futureRec;
        private bool isPush;

        public Directions[] Direcs
        {
            get { return directions.ToArray(); }
        }

        public Box(Vector2 pos, Directions d, bool isPush)
            : base(new Rectangle((int)pos.X, (int)pos.Y, 20, 20))
        {
            directions = new List<Directions>();
            directions.Add(d);
            this.isPush = isPush;
            if (isPush)
                color = Color.Red;
            else
                color = Color.Blue;

        }

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
            Rectangle playerRec = data.Player.Rec;
            keys = Keyboard.GetState();
            if (isPush)
            {
                if (playerRec.Intersects(this.rec))
                {
                    if (directions.Contains(Directions.TOP) || directions.Contains(Directions.DOWN) || directions.Contains(Directions.ALL))
                    {
                        velo.Y = data.Player.Velocity.Y;
                    }
                    if (directions.Contains(Directions.RIGHT) || directions.Contains(Directions.LEFT) || directions.Contains(Directions.ALL))
                    {
                        velo.X = data.Player.Velocity.X;
                    }
                }
                else
                {
                    velo = Vector2.Zero;
                }
            }
            else
            {
                Rectangle top, left, bottom, right;
                top = new Rectangle(Rec.X + 4, Rec.Y - 10, Rec.Width - 8, 10);
                bottom = new Rectangle(Rec.X + 4, Rec.Y + Rec.Height, Rec.Width - 8, 10);

                left = new Rectangle(Rec.X - 10, Rec.Y + 4, 10, Rec.Height - 8);
                right = new Rectangle(Rec.X + Rec.Width, Rec.Y + 4, 10, Rec.Height - 8);

                if (keys.IsKeyDown(Keys.LeftShift))
                {
                    if (directions.Contains(Directions.LEFT) && playerRec.Intersects(left) || directions.Contains(Directions.RIGHT) && playerRec.Intersects(right))
                    {
                        velo.X = data.Player.Velocity.X;
                    }
                    if (directions.Contains(Directions.TOP) && playerRec.Intersects(top) || directions.Contains(Directions.DOWN) && playerRec.Intersects(bottom))
                    {
                        velo.Y = data.Player.Velocity.Y;
                    }
                }
                else
                    velo = Vector2.Zero;

            }


            futureRec = new Rectangle((int)(rec.X + velo.X), (int)(rec.Y + velo.Y), rec.Width, rec.Height);
            if (futureRec.X < 0 || futureRec.X + futureRec.Width > Game1.DisplayWidth)
            {
                velo.X = 0;
            }
            if (futureRec.Y < 0 || futureRec.Y + futureRec.Height > Game1.DisplayHeight)
            {
                velo.Y = 0;
            }

            if (directions.Contains(Directions.ALL))
            {
                foreach (Base obj in data.Data.AllObjects)
                {
                    if (!obj.Equals(this) && obj.GetType() == typeof(Box) && 
                        !obj.GetType().IsSubclassOf(typeof(GroundObj)))
                    {
                        Rectangle top, left, bottom, right;
                        top = new Rectangle(Rec.X + 4, Rec.Y - 2, Rec.Width - 8, 2);
                        bottom = new Rectangle(Rec.X + 4, Rec.Y + Rec.Height, Rec.Width - 8, 2);

                        left = new Rectangle(Rec.X - 2, Rec.Y + 4, 2, Rec.Height - 8);
                        right = new Rectangle(Rec.X + Rec.Width, Rec.Y + 4, 1, Rec.Height - 8);


                        if (obj.Rec.Intersects(top))
                        {
                            if (velo.Y < 0)
                                velo.Y = 0;
                        }
                        if (obj.Rec.Intersects(bottom))
                        {
                            if (velo.Y > 0)
                                velo.Y = 0;
                        }
                        if (obj.Rec.Intersects(left))
                        {
                            if (velo.X < 0)
                                velo.X = 0;
                        }
                        if (obj.Rec.Intersects(right))
                        {
                            if (velo.X > 0)
                                velo.X = 0;
                        }
                    }
                }

            }
            else
                foreach (Base obj in data.Data.AllObjects)
                    if (!obj.Equals(this) && obj.GetType() == typeof(Box) &&
                        !obj.GetType().IsSubclassOf(typeof(GroundObj)) &&
                        futureRec.Intersects(obj.Rec))
                    {                 
                        velo = Vector2.Zero;
                    }
            oldKeys = keys;
            base.Update(gameTime, data);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (directions.Contains(Directions.LEFT) || directions.Contains(Directions.ALL))
                spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("ArrowLeft"), rec, color);
            if (directions.Contains(Directions.RIGHT) || directions.Contains(Directions.ALL))
                spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("ArrowRight"), rec, color);
            if (directions.Contains(Directions.TOP) || directions.Contains(Directions.ALL))
                spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("ArrowUp"), rec, color);
            if (directions.Contains(Directions.DOWN) || directions.Contains(Directions.ALL))
                spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("ArrowDown"), rec, color);
        }
    }
}

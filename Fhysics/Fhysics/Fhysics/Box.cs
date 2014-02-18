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
        
        public bool canMoveUp, canMoveDown, canMoveRight, canMoveLeft;
        private int invisRecWidth;

        public Rectangle TopRec
        {
            get { return new Rectangle(Rec.X + 1, Rec.Y - invisRecWidth, Rec.Width - 2, invisRecWidth); }
        }

        public Rectangle DownRec
        {
            get { return new Rectangle(Rec.X + 1, Rec.Y + Rec.Height, Rec.Width - 2, invisRecWidth); }
        }

        public Rectangle LeftRec
        {
            get { return new Rectangle(Rec.X - invisRecWidth, Rec.Y + 1, invisRecWidth, Rec.Height - 2); }
        }

        public Rectangle RightRec
        {
            get { return new Rectangle(Rec.X + Rec.Width, Rec.Y + 1, invisRecWidth, Rec.Height - 2); }
        }

        private bool isPush;
        public bool IsPush
        {
            get { return isPush; }
        }

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
                invisRecWidth = 3;
                Rectangle outerRec = new Rectangle(LeftRec.X, TopRec.Y, Rec.Width + LeftRec.Width + RightRec.Width, Rec.Height + TopRec.Height + DownRec.Height);                

                if (directions.Contains(Directions.TOP) && playerRec.Intersects(DownRec) ||
                    directions.Contains(Directions.DOWN) && playerRec.Intersects(TopRec) ||
                    directions.Contains(Directions.ALL) && (playerRec.Intersects(DownRec) || playerRec.Intersects(TopRec)))
                {
                    velo.Y = data.Player.Velocity.Y;
                }
                else
                {
                    velo.Y = 0;
                }

                if (directions.Contains(Directions.RIGHT) && playerRec.Intersects(LeftRec) ||
                    directions.Contains(Directions.LEFT) && playerRec.Intersects(RightRec) ||
                    directions.Contains(Directions.ALL) && (playerRec.Intersects(LeftRec) || playerRec.Intersects(RightRec)))
                {
                    velo.X = data.Player.Velocity.X;
                }
                else
                {
                    velo.X = 0;
                }
            }
            else
            {
                invisRecWidth = 7;
                if (keys.IsKeyDown(Keys.LeftShift))
                {
                    if (directions.Contains(Directions.LEFT) && playerRec.Intersects(LeftRec) || 
                        directions.Contains(Directions.RIGHT) && playerRec.Intersects(RightRec) || directions.Contains(Directions.ALL))
                    {
                        velo.X = data.Player.Velocity.X;
                    }
                    else
                    {
                        velo.X = 0;
                    }

                    if (directions.Contains(Directions.TOP) && playerRec.Intersects(TopRec) || 
                        directions.Contains(Directions.DOWN) && playerRec.Intersects(DownRec) || directions.Contains(Directions.ALL))
                    {
                        velo.Y = data.Player.Velocity.Y;
                    }
                    else
                    {
                        velo.Y = 0;
                    }
                }
                else
                    velo = Vector2.Zero;

            }

            canMoveUp = canMoveDown = canMoveLeft = canMoveRight = true;

            futureRec = new Rectangle((int)(rec.X + velo.X), (int)(rec.Y + velo.Y), rec.Width, rec.Height);            
            if (futureRec.X < 0 || futureRec.X + futureRec.Width > Game1.DisplayWidth)
            {
                velo.X = 0;
                canMoveLeft = futureRec.X > 0;
                canMoveRight = futureRec.X + futureRec.Width <= Game1.DisplayWidth;
            }
            if (futureRec.Y < 0 || futureRec.Y + futureRec.Height > Game1.DisplayHeight)
            {
                velo.Y = 0;
                canMoveUp = futureRec.Y > 0;
                canMoveDown = futureRec.Y + futureRec.Height <= Game1.DisplayHeight;
            }

            foreach (Base obj in data.Data.AllObjects)
            {
                if (!obj.Equals(this) && obj.GetType() == typeof(Box) &&
                    !obj.GetType().IsSubclassOf(typeof(GroundObj)))
                {
                    if (IsPush)
                    {

                        if (directions.Contains(Directions.TOP) || directions.Contains(Directions.ALL))
                            canMoveUp = canMoveUp && !obj.Rec.Intersects(TopRec);
                        else
                            canMoveUp = false;

                        if (directions.Contains(Directions.DOWN) || directions.Contains(Directions.ALL))
                            canMoveDown = canMoveDown && !obj.Rec.Intersects(DownRec);
                        else
                            canMoveDown = false;

                        if (directions.Contains(Directions.LEFT) || directions.Contains(Directions.ALL))
                            canMoveLeft = canMoveLeft && !obj.Rec.Intersects(LeftRec);
                        else
                            canMoveLeft = false;

                        if (directions.Contains(Directions.RIGHT) || directions.Contains(Directions.ALL))
                            canMoveRight = canMoveRight && !obj.Rec.Intersects(RightRec);
                        else
                            canMoveRight = false;
                    }
                    else
                    {
                        if (directions.Contains(Directions.DOWN) || directions.Contains(Directions.ALL))
                            canMoveDown = playerRec.Intersects(DownRec);
                        else
                            canMoveDown = false;

                        if (directions.Contains(Directions.TOP) || directions.Contains(Directions.ALL))
                            canMoveUp = playerRec.Intersects(TopRec);
                        else
                            canMoveUp = false;

                        if (directions.Contains(Directions.LEFT) || directions.Contains(Directions.ALL))
                            canMoveLeft = playerRec.Intersects(LeftRec);
                        else
                            canMoveLeft = false;

                        if (directions.Contains(Directions.RIGHT) || directions.Contains(Directions.ALL))
                            canMoveRight = playerRec.Intersects(RightRec);
                        else
                            canMoveRight = false;
                    }
                        

                }
            }

            if (!canMoveDown && velo.Y > 0)
                velo.Y = 0;
            if (!canMoveUp && velo.Y < 0)
                velo.Y = 0;
            if (!canMoveLeft && velo.X < 0)
                velo.X = 0;
            if (!canMoveRight && velo.X > 0)
                velo.X = 0;

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

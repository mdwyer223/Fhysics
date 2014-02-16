﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fhysics
{
    public enum Directions { RIGHT, LEFT, TOP, DOWN, ALL, NONE };

    public class Box : Base
    {
        List<Directions> directions;
        Vector2 oldVelo;

        Rectangle futureRec;
        private bool isPush;

        public bool CanMoveUp
        {
            get;
            set;
        }

        public bool CanMoveDown
        {
            get;
            set;
        }

        public bool CanMoveLeft
        {
            get;
            set;
        }

        public bool CanMoveRight
        {
            get;
            set;
        }

        public Directions[] Direcs
        {
            get { return directions.ToArray(); }
        }

        public Box(Vector2 pos, Directions d, bool isPush)
            : base(new Rectangle((int)pos.X, (int)pos.Y, 20, 20))
        {
            CanMoveDown = CanMoveUp = CanMoveLeft = CanMoveRight = true;
            directions = new List<Directions>();
            directions.Add(d);
            this.isPush = isPush;
            if (isPush)
                color = Color.Red;
            else
                color = Color.Blue;

            if (directions.Contains(Directions.NONE))
                color = Color.Black;
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

            if (directions.Contains(Directions.NONE))
                color = Color.Black;
        }

        public override void Update(GameTime gameTime, Map data)
        {
            Rectangle playerRec = data.Player.Rec;
            
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


            futureRec = new Rectangle((int)(rec.X + velo.X), (int)(rec.Y + velo.Y), rec.Width, rec.Height); 
            if (directions.Contains(Directions.ALL))
            {
                foreach (Base obj in data.Data.AllObjects)
                {
                    if (!obj.Equals(this) && obj.GetType() == typeof(Box))
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
                        futureRec.Intersects(obj.Rec))
                    {                 
                        velo = Vector2.Zero;
                    }

            oldVelo = velo;
            base.Update(gameTime, data);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //might change colors
            //arrow rotation
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

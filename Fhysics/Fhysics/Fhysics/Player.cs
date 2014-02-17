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

        public bool canMoveUp, canMoveDown, canMoveRight, canMoveLeft;

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
            onIce = false;
            List<Base> o = data.Data.AllObjects;
            for(int i = 0; i<o.Count;i++)
            {
                if(o[i] != null && o[i].GetType() == typeof(IceStrip))
                {
                    onIce = onIce || o[i].Rec.Intersects(Rec);
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
                    if (keys.IsKeyDown(Keys.W) && canMoveUp)
                    {
                        velo.Y = -SPEED;
                    }

                    if (keys.IsKeyDown(Keys.S) && canMoveDown)
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
                    if (keys.IsKeyDown(Keys.A) && canMoveLeft)
                    {
                        velo.X = -SPEED;
                    }
                    if (keys.IsKeyDown(Keys.D) && canMoveRight)
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


            canMoveDown = canMoveUp = canMoveLeft = canMoveRight = true;
            Rectangle futureRec = new Rectangle((int)(Rec.X + velo.X), (int)(Rec.Y + velo.Y), Rec.Width, Rec.Height);           
            if (futureRec.X < 0 || futureRec.X + futureRec.Width > Game1.DisplayWidth)
            {
                velo.X = 0;
                canMoveLeft = futureRec.X > 0;
                canMoveRight = futureRec.X + futureRec.Width < Game1.DisplayWidth;
            }
            if (futureRec.Y < 0 || futureRec.Y + futureRec.Height > Game1.DisplayHeight)
            {
                velo.Y = 0;
                canMoveUp = futureRec.Y > 0;
                canMoveDown = futureRec.Y + futureRec.Height < Game1.DisplayHeight;
            }
            if (onIce)
            {
                velo *= .95f;
            }

            foreach (Base obj in data.Data.AllObjects)
                if (obj.Rec.Intersects(futureRec) && obj != null && obj.GetType() == typeof(Fan))
                {
                    velo = Vector2.Zero;
                }
                else if (obj.GetType() == typeof(Box) &&
                    futureRec.Intersects(obj.Rec))
                {
                    Box box = (Box)obj;
                    //if (box.Direcs.Contains(Directions.NONE))
                    //{
                        velo = Vector2.Zero;
                        canMoveDown = canMoveUp = canMoveLeft = canMoveRight = false;
                        
                    //}

                    //if (box.IsPush)
                    //{
                    //    //Rectangle innerRec = new Rectangle(box.Rec.X + 2, box.Rec.Y + 2, box.Rec.X + box.Rec.Width - 4, box.Rec.Y + box.Rec.Height - 4);
                        

                    //    if (box.Direcs.Contains(Directions.TOP) || box.Direcs.Contains(Directions.ALL))
                    //        canMoveUp = canMoveUp && box.canMoveUp;
                    //    else
                    //        canMoveUp = false;

                    //    if (box.Direcs.Contains(Directions.DOWN) || box.Direcs.Contains(Directions.ALL))
                    //        canMoveDown = canMoveDown && box.canMoveDown;
                    //    else
                    //        canMoveDown = false;

                    //    if (box.Direcs.Contains(Directions.LEFT) || box.Direcs.Contains(Directions.ALL))
                    //        canMoveLeft = canMoveLeft && box.canMoveLeft;
                    //    else
                    //        canMoveLeft = false;

                    //    if (box.Direcs.Contains(Directions.RIGHT) || box.Direcs.Contains(Directions.ALL))
                    //        canMoveRight = canMoveRight && box.canMoveRight;
                    //    else
                    //        canMoveRight = false;
                    //}
                    //else
                    //{
                        
                    //    if (!keys.IsKeyDown(Keys.LeftShift) || futureRec.Intersects(box.Rec))                        
                    //    {
                    //        velo = Vector2.Zero;
                    //        canMoveDown = canMoveUp = canMoveLeft = canMoveRight = false;
                    //        break;
                    //    }

                    //    canMoveDown = Rec.Intersects(box.DownRec) && box.Direcs.Contains(Directions.DOWN) || box.Direcs.Contains(Directions.ALL);

                    //    canMoveUp = Rec.Intersects(box.TopRec) && box.Direcs.Contains(Directions.TOP) || box.Direcs.Contains(Directions.ALL);

                    //    canMoveRight = Rec.Intersects(box.RightRec) && box.Direcs.Contains(Directions.RIGHT) || box.Direcs.Contains(Directions.ALL);

                    //    canMoveLeft = Rec.Intersects(box.LeftRec) && box.Direcs.Contains(Directions.LEFT) || box.Direcs.Contains(Directions.ALL);
                            
                    //}

                    
                }

            if (!canMoveDown && velo.Y > 0)
                velo.Y = 0;
            if (!canMoveUp && velo.Y < 0)
                velo.Y = 0;
            if (!canMoveLeft && velo.X < 0)
                velo.X = 0;
            if (!canMoveRight && velo.X > 0)
                velo.X = 0;
            
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

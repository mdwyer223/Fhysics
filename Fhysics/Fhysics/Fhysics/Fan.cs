using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fhysics
{
    public enum Orientation { RIGHT, LEFT, UP, DOWN };

    public class Fan: Base
    {
        Orientation direction;
        List<Particle> particles;
        Vector2 appliedVelo;

        Rectangle zone, oldZone;

        int spawnTime, spawnTimer;
        const int MAX_SIZE = 4, MIN_SIZE = 2;

        bool objInZone;

        public Fan(Rectangle rec, Orientation d)
            : base(rec)
        {
            direction = d;
            particles = new List<Particle>();
            spawnTime = spawnTimer = 0;

            if (d == Orientation.RIGHT)
            {
                zone = new Rectangle(rec.X, rec.Y, Game1.DisplayWidth - rec.X, rec.Height);
                appliedVelo = new Vector2(5, 0);
            }
            else if (d == Orientation.LEFT)
            {
                zone = new Rectangle(0, rec.Y, rec.X, rec.Height);
                appliedVelo = new Vector2(-5, 0);
            }
            else if (d == Orientation.UP)
            {
                zone = new Rectangle(rec.X, 0, rec.Width, rec.Y);
                appliedVelo = new Vector2(0, -5);
            }
            else if (d == Orientation.DOWN)
            {
                zone = new Rectangle(rec.X, rec.Y, rec.Width, Game1.DisplayHeight - (rec.Y + rec.Height));
                appliedVelo = new Vector2(0, 5);
            }

            oldZone = zone;
        }

        public override void Update(GameTime gameTime, Map data)
        {
            objInZone = false;

            if(spawnTimer < spawnTime)
            {
                spawnTimer++;
            }
            else
            {
                spawnTimer =0;
                addParticle();
            }

            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i] != null)
                {
                    particles[i].Update(gameTime, data);
                    if (particles[i].OffScreen)
                    {
                        particles.RemoveAt(i);
                    }
                }
            }
            List<Base> objs = data.Data.AllObjects;
            for (int j = 0; j < objs.Count; j++)
            {
                if (objs[j].GetType() == typeof(Box))
                {
                    Box b = (Box)objs[j];
                    if (b.Rec.Intersects(zone))
                    {
                        switch (this.direction)
                        {
                            case Orientation.UP:
                                {
                                    if (b.Direcs.Contains(Directions.DOWN))
                                    {
                                        zone.Y = b.Rec.Y;
                                        zone.Height = rec.Y - zone.Y;
                                        objInZone = true;
                                    }
                                    else if (b.Direcs.Contains(Directions.NONE))
                                    {
                                        //does nothing
                                    }
                                    else
                                    {
                                        b.Position += appliedVelo;
                                    }
                                    break;
                                }
                            case Orientation.DOWN:
                                {
                                    if (b.Direcs.Contains(Directions.TOP))
                                    {
                                        zone.Height = b.Rec.Y - zone.Y;
                                        objInZone = true;
                                    }
                                    else if (b.Direcs.Contains(Directions.NONE))
                                    {
                                        //does nothing
                                    }
                                    else
                                    {
                                        b.Position += appliedVelo;
                                    }
                                    break;
                                }
                            case Orientation.RIGHT:
                                {
                                    if (b.Direcs.Contains(Directions.LEFT))
                                    {
                                        zone.Width = (b.Rec.X + (b.Rec.Width / 2)) - zone.X;
                                        objInZone = true;
                                    }
                                    else if (b.Direcs.Contains(Directions.NONE))
                                    {
                                        //does nothing
                                    }
                                    else
                                    {
                                        b.Position += appliedVelo;
                                    }
                                    break;
                                }
                            case Orientation.LEFT:
                                {
                                    if (b.Direcs.Contains(Directions.RIGHT))
                                    {
                                        zone.X = b.Rec.X;
                                        zone.Width = rec.X - b.Rec.X;
                                        objInZone = true;
                                    }
                                    else if (b.Direcs.Contains(Directions.NONE))
                                    {
                                        //does nothing
                                    }
                                    else
                                    {
                                        b.Position += appliedVelo;
                                    }
                                    break;
                                }
                        }
                    }
                }
                //if (objs[j].GetType() != typeof(Fan) &&
                //    objs[j].GetType() != typeof(Gap))
                //{
                //    if (objs[j].Rec.Intersects(zone))
                //    {
                //        objs[j].Position += appliedVelo;
                //    }
                //}
            }
            if (!objInZone)
            {
                zone = oldZone;
            }
            //base.Update(gameTime, data);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in particles)
            {
                if (p != null)
                {
                    p.Draw(spriteBatch);
                }
            }
            base.Draw(spriteBatch);
        }

        protected void addParticle()
        {
            Random rand = new Random();
            Particle p;
            Vector2 accel = Vector2.Zero;
            spawnTime = rand.Next(10, 30);

            int x = Game1.DisplayWidth, y = Game1.DisplayHeight, size;
            size = rand.Next(MIN_SIZE, MAX_SIZE);
            if (direction == Orientation.UP || direction == Orientation.DOWN)
            {
                x = rand.Next(Rec.X, Rec.X + Rec.Width);
                y = Rec.Y;
            }
            else if (direction == Orientation.LEFT || direction == Orientation.RIGHT)
            {
                x = Rec.X;
                y = rand.Next(Rec.Y, Rec.Y + Rec.Height);
            }
            if (direction == Orientation.UP)
            {
                accel = new Vector2(0, (float)(-rand.NextDouble() * .01f));
            }
            else if (direction == Orientation.DOWN)
            {
                accel = new Vector2(0, (float)(rand.NextDouble() * .01f));
            }
            else if (direction == Orientation.RIGHT)
            {
                accel = new Vector2((float)(rand.NextDouble() * .01f), 0);
            }
            else if (direction == Orientation.LEFT)
            {
                accel = new Vector2((float)(-rand.NextDouble() * .01f), 0);
            }

            p = new Particle(new Rectangle((int)x, (int)y, size, size), accel);
            particles.Add(p);
        }
    }
}

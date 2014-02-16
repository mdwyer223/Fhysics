using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fhysics
{
    public class Turret : Base
    {
        protected Vector2 projVelo;
        List<Particle> projectiles;
        protected int speed = 5;

        int attackTime, attackTimer;
        public Turret(Rectangle rec)
            : base(rec)
        {
            color = Color.Orange;
            attackTimer = 0;
            attackTime = 150;
            projectiles = new List<Particle>();
            texture = Game1.GameContent.Load<Texture2D>("turretR");
        }

        public override void Update(GameTime gameTime, Map data)
        {
            projVelo = Vector2.Normalize((data.Player.Position - this.Position)) * speed;
            if(attackTimer< attackTime)
            {
                attackTimer ++;
            }
            else{
                attackTimer = 0;
                addShot(data.Player);
            }
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (projectiles[i] != null)
                {
                    projectiles[i].Update(gameTime, data);
                    List<Base> objs = data.Data.AllObjects;
                    for (int j = 0; j < objs.Count; j++)
                    {
                        if (objs[j] != null && !objs[j].Equals(this) && objs.GetType() != typeof(Player))
                        {
                            if (projectiles[i].Rec.Intersects(objs[j].Rec))
                            {
                                projectiles.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    if (projectiles.Count == 0)
                        break;
                    if (projectiles[i].Rec.Intersects(data.Player.Rec))
                    {
                        data.Player.IsDead = true;
                        Game1.LossText = "You've been shot!";
                    }
                    if (projectiles[i].OffScreen)
                    {
                        projectiles.RemoveAt(i);
                    }  
                }
            }
            base.Update(gameTime, data);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in projectiles)
            {
                if (p != null)
                {
                    p.Draw(spriteBatch);
                }
            }
            spriteBatch.Draw(texture, rec, null, color, (float)(Math.Atan2(-projVelo.X, projVelo.Y)), 
                new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0);
            //base.Draw(spriteBatch);
        }

        protected void addShot(Player player)
        {
            Random rand = new Random();
            attackTime = rand.Next(100, 150);

            Particle p = new Particle(new Rectangle(this.Rec.Center.X, this.Rec.Center.Y, 3, 3), 
                Vector2.Normalize((player.Position - this.Position)) * speed, Color.Orange);
            projectiles.Add(p);
        }

    }
}

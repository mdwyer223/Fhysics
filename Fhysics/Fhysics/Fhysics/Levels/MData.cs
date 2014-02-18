using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fhysics
{
    public class MData
    {
        List<Base> objs;
        SpriteFont font;
        protected bool passed;
        protected string messageText;
        protected Vector2 textPos;

        public List<Base> AllObjects
        {
            get { return objs; }
        }

        public MData()
        {
            objs = new List<Base>();
            font = Game1.GameContent.Load<SpriteFont>("Message");
            addObject(new Wall(new Rectangle(-20, 0, 20, Game1.DisplayHeight)));
            addObject(new Wall(new Rectangle(Game1.DisplayWidth, 0, 20, Game1.DisplayHeight)));
            addObject(new Wall(new Rectangle(0, -20, Game1.DisplayWidth, 20)));
            addObject(new Wall(new Rectangle(0, Game1.DisplayHeight, Game1.DisplayWidth, 20)));
        }

        public MData(Player p)
        {
            objs = new List<Base>();
            font = Game1.GameContent.Load<SpriteFont>("Message");
            addObject(new Wall(new Rectangle(-20, 0, 20, Game1.DisplayHeight)));
            addObject(new Wall(new Rectangle(Game1.DisplayWidth, 0, 20, Game1.DisplayHeight)));
            addObject(new Wall(new Rectangle(0, -20, Game1.DisplayWidth, 20)));
            addObject(new Wall(new Rectangle(0, Game1.DisplayHeight, Game1.DisplayWidth, 20)));
        }

        public virtual void Update(GameTime gameTime, Map map)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                if (objs[i] != null)
                {
                    objs[i].Update(gameTime, map);
                    if (objs[i].GetType() == typeof(Goal))
                    {
                        if (objs[i].Rec.Intersects(map.Player.Rec))
                        {
                            passed = true;
                        }
                    }
                    
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Base b in objs)
            {
                b.Draw(spriteBatch);
            }
            if (messageText != null)
            {
                spriteBatch.DrawString(font, messageText, new Vector2(5, Game1.DisplayHeight - 30), Color.Blue);
            }
        }

        public void addObject(Base o)
        {
            objs.Add(o);
        }

        public virtual void resetMap(Map map)
        {
            map.changeLevel(new MData());
        }
    }
}

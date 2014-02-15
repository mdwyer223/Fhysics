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
        public List<Base> AllObjects
        {
            get { return objs; }
        }

        public MData()
        {
            objs = new List<Base>();
            addObject(new Wall(new Rectangle(-20, 0, 20, Game1.DisplayHeight)));
            addObject(new Wall(new Rectangle(Game1.DisplayWidth, 0, 20, Game1.DisplayHeight)));
            addObject(new Wall(new Rectangle(0, -20, Game1.DisplayWidth, 20)));
            addObject(new Wall(new Rectangle(0, Game1.DisplayHeight, Game1.DisplayWidth, 20)));
        }

        public MData(Player p)
        {
            objs = new List<Base>();
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
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Base b in objs)
            {
                b.Draw(spriteBatch);
            }
        }

        public void addObject(Base o)
        {
            objs.Add(o);
        }
    }
}

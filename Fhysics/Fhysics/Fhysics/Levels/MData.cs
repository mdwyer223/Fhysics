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
        }

        public MData(Player p)
        {
            objs = new List<Base>();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fhysics
{
    public class Gap : Base
    {
        List<Box> boxes;
        bool walkable = false;
        public List<Box> Boxes
        {
            get { return boxes; }
        }
        public Gap(Rectangle rec)
            : base(rec)
        {
            color = Color.Black;
        }

        public override void Update(GameTime gameTime, Map data)
        {
            walkable = false;

            List<Base> objs = data.Data.AllObjects;
            for (int i = 0; i < objs.Count; i++)
            {
                if (objs[i] != null)
                {
                    if (objs[i].Rec.Intersects(Rec))
                    {
                        boxes.Add((Box)objs[i]);
                        //adjust position
                        data.Data.AllObjects.RemoveAt(i);
                    }

                }
                for(int j = 0; j< boxes.Count; j++)
                {
                    if (boxes[j].Rec.Intersects(data.Player.Rec))
                    {
                        walkable = true;
                    }
                }

                if (!walkable)
                {
                    if (data.Player.Rec.Intersects(Rec))
                    {
                        data.Player.IsDead = true;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

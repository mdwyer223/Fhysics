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
        //List<Box> boxes;
        List<Box> boxes;
        bool walkable = false, goodToDrop;
        public List<Box> Boxes
        {
            get { return boxes; }
        }

        public Gap(Rectangle rec)
            : base(rec)
        {
            boxes = new List<Box>();
            color = Color.Black;
        }

        public override void Update(GameTime gameTime, Map data)
        {
            walkable = false;
            goodToDrop = true;

            List<Base> objs = data.Data.AllObjects;
            for (int k = 0; k < boxes.Count; k++)
            {
                if (boxes[k].Rec.Intersects(data.Player.Rec) && !walkable)
                {
                    walkable = true;
                }
            }
            for (int i = 0; i < objs.Count; i++)
            {
                if (boxes.Count == 0)
                {
                    goodToDrop = true;
                }
                else
                {
                    for (int j = 0; j < boxes.Count; j++)
                    {
                        if (boxes[j].Rec.Intersects(objs[i].Rec) && objs[i].GetType() != typeof(Gap))
                        {
                            goodToDrop = false;
                            break;
                        }
                    }
                }
                if (objs[i] != null && objs[i].GetType() == typeof(Box))
                {
                    if (objs[i].Rec.Intersects(Rec) && goodToDrop)
                    {
                        Rectangle objRec = objs[i].Rec;
                        if ((objRec.X >= Rec.X && objRec.Y >= Rec.Y)
                            && (objRec.X + objRec.Width <= Rec.X + Rec.Width && objRec.Y + objRec.Height <= Rec.Y + Rec.Height))
                        {
                            boxes.Add((Box)objs[i]);
                            data.Data.AllObjects.RemoveAt(i);
                        }
                    }

                }

                if (!walkable)
                {
                    Rectangle objRec = data.Player.Rec;
                    if ((objRec.X >= Rec.X && objRec.Y >= Rec.Y)
                            && (objRec.X + objRec.Width / 2 <= Rec.X + Rec.Width && objRec.Y + objRec.Height / 2 <= Rec.Y + Rec.Height))
                    {
                        data.Player.IsDead = true;
                        Game1.LossText = "You fell in a hole";
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Box b in boxes)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}

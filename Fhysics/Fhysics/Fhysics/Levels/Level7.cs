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
    public class Level7 : MData
    {
        public Level7(Player p) 
            :base(p)
        {
            p.Position = new Vector2(10, 10);
            addObject(new Gap(new Rectangle(0, 100, 200, 30)));
            addObject(new Gap(new Rectangle(200, 0, 27, 100)));
            addObject(new Gap(new Rectangle(110, 100, 30, 30)));


            addObject(new Box(new Vector2(170, 30), Directions.ALL, true));
            addObject(new Box(new Vector2(0, 100), Directions.RIGHT, true));
            addObject(new Box(new Vector2(230, 0), Directions.NONE, true));

            Vector2 pos = new Vector2(0, 400);
            //Directions d = Directions.NONE;
            //for (int i = 0; i < 40; i++)
            //{
            //    addObject(new Box(pos, d, true));
            //    if (i == 4)
            //    {
            //        d = Directions.TOP;
            //    }
            //    else
            //    {
            //        d = Directions.NONE;
            //    }
            //    pos.X += 23;
            //}

            //pos = new Vector2(0, 200);
            //d = Directions.NONE;
            //for (int i = 0; i < 10; i++)
            //{
            //    addObject(new Box(pos, d, true));
            //    if (i == 2)
            //    {
            //        d = Directions.TOP;
            //    }
            //    else
            //    {
            //        d = Directions.NONE;
            //    }
            //    pos.X += 25;
            //}

            addObject(new Box(new Vector2(290, 100), Directions.DOWN, true));
            addObject(new Box(new Vector2(320, 100), new List<Directions> { Directions.DOWN, Directions.LEFT }, true));

            addObject(new Goal(new Rectangle(290, 243, 20, 20)));
        }

        public override void Update(GameTime gameTime, Map map)
        {
            if (passed)
            {
                map.changeLevel(new Level8(map.Player));
                map.Player.levelUp();
            }
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level7(map.Player));
        }
    }
}

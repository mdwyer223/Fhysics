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
    public class Level3 : MData
    {
        public Level3(Player p)
            : base(p)
        {
            Directions d = Directions.NONE;
            p.Position = new Vector2(5, 230);
            bool push = true;

            addObject(new Gap(new Rectangle(550, 0, 21, Game1.DisplayHeight)));

            Vector2 pos = new Vector2(400, 0);
            for (int i = 0; i < 24; i++)
            {
                Box b = new Box(pos, d, push);
                addObject(b);
                if(i == 15)
                {
                    d = Directions.ALL;
                }
                else
                    d = Directions.NONE;

                pos.Y += 25;
            }

            addObject(new Goal(new Rectangle(700, 230, 20, 20)));
            messageText = "Gaps will end the game! Cover them!";
        }

        public override void Update(GameTime gameTime, Map map)
        {
            if (passed)
            {
                map.changeLevel(new Level4(map.Player));
            }
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level3(map.Player));
        }
    }
}

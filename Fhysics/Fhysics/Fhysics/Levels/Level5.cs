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
    public class Level5 : MData
    {
        public Level5(Player p)
            : base(p)
        {
            p.Position = new Vector2(30, 450);
            addObject(new Gap(new Rectangle(250, 0, Game1.DisplayWidth, 240)));
            addObject(new Gap(new Rectangle(300, 280, 30, Game1.DisplayHeight)));

            Vector2 pos = new Vector2(2, 300);
            Directions d = Directions.NONE;
            for (int i = 0; i < 12; i++)
            {
                addObject(new Box(pos, d, true));
                if (i == 10)
                {
                    d = Directions.TOP;
                }
                else
                {
                    d = Directions.NONE;
                }
                pos.X += 25;
            }

            addObject(new Box(new Vector2(300, 250), Directions.LEFT, false));

            addObject(new Goal(new Rectangle(750, 450, 20, 20)));
        }

        public override void Update(GameTime gameTime, Map map)
        {
            if (passed)
            {
                map.Player.levelUp();
                map.changeLevel(new Level6(map.Player, map));
            }
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level5(map.Player));
        }
    }
}

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
    public class Level4 : MData
    {
        public Level4(Player p)
            : base(p)
        {
            p.Position = new Vector2(30, 450);

            addObject(new Gap(new Rectangle(0, 100, Game1.DisplayWidth, 23)));

            Vector2 pos = new Vector2(2, 300);
            Directions d = Directions.TOP;
            for (int i = 0; i < 18; i++)
            {
                addObject(new Box(pos, d, true));
                d = Directions.NONE;
                pos.X += 25;
            }

            addObject(new Box(new Vector2(5, 220), Directions.RIGHT, false));
            addObject(new Goal(new Rectangle(20, 50, 20, 20)));
        }

        public override void Update(GameTime gameTime, Map map)
        {
            if (passed)
            {
                map.changeLevel(new Level5(map.Player));
                map.Player.levelUp();
            }
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level4(map.Player));
        }
    }
}

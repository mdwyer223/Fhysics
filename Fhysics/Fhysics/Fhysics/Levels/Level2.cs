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
    public class Level2 : MData
    {
        public Level2(Player p)
            : base(p)
        {
            Directions d = Directions.RIGHT;
            bool push = true;

            Vector2 pos = new Vector2(400, 0);
            for (int i = 0; i < 24; i++)
            {
                Box b = new Box(pos, d, push);
                addObject(b);
                if (d == Directions.RIGHT)
                {
                    d = Directions.LEFT;
                    push = false;
                }
                else
                {
                    d = Directions.RIGHT;
                    push = true;
                }

                pos.Y += 25;
            }

            addObject(new Goal(new Rectangle(700, 230, 20, 20)));
            p.Position = new Vector2(5, 230);
            messageText = "Hold shift to pull, walk to push";
        }

        public override void Update(GameTime gameTime, Map map)
        {
            if (passed)
            {
                map.Player.levelUp();
                map.changeLevel(new Level3(map.Player));
            }
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level2(map.Player));
        }
    }
}

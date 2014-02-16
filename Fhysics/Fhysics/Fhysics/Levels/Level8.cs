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
    public class Level8 : MData
    {
        public Level8(Player p) 
            : base(p)
        {
            p.Position = new Vector2(10, Game1.DisplayHeight / 2);
            addObject(new Gap(new Rectangle(660, 0, 65, Game1.DisplayHeight)));

            addObject(new Fan(new Rectangle(75, 220, 10, 40), Orientation.RIGHT));
            addObject(new Box(new Vector2(280, 280), Directions.TOP, true));
            addObject(new Box(new Vector2(305, 280), Directions.TOP, true));
            addObject(new Box(new Vector2(330, 280), Directions.TOP, true));

            addObject(new Goal(new Rectangle(750, 230, 20, 20)));
        }

        public override void Update(GameTime gameTime, Map map)
        {
            if (passed)
            {
                map.Player.levelUp();
                map.changeLevel(new Level9(map.Player));
            }
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level8(map.Player));
        }
    }
}

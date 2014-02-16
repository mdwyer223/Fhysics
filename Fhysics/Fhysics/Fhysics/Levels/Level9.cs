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
    public class Level9 : MData
    {
        public Level9(Player p)
            : base(p)
        {
            p.Position = new Vector2(25, 450);
            addObject(new Gap(new Rectangle(660, 0, 65, Game1.DisplayHeight)));

            addObject(new Fan(new Rectangle(25, 25, 10, 50), Orientation.RIGHT));
            addObject(new Box(new Vector2(280, 95), Directions.TOP, true));
            addObject(new Box(new Vector2(305, 95), Directions.TOP, true));
            addObject(new Box(new Vector2(330, 95), Directions.TOP, true));

            addObject(new Box(new Vector2(500, 35), new List<Directions> { Directions.DOWN, Directions.LEFT }, false));
            addObject(new Box(new Vector2(350, 95), Directions.LEFT, true));
            addObject(new Box(new Vector2(350, 125), Directions.LEFT, true));
            addObject(new Box(new Vector2(350, 155), Directions.LEFT, false));
            for (int i = 185; i < 480; i += 30)
                addObject(new Box(new Vector2(350, i), Directions.LEFT, true));
            addObject(new Gap(new Rectangle(0, 95, 275, 30)));

            addObject(new Goal(new Rectangle(750, 230, 20, 20)));
        }

        public override void Update(GameTime gameTime, Map map)
        {
            if (passed)
            {
                map.Player.levelUp();
                map.changeLevel(new Level10(map.Player));
            }
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level9(map.Player));
        }
    }
}

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
    public class Level10 : MData
    {
         public Level10(Player p)
            : base(p)
        {
            p.Position = new Vector2(30, 450);

            addObject(new Gap(new Rectangle(550, 150, 90, 200)));
            addObject(new Fan(new Rectangle(400, 110, 20, 75), Orientation.LEFT));
            addObject(new Fan(new Rectangle(475, 478, 20, 2), Orientation.UP));
            addObject(new Fan(new Rectangle(2, 2, 100, 2), Orientation.DOWN));
            addObject(new Fan(new Rectangle(0, 360, 2, 120), Orientation.RIGHT));
            addObject(new Fan(new Rectangle(700, 478, 100, 2), Orientation.UP));
            addObject(new Fan(new Rectangle(780, 150, 20, 120), Orientation.LEFT));
            
            addObject(new Box(new Vector2(400, 200), Directions.RIGHT, true));
            Directions d = Directions.ALL;
            for (int i = 280; i < 390; i+=30)
            {
                addObject(new Box(new Vector2(i, 130), d, true));
                
            }
            addObject(new Goal(new Rectangle(590, 240, 20, 20)));
        }

        public override void Update(GameTime gameTime, Map map)
        {
            if (passed)
            {
                map.Player.levelUp();
                map.changeLevel(new Level11(map.Player));
            }
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level10(map.Player));
        }
    }
}

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
    public class Level11 : MData
    {
        public Level11(Player p)
            : base(p)
        {
            p.Position = new Vector2(30, 450);
            addObject(new Turret(new Rectangle(400, 240, 20, 20)));

            bool isPush = false;
            for(int i=340; i<480;i += 40)
            {
                addObject(new Box(new Vector2(100, i), Directions.NONE, isPush));
                isPush = !isPush;
            }

            for (int i = 100; i<250; i += 40)
            {
                addObject(new Box(new Vector2(i, 90), Directions.NONE, isPush));
                isPush = !isPush;
            }

            for (int i = 100; i < 400; i += 40)
            {
                addObject(new Box(new Vector2(600, i), Directions.NONE, isPush));
                isPush = !isPush;
            }

            addObject(new Goal(new Rectangle(700, 300, 20, 20)));

        }

        public override void Update(GameTime gameTime, Map map)
        {
            if (passed)
            {
                map.Player.levelUp();
                map.changeLevel(new Level12(map.Player));
            }
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level11(map.Player));
        }

    }
}

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
    public class Level12 : MData
    {
        public Level12(Player p)
            : base(p)
        {
            p.Position = new Vector2(15, 455);

            addObject(new IceStrip(new Rectangle(0, 0, 40, 455)));

            addObject(new Gap(new Rectangle(41, 56, 600, 430)));
            addObject(new Gap(new Rectangle(41, 0, 800, 29)));
            addObject(new Gap(new Rectangle(670, 50, 220, 430)));
            addObject(new IceStrip(new Rectangle(40, 30, 800, 35)));
            addObject(new IceStrip(new Rectangle(640, 40, 30, 440)));

            
            addObject(new Turret(new Rectangle(400, 240, 20, 20)));
            addObject(new Goal(new Rectangle(645, 450, 20, 20)));
        
        }

        public override void Update(GameTime gameTime, Map map)
        {
            if (passed)
            {
                map.Player.levelUp();
                map.changeLevel(new Level13(map.Player));
            }
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level12(map.Player));
        }
    }
}

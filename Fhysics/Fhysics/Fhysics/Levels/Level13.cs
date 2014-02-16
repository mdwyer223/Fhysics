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
    public class Level13 : MData
    {
        public Level13(Player p)
            : base(p)
        {
            p.Position = new Vector2(5, 450);
            addObject(new Gap(new Rectangle(35, 110, 75, 400)));
            addObject(new Gap(new Rectangle(35, 0, 360, 75)));
            addObject(new Gap(new Rectangle(145, 0, 250, 410)));
            addObject(new Gap(new Rectangle(110, 445, Game1.DisplayWidth, 100)));
            addObject(new Gap(new Rectangle(395, 0, Game1.DisplayWidth, 20)));
            addObject(new Gap(new Rectangle(540, 20, Game1.DisplayWidth, 35)));
            addObject(new Gap(new Rectangle(430, 55, Game1.DisplayWidth, Game1.DisplayHeight)));

            addObject(new IceStrip(new Rectangle(0, 0, 35, 435)));
            addObject(new IceStrip(new Rectangle(35, 75, 110, 35)));
            addObject(new IceStrip(new Rectangle(110, 110, 35, 335)));
            addObject(new IceStrip(new Rectangle(145, 410, 285, 35)));
            addObject(new IceStrip(new Rectangle(395, 20, 35, 390)));
            addObject(new IceStrip(new Rectangle(395, 20, 125, 35)));

            addObject(new Turret(new Rectangle(600, 50, 20, 20)));
            addObject(new Turret(new Rectangle(600, 240, 20, 20)));
            addObject(new Turret(new Rectangle(600, 460, 20, 20)));

            addObject(new Goal(new Rectangle(520, 20, 20, 35)));
        }

        public override void Update(GameTime gameTime, Map map)
        {
            if (passed)
            {
                map.Player.levelUp();
                Game1.State = GameState.END;
                Game1.LossText = "You made it through the maze!";
            }
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level13(map.Player));
        }
    }
}

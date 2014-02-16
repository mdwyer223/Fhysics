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
    public class Level1 : MData
    {
        public Level1()
            : base()
        {
            //List<Directions> d = new List<Directions>();
            //Directions dActual = Directions.RIGHT;
            //d.Add(dActual);

            //addObject(new Gap(new Rectangle(500, 0, 25, Game1.DisplayHeight)));

            addObject(new Goal(new Rectangle(700, 230, 20, 20)));
            messageText = "W, A, S, D movement (Up, Left, Down, Right)";
        }

        public Level1(Player p)
            : base(p)
        {
            //List<Directions> d = new List<Directions>();
            //Directions dActual = Directions.RIGHT;
            //d.Add(dActual);

            //addObject(new Gap(new Rectangle(500, 0, 25, Game1.DisplayHeight)));
            addObject(new Goal(new Rectangle(700, 230, 20, 20)));

            //Box b = new Box(new Vector2(390, 230), d, true);
            //addObject(b);

            //Fan f = new Fan(new Rectangle(200, 100, 50, 50), Orientation.RIGHT);
            //addObject(f);

            p.Position = new Vector2(5, 230);
            messageText = "W, A, S, D movement (Up, Left, Down, Right)";
        }

        public override void Update(GameTime gameTime, Map map)
        {
            if (passed)
            {
                map.Player.levelUp();
                map.changeLevel(new Level2(map.Player));
            }
            
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level1(map.Player));
        }
    }
}

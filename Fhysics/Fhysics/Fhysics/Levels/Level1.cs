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
            List<Directions> d = new List<Directions>();
            Directions dActual = Directions.NONE;
            d.Add(dActual);

            Box b = new Box(new Vector2(350, 90), d, true);
            addObject(b);
            b = new Box(new Vector2(500, 151), d, true);
            addObject(b);
            b = new Box(new Vector2(400, 261), d, true);
            addObject(b);
            b = new Box(new Vector2(320, 400), d, true);
            addObject(b);
            b = new Box(new Vector2(620, 321), d, true);
            addObject(b);

            addObject(new Goal(new Rectangle(700, 230, 20, 20)));
            messageText = "W, A, S, D movement (Up, Left, Down, Right)";
        }

        public Level1(Player p)
            : base(p)
        {
            List<Directions> d = new List<Directions>();
            Directions dActual = Directions.NONE;
            d.Add(dActual);
            
            Box b = new Box(new Vector2(350, 90), d, true);
            addObject(b);
            b = new Box(new Vector2(500, 151), d, true);
            addObject(b);
            b = new Box(new Vector2(400, 261), d, true);
            addObject(b);
            b = new Box(new Vector2(320, 400), d, true);
            addObject(b);
            b = new Box(new Vector2(620, 321), d, true);
            addObject(b);

            addObject(new Goal(new Rectangle(700, 230, 20, 20)));

            

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

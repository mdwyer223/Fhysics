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
            List<Directions> d = new List<Directions>();
            Directions dActual = Directions.RIGHT;
            d.Add(dActual);

            Box b = new Box(new Vector2(50, 230), d, true);
            addObject(b);

            Fan f = new Fan(new Rectangle(100, 100, 50, 50), Orientation.RIGHT);
            addObject(f);

            f = new Fan(new Rectangle(200, 440, 50, 50), Orientation.UP);
            addObject(f);

            f = new Fan(new Rectangle(600, 25, 50, 50), Orientation.DOWN);
            addObject(f);

            f = new Fan(new Rectangle(650, 380, 50, 50), Orientation.LEFT);
            addObject(f);

            Turret t = new Turret(new Rectangle(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2, 25, 25));
            addObject(t);

            p.Position = new Vector2(5, 230);
        }
    }
}

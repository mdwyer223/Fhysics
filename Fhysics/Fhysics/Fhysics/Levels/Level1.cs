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
            Directions dActual = Directions.LEFT;
            d.Add(dActual);
            addObject(new Box(new Vector2(360, 230), Directions.RIGHT, true));
            addObject(new Box(new Vector2(300, 230), Directions.RIGHT, true));
            addObject(new Box(new Vector2(240, 230), Directions.RIGHT, true));
            addObject(new Box(new Vector2(180, 230), Directions.RIGHT, true));
            addObject(new Box(new Vector2(120, 260), Directions.ALL, true));
            addObject(new Box(new Vector2(120, 60), Directions.NONE, true));
        }
    }
}

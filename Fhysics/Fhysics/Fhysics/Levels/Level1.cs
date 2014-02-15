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
            Directions dActual = Directions.RIGHT;
            d.Add(dActual);
            Box b = new Box(new Vector2(390, 230), d);
            addObject(b);
        }
    }
}

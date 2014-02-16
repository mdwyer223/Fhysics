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
    public class Level4 : MData
    {
        public Level4(Player p)
            : base(p)
        {
            p.Position = new Vector2(30, 450);
            

        }

        public override void Update(GameTime gameTime, Map map)
        {
            base.Update(gameTime, map);
        }

        public override void resetMap(Map map)
        {
            map.changeLevel(new Level4(map.Player));
        }
    }
}

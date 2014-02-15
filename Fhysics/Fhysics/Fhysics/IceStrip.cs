﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fhysics
{
    public class IceStrip : Base
    {
        public IceStrip(Rectangle rec)
            : base(rec)
        {
            color = Color.LightBlue;
        }

        public override void Update(GameTime gameTime, Map data)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

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
    public class Map
    {
        Player player;
        MData data;
        public MData Data
        {
            get { return data; }
        }

        public Player Player
        {
            get { return player; }
        }

        public Map(Player p, MData data)
        {
            player = p;
            this.data = data;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                Game1.State = GameState.PAUSE;
            }
            else
            {
                player.Update(gameTime, this);
                data.Update(gameTime, this);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            data.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }

        public void changeLevel(MData newData)
        {
            data = newData;
        }
    }
}

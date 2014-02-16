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
    public class PauseScreen
    {
        SpriteFont font;
        Map map;
        string text = "Esc - Resume\nR - restart level\nG - restart game\nQ - Quit";

        bool quitting;
        KeyboardState keys, oldKeys;

        public bool Quit
        {
            get { return quitting; }
        }

        public PauseScreen(Map map)
        {
            this.map = map;
            keys = oldKeys = Keyboard.GetState();
            font = Game1.GameContent.Load<SpriteFont>("Time");
        }

        public virtual void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.R))
            {
                map.Data.resetMap(map);
                Game1.State = GameState.PLAYING;
            }
            else if (keys.IsKeyDown(Keys.G))
            {
                map.changeLevel(new Level1());
                Game1.State = GameState.PLAYING;
            }
            else if (keys.IsKeyDown(Keys.Q))
            {
                quitting = true;
            }
            else if (keys.IsKeyDown(Keys.Escape))
            {
                Game1.State = GameState.PLAYING;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, new Vector2((Game1.DisplayWidth / 2) - 100, 50), Color.White);
        }
    }
}

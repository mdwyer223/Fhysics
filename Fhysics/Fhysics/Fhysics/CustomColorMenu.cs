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
    public class CustomColorMenu
    {
        bool redDone = false;
        bool greenDone = false;
        bool blueDone= false;
        int redValue;
        int greenValue;
        int blueValue;
        Color custom = Color.White;
        SpriteFont subtitleFont;
        Vector2 colorVector = new Vector2(10,400);
        KeyboardState oldkeys, keys;

        public Color CustomColor
        {
            get { return custom; }
        }
        
        public CustomColorMenu()
        {
            Load();
            keys = oldkeys = Keyboard.GetState();

        }
        public void Load()
        {
            subtitleFont = Game1.GameContent.Load<SpriteFont>("Subtitle");
        }
        public void Update()
        {
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.W))
            {
                if (redDone == false)
                {
                    if (redValue != 255)
                    {
                        redValue++;

                    }
                }
                else if (greenDone == false)
                {
                    if (greenValue != 255)
                    {
                        greenValue++;
                    }
                }
                else if (blueDone == false)
                {
                    if (blueValue != 255)
                    {
                        blueValue++;
                    }
                }

            }
            if (keys.IsKeyDown(Keys.S))
            {
                if (redDone == false)
                {
                    if (redValue != 0)
                    {
                        redValue--;
                    }
                }
                else if (greenDone == false)
                {
                    if (greenValue != 0)
                    {
                        greenValue--;
                    }
                }
                else if (blueDone == false)
                {
                    if (blueValue != 0)
                    {
                        blueValue--;
                    }
                }

            }
            if (keys.IsKeyDown(Keys.Enter) && oldkeys.IsKeyUp(Keys.Enter))
            {
                if (redDone == false)
                {
                    redDone = true;
                    greenDone = false;
                    blueDone = false;
                }
                else if (greenDone == false && redDone)
                {
                    greenDone = true;
                    blueDone = false;
                }
                else if (blueDone == false && greenDone && redDone)
                {
                    blueDone = true;
                    redDone = false;
                    greenDone = false;
                }     
            }
            oldkeys = keys;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            custom = new Color(redValue, greenValue, blueValue);

            if (redDone == false)
            {
                spriteBatch.DrawString(subtitleFont, "Red Value!\nPress W or S", colorVector, Color.Black);
            }
            if (redDone && greenDone== false)
            {
                spriteBatch.DrawString(subtitleFont, "Green Value!\nPress W or S", colorVector, Color.Black);
            }
            if (blueDone == false && redDone && greenDone)
            {
                spriteBatch.DrawString(subtitleFont, "Blue Value!\nPress W or S", colorVector, Color.Black);
            }
            
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fhysics
{
    public enum GameState { START, PLAYING, END, PAUSE };
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PauseScreen pause;
        EndScreen end;
        Map map;

        //levels
        static int level;

        //make a timer
        static string timer;
        float minutes, seconds, miliseconds, minutesDeduction, secondsDeduction;
        SpriteFont timeFont;

        static ContentManager gameContent;
        public static ContentManager GameContent
        {
            get { return gameContent; }
        }

        static GameState gState = GameState.PLAYING;
        public static GameState State
        {
            get { return gState; }
            set { gState = value; }
        }

        static int displayHeight, displayWidth;
        public static int DisplayHeight
        {
            get
            {
                return displayHeight;
            }
        }
        public static int DisplayWidth
        {
            get { return displayWidth; }
        }

        static string lossInfo;
        public static string LossText
        {
            get { return lossInfo; }
            set { lossInfo = value; }
        }

        public static string Time
        {
            get { return timer; }
        }

        public static int LevelPassed
        {
            get { return level; }
            set { level = value; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            gameContent = Content;
        }

        protected override void Initialize()
        {
            displayWidth = GraphicsDevice.Viewport.Width;
            displayHeight = GraphicsDevice.Viewport.Height;

            Player player = new Player(new Vector2(5, 230), Color.Purple);
                               
            Level1 l1 = new Level1();
            map = new Map(player, l1);

            pause = new PauseScreen(map);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            timeFont = Content.Load<SpriteFont>("Time");
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (State == GameState.START)
            {
            }
            else if (State == GameState.PLAYING)
            {
                miliseconds += gameTime.ElapsedGameTime.Milliseconds / 1000f;
                map.Update(gameTime);
                seconds = (int)miliseconds;
                minutes = seconds / 60;

                if (miliseconds > 60)
                {
                    miliseconds = 0f;
                }

                if (seconds < 10)
                {
                    timer = "" + (int)minutes + ":0" + (int)seconds;
                }
                else
                {
                    timer = "" + (int)minutes + ":" + (int)seconds;
                }
            }
            else if (State == GameState.END)
            {
                end = new EndScreen(LevelPassed);
                end.Update(gameTime);
                if (end.Restart)
                {
                    restartGame();
                }
            }
            else if (State == GameState.PAUSE)
            {
                pause.Update(gameTime);

                if (pause.Quit)
                {
                    this.Exit();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            if (State == GameState.START)
            {

            }
            else if (State == GameState.PLAYING)
            {
                spriteBatch.Begin();

                map.Draw(spriteBatch);

                spriteBatch.DrawString(timeFont, timer, new Vector2(800 - timeFont.MeasureString(timer).X, 5), Color.Red);

                spriteBatch.End();
            }
            else if (State == GameState.END)
            {
                if (end != null)
                {
                    spriteBatch.Begin();
                    end.Draw(spriteBatch);
                    spriteBatch.End();
                }
            }
            else if (State == GameState.PAUSE)
            {
                spriteBatch.Begin();
                pause.Draw(spriteBatch);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public void restartGame()
        {
            Player player = new Player(new Vector2(5, 230), Color.Purple);

            Level1 l1 = new Level1();
            map = new Map(player, l1);
            secondsDeduction = seconds;
            minutesDeduction += minutes;
            seconds = minutes = 0;

            pause = new PauseScreen(map);
            end = null;
        }
    }
}

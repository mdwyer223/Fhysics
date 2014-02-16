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

        SpriteFont startFont;
        SpriteFont subtitleFont;
        byte redIntensity = 0;
        bool redCountingUp = true;
        
        //player
        Texture2D playerTexture;
        
        Vector2 playerMenuVector;
        Rectangle playerRec;
        Color playerColor = Color.White;

        //custom color
        bool customColorChoice;
        Vector2 colorChoiceVector;
        CustomColorMenu customMenu = null;
        
        //start menu text
        string start = "Fhysics!";
        string begin = "Press space to start!";
        string colorChoice = "Pick a color\n 1.Red \n 2.Blue\n 3.Green\n 4.White\n 5.Custom \n ";
        Vector2 beginTextVector;
        Vector2 startTextVector;
        

        //make a timer
        static string timer;
        float minutes, seconds, miliseconds, minutesDeduction, secondsDeduction;
        SpriteFont timeFont;

        static ContentManager gameContent;
        public static ContentManager GameContent
        {
            get { return gameContent; }
        }

        static GameState gState = GameState.START;
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
                               
            
            map = new Map(player, new Level1(player));

            pause = new PauseScreen(map);

            startTextVector = new Vector2(8,8);
            beginTextVector = new Vector2(10, 60);
            playerRec = new Rectangle(GraphicsDevice.Viewport.Height/4, GraphicsDevice.Viewport.Width/2, 300, 300);
            colorChoiceVector = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            playerMenuVector = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            
            base.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            timeFont = Content.Load<SpriteFont>("Time");

            playerTexture = this.Content.Load<Texture2D>("player");
            startFont = Game1.gameContent.Load<SpriteFont>("StartMenu");
            subtitleFont = Game1.gameContent.Load<SpriteFont>("Subtitle");
            Vector2 startStringWidth = startFont.MeasureString(start);
            Vector2 colorStringWidth = startFont.MeasureString(colorChoice);
            //startTextVector.X -= startStringWidth.X / 2;
            //startTextVector.Y -= startStringWidth.Y/2;
            colorChoiceVector.X -= colorStringWidth.X;
            colorChoiceVector.Y -= colorStringWidth.Y;
            
           // playerRec.X -= (int)playerRec.Width/2;
            playerRec.Y -= (int)playerRec.Height;
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (State == GameState.START)
            {
                if (keys.IsKeyDown(Keys.Space))
                {
                    gState = GameState.PLAYING;
                    customColorChoice = false;
                    map.Player.Color = playerColor;
                }
               //start text flash
                if (redIntensity == 255) 
                    redCountingUp = false;
                if (redIntensity == 0) 
                    redCountingUp = true;
                if (redCountingUp)
                    redIntensity+=5; 
                else 
                    redIntensity-=5;
                
                //player color choice

                if (keys.IsKeyDown(Keys.D1))
                {
                    playerColor = new Color(255, 0, 0);
                    customMenu = null;
                    customColorChoice = false;
                }
                if (keys.IsKeyDown(Keys.D2))
                {
                    playerColor = new Color(0,0,255);
                    customMenu = null;
                    customColorChoice= false;
                }
                if (keys.IsKeyDown(Keys.D3))
                {
                    playerColor = new Color(0, 255, 0);
                    customMenu = null;
                    customColorChoice = false;
                }
                if (keys.IsKeyDown(Keys.D4))
                {
                    playerColor = new Color(255, 255, 255);
                    customMenu = null;
                    customColorChoice = false;

                }
                if (keys.IsKeyDown(Keys.D5))
                {
                    customColorChoice = true;
                    customMenu = new CustomColorMenu();
                }
                
            }
            if (customMenu != null && customColorChoice)
            {
                customMenu.Update();
                playerColor = customMenu.CustomColor;
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
                spriteBatch.Begin();
                Color startColor;


                startColor = new Color(redIntensity, 0, 0);
                spriteBatch.DrawString(startFont, start, startTextVector, startColor);
                spriteBatch.DrawString(subtitleFont, begin, beginTextVector, startColor);
                spriteBatch.DrawString(startFont, colorChoice, colorChoiceVector, startColor);


                if (customColorChoice)
                {
                    customMenu.Draw(spriteBatch);

                    spriteBatch.Draw(playerTexture, playerRec, customMenu.CustomColor);
                }
                else
                {
                    spriteBatch.Draw(playerTexture, playerRec, playerColor);
                }

                spriteBatch.End();

            }
            else if (State == GameState.PLAYING)
            {
                spriteBatch.Begin();

                map.Draw(spriteBatch);

                spriteBatch.DrawString(timeFont, timer + "\n Level: " + level, new Vector2(800 - timeFont.MeasureString("Level: 999").X, 5), Color.Red);

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
            Player player = new Player(new Vector2(5, 230), playerColor);

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

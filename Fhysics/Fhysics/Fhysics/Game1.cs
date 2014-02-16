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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public enum GameState { START, PLAYING, END, PAUSE };

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
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
        
        //string colorChoices =

        //make a timer

        static GameState gState;
        public static GameState State
        {
            get { return gState; }
            set { gState = value; }
        }

        static ContentManager gameContent;
        public static ContentManager GameContent
        {
            get { return gameContent; }
        }

        public Game1()
        {
           
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            gameContent = Content;
            gState = GameState.START;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            startTextVector = new Vector2(8,8);
            beginTextVector = new Vector2(10, 60);
            playerRec = new Rectangle(GraphicsDevice.Viewport.Height/4, GraphicsDevice.Viewport.Width/2, 300, 300);
            colorChoiceVector = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            playerMenuVector = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
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

            
            
           
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState keys = Keyboard.GetState();
            
            
            if (gState == GameState.START)
            {
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
                

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin();

            if (gState == GameState.START)
            {
                Color startColor;
                

                startColor = new Color(redIntensity, 0,0);
                spriteBatch.DrawString(startFont, start, startTextVector, startColor);
                spriteBatch.DrawString(subtitleFont, begin, beginTextVector, startColor);
                spriteBatch.DrawString(startFont, colorChoice, colorChoiceVector, startColor);


                if (customColorChoice)
                {
                    customMenu.Draw(spriteBatch);
                   
                    spriteBatch.Draw(playerTexture, playerRec,customMenu.CustomColor);
                }
                else
                {
                    spriteBatch.Draw(playerTexture, playerRec, playerColor);
                }
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}

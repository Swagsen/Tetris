using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Tetris
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Figura figura;
        GameManager gameManager;
        MenuScreen menuScreen;
        GameOverScreen gameOverScreen;
        ScoreScreen scoreScreen;

        public enum state { MENU, GAME, GAMEOVER, SCORE, EXIT };
        static public state State = state.MENU;
        public static int CurrentScore;
        public static bool Restart = false;
        public static List<int> Top5Score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);


            graphics.PreferredBackBufferWidth = 320; //320
            graphics.PreferredBackBufferHeight = 640;
            graphics.ApplyChanges();


            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            //figura = new J(Content);
            Figura.content = Content;
            gameManager = new GameManager(Content);
            menuScreen = new MenuScreen(Content);
            gameOverScreen = new GameOverScreen(Content);
            scoreScreen = new ScoreScreen(Content);
            Top5Score = new List<int>(5) { 0, 0, 0, 0, 0 };
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            if (Restart)
            {
                gameManager = new GameManager(Content);
                Restart = false;
            }

            switch (State)
            {
                case state.MENU:
                    menuScreen.Update();
                    break;
                case state.GAME:
                    gameManager.Update(gameTime);
                    break;
                case state.GAMEOVER:
                    gameOverScreen.Update();
                    break;
                case state.SCORE:
                    scoreScreen.Update();
                    break;
                case state.EXIT:
                    Exit();
                    break;

            }

            //if (!gameManager.Update(gameTime))
            //    Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();



            switch (State)
            {
                case state.MENU:
                    menuScreen.Draw(spriteBatch);
                    break;
                case state.GAME:
                    gameManager.Draw(spriteBatch);
                    break;
                case state.GAMEOVER:
                    gameOverScreen.Draw(spriteBatch);
                    break;
                case state.SCORE:
                    scoreScreen.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}

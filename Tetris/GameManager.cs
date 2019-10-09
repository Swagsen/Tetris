using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Tetris
{
    class GameManager
    {
        int[,] landed;
        Texture2D image;
        const int height = 20;
        const int width = 10;
        Figura figura;
        public static ContentManager content;

        KeyboardState previousState;

        int StepTime = 300; 
        int ElapsedTime = 0;
        int KeyBoardElapsedTime = 0;

        int Points = 0;

        public GameManager()
        {
            image = content.Load<Texture2D>("klocek");
            landed = new int[height, width];
            figura = new Figura();
        }

        public enum CANTMOVE { LEFT, RIGHT, DOWN, GITGUT, AtAll };


        void Place()
        {
            for (int y = 0; y < figura.Size; y++)
            {
                for (int x = 0; x < figura.Size; x++)
                {
                    if (figura.CurrentPiece[y, x] != 0)
                    {
                        landed[(int)figura.position.Y + y, (int)figura.position.X + x] = figura.CurrentColor;
                    }
                }
            }
        }

        public void RemoveCompleteLines()
        {
            for (int y = height - 1; y >= 0; y--)
            {
                bool isComplete = true;
                for (int x = 0; x < width; x++)
                {
                    if (landed[y, x] == 0)
                    {
                        isComplete = false;
                    }
                }

                if (isComplete)
                {
                    for (int yc = y; yc > 0; yc--)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            landed[yc, x] = landed[yc - 1, x];
                        }
                    }
                    y++;

                    Console.Write("+miljon punktow\n");
                    Points += 100;
                }
            }
        }


        public CANTMOVE Collision()
        {
            CANTMOVE CanYouMove = CANTMOVE.GITGUT;
            for (int y = 0; y < figura.Size; y++)
            {
                for (int x = 0; x < figura.Size; x++)
                {
                    if (figura.CurrentPiece[y, x] != 0)
                    {

                        if (landed[(int)figura.position.Y + y, (int)figura.position.X + x] != 0)
                        {
                            return CANTMOVE.AtAll;

                        }
                        if ((figura.position.Y + y + 1 >= height) || (landed[(int)figura.position.Y + y + 1, (int)figura.position.X + x] != 0))
                        {
                            Place();
                            figura = new Figura();
                            return CANTMOVE.DOWN;
                        }
                        if (figura.position.X + x - 1 < 0)
                        {
                            CanYouMove = CANTMOVE.LEFT;
                        }
                        if (figura.position.X + x + 1 >= width)
                        {
                            CanYouMove = CANTMOVE.RIGHT;
                        }
                    }
                }
            }

            return CanYouMove;
        }

        public bool Update(GameTime gameTime)
        {
            ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            KeyBoardElapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            CANTMOVE OrCanYouQuestionMark = Collision(); 
            KeyboardState state = Keyboard.GetState();

            if (ElapsedTime > StepTime)
            {
                if (OrCanYouQuestionMark != CANTMOVE.DOWN)
                {
                    figura.MoveDown();
                }

                Console.Clear();
                Console.Write("Czas: " + (int)gameTime.TotalGameTime.TotalSeconds + " Punkty: " + Points + "\n");
                ElapsedTime = 0;
            }


            if (KeyBoardElapsedTime > 200)
            {
                if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.Up))
                {
                    KeyBoardElapsedTime = 0;
                }

                if (state.IsKeyDown(Keys.Down))
                {
                    ElapsedTime = StepTime + 1;
                    KeyBoardElapsedTime = 175;
                }
            }

            if(OrCanYouQuestionMark == CANTMOVE.AtAll)
            {
                Console.Write("kuniec\n");
                return false;
            }

            if (state.IsKeyDown(Keys.Right) && !previousState.IsKeyDown(Keys.Right) && OrCanYouQuestionMark != CANTMOVE.RIGHT)
            {
                figura.MoveRight();
            }
            if (state.IsKeyDown(Keys.Left) && !previousState.IsKeyDown(Keys.Left) && OrCanYouQuestionMark != CANTMOVE.LEFT)
            {
                figura.MoveLeft();
            }
            if (state.IsKeyDown(Keys.Up) && !previousState.IsKeyDown(Keys.Up))
            {
                figura.Rotate();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && !previousState.IsKeyDown(Keys.Down) && OrCanYouQuestionMark != CANTMOVE.DOWN)
            {
                figura.MoveDown();
            }

            RemoveCompleteLines();

            previousState = state;

            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    spriteBatch.Draw(image, new Vector2(x, y) * 32, Figura.Colors[landed[y, x]]);
                }
            }
            figura.Draw(spriteBatch);
        }

    }
}

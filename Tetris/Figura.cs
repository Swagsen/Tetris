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
    class Figura
    {
        public Vector2 position { get; protected set; }
        public static ContentManager content;

        Random rand;

        Texture2D image;

        public int CurrentColor { get; protected set; }

        public static Color[] Colors = 
        {
          Color.Transparent,  
          Color.Orange,       
          Color.Blue,         
          Color.Red,          
          Color.LightSkyBlue, 
          Color.Yellow,       
          Color.Magenta,      
          Color.LimeGreen     
        };

        List<int[,]> pieces;

        public int[,] CurrentPiece { get; protected set; }
        public int Size { get; protected set; }


        public Figura()
        {
            image = content.Load<Texture2D>("klocek");
            position = new Vector2(3, 0);
            rand = new Random();
            CurrentColor = rand.Next(7) + 1;

            pieces = new List<int[,]>();

            pieces.Add(new int[4, 4] {
                    {0, 0, 0, 0},
                    {1, 1, 1, 1},
                    {0, 0, 0, 0},
                    {0, 0, 0, 0}
                });

            pieces.Add(new int[3, 3] {
                    {0, 0, 1},
                    {1, 1, 1},
                    {0, 0, 0}
                });

            pieces.Add(new int[2, 2] {
                    {1, 1},
                    {1, 1}
                });

            pieces.Add(new int[3, 3] {
                    {0, 1, 1},
                    {1, 1, 0},
                    {0, 0, 0}
                });


            pieces.Add(new int[3, 3] {
                    {0, 1, 0},
                    {1, 1, 1},
                    {0, 0, 0}
                });

            pieces.Add(new int[3, 3] {
                    {1, 1, 0},
                    {0, 1, 1},
                    {0, 0, 0}
                });


            CurrentPiece = pieces[rand.Next(6)];
            Size = CurrentPiece.GetLength(0);
        }


        public void Rotate()
        {
            int[,] npiece = new int[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    npiece[j, i] = CurrentPiece[i, Size - 1 - j];
                }
            }

            CurrentPiece = npiece;
        }

        public void MoveLeft()
        {
            position = new Vector2(position.X - 1, position.Y);
        }

        public void MoveRight()
        {
            position = new Vector2(position.X + 1, position.Y);
        }

        public void MoveDown()
        {
            position = new Vector2(position.X, position.Y + 1);
        }


        public void Update()
        {
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (CurrentPiece[y, x] == 1)
                        spriteBatch.Draw(image, (new Vector2(x, y) + position) * 32, Colors[CurrentColor]);
                }
            }
        }
    }
}

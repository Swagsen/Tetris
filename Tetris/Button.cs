using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Button
    {
        Vector2 positionText;
        Color currentColor;
        public string Text { get; private set; }
        SpriteFont font;

        Texture2D textureArrow;
        Vector2 positionArrow;
        Vector2 sizeArrow;
        Rectangle rectangleArrow;

        public enum state { notSelected, selected, clicked };
        public state CurrentState { get;  set; }
        //public object Font { get; }

        public Button(Vector2 positionText, string text, SpriteFont font)
        {
            this.positionText = positionText;
            this.Text = text;
            this.font = font;

            CurrentState = state.notSelected;
            currentColor = Color.White;
        }

        public Button(Texture2D textureArrow, Vector2 positionText, string text, SpriteFont font)
        {
            this.textureArrow = textureArrow;
            this.positionText = positionText;
            this.Text = text;
            this.font = font;

            CurrentState = state.notSelected;
            currentColor = Color.White;


            positionArrow = new Vector2(200, positionText.Y);
            sizeArrow = new Vector2(font.MeasureString(text).Y, font.MeasureString(text).Y);

            rectangleArrow = new Rectangle((int)positionArrow.X, (int)positionArrow.Y, (int)sizeArrow.X, (int)sizeArrow.Y);
        }

        public void SetState(state _state)
        {
            CurrentState = _state;
        }

        public void Update()
        {
            if (CurrentState == state.notSelected)
            {
                currentColor = Color.White;
            }
            else if (CurrentState == state.selected)
            {
                currentColor = Color.Purple;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentState == state.selected)
            {
                spriteBatch.Draw(textureArrow, rectangleArrow, Color.Purple);
            }
            spriteBatch.DrawString(font, Text, positionText, currentColor);
        }
    }

}

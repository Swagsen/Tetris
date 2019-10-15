using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class ScoreScreen
    {


        Texture2D backgroundPicture;
        Button button;

        SpriteFont font;

        KeyboardState currentState;
        KeyboardState prevState;

        public ScoreScreen(ContentManager Content)
        {
            backgroundPicture = Content.Load<Texture2D>("BackgroundMenu1");



            font = Content.Load<SpriteFont>("MenuFont");

            button = new Button(Content.Load<Texture2D>("Arrow4"), new Vector2(10, 500), "BACK", Content.Load<SpriteFont>("MenuFont")) { CurrentState = Button.state.selected };

        }

        public void Update()
        {
            prevState = currentState;
            currentState = Keyboard.GetState();


            if (currentState.IsKeyDown(Keys.Enter) && currentState != prevState)
            {
                Game1.State = Game1.state.MENU;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundPicture, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(font, "TOP SCORE", new Vector2(0, 30), Color.White);

            for (int i = 0; i < Game1.Top5Score.Count(); i++)
            {
                spriteBatch.DrawString(font, Game1.Top5Score[i].ToString(), new Vector2(0, 30 + (i + 1) * (font.MeasureString(Game1.Top5Score[i].ToString()).Y)), Color.White);
            }

            button.Draw(spriteBatch);

        }
    }
}

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
    class GameOverScreen
    {


        Texture2D backgroundPicture;
        List<Button> buttons;
        List<Button> texts;
        SpriteFont font;

        int state;

        const int buttonBetween = 60;
        const int startingHeight = 100;

        KeyboardState currentState;
        KeyboardState prevState;

        public GameOverScreen(ContentManager Content)
        {
            backgroundPicture = Content.Load<Texture2D>("BackgroundMenu1");
            Texture2D _arrowTexture = Content.Load<Texture2D>("Arrow4");



            font = Content.Load<SpriteFont>("MenuFont");
            
            
            
            buttons = new List<Button>();
            buttons.Add(new Button(_arrowTexture, new Vector2(10, startingHeight +4* buttonBetween), "TAK", Content.Load<SpriteFont>("MenuFont")));
            buttons.Add(new Button(_arrowTexture, new Vector2(10, startingHeight + 5* buttonBetween), "NIE", Content.Load<SpriteFont>("MenuFont")));

            state = 0;
        }

        public void Update()
        {


            buttons[state].SetState(Button.state.notSelected);

            if (currentState.IsKeyDown(Keys.Down) && currentState != prevState)
            {
                state++;
                if (state > buttons.Count() - 1)
                    state = 0;
            }
            else if (currentState.IsKeyDown(Keys.Up) && currentState != prevState)
            {
                state--;
                if (state < 0)
                    state = buttons.Count() - 1;
            }

            buttons[state].SetState(Button.state.selected);

            foreach (Button button in buttons)
            {
                button.Update();


            }

            if (currentState.IsKeyDown(Keys.Enter) && currentState != prevState)
            {
                Game1.Restart = true;
                Game1.Top5Score[4] = Game1.CurrentScore;
                Game1.Top5Score.Sort((a, b) => -1 * a.CompareTo(b)); //sortowanko
                switch (buttons[state].Text)
                {
                    case "TAK":
                        Game1.State = Game1.state.GAME;
                        break;
                    case "NIE":
                        Game1.State = Game1.state.MENU;
                        break;
     
                }
            }
            //buttons[state].SetState(Button.state.selected);

            prevState = currentState;
            currentState = Keyboard.GetState();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundPicture, new Vector2(0, 0), Color.White);

            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font, "SCORE:", new Vector2(0, 30), Color.White);
            spriteBatch.DrawString(font, Game1.CurrentScore.ToString(), new Vector2(0, 30 + 1 * (font.MeasureString(Game1.CurrentScore.ToString()).Y)), Color.White);
            spriteBatch.DrawString(font, "Kontynuowac?", new Vector2(0, 30 + 2 *(font.MeasureString("Kontynuowac?").Y)), Color.White);

        }
    }
}








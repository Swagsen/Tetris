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
    class MenuScreen
    {
        Texture2D backgroundPicture;
        List<Button> buttons;

        int state;

        const int buttonBetween = 60;
        const int startingHeight = 200;

        KeyboardState currentState;
        KeyboardState prevState;

        public MenuScreen(ContentManager Content)
        {
            backgroundPicture = Content.Load<Texture2D>("BackgroundMenu1");
            Texture2D _arrowTexture = Content.Load<Texture2D>("Arrow4");

            buttons = new List<Button>();
            buttons.Add(new Button(_arrowTexture, new Vector2(10, startingHeight),  "START", Content.Load<SpriteFont>("MenuFont")));
            buttons.Add(new Button(_arrowTexture, new Vector2(10, startingHeight + buttonBetween), "SCORE", Content.Load<SpriteFont>("MenuFont")));
            buttons.Add(new Button(_arrowTexture, new Vector2(10, startingHeight + 2 * buttonBetween), "EXIT", Content.Load<SpriteFont>("MenuFont")));

            state = 0;
        }

        public void Update()
        {

            prevState = currentState;
            currentState = Keyboard.GetState();


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
                switch (buttons[state].Text)
                {
                    case "START":
                        Game1.State = Game1.state.GAME;
                        break;
                    case "SCORE":
                        Game1.State = Game1.state.SCORE;
                        break;
                    case "EXIT":
                        Game1.State = Game1.state.EXIT;
                        break;

                }
            }
            //buttons[state].SetState(Button.state.selected);

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundPicture, new Vector2(-330, 0), Color.White);

            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }

        }
    }
}

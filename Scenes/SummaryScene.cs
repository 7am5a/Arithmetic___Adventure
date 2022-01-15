using Arithmetic___Adventure.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arithmetic___Adventure.Scenes
{
    class SummaryScene : Component
    {

        Texture2D mouseCursor;

        public static int level = 0;

        public static int scoreSum1 = 0;
        public static int scoreSum2 = 0;
        public static int scoreSum3 = 0;

        public static int timeSum1 = 0;
        public static int timeSum2 = 0;
        public static int timeSum3 = 0;

        //napisy na pasku
        private SpriteFont gameFont;

        //tło
        private Texture2D backgroundMenu;
        private Rectangle backgroundMenuRect;

        //ograniczenie liczby przycisków odgórnie
        private const int MAX_BUTTONS = 2;

        //dodanie przycisków
        private Texture2D[] menuButtons = new Texture2D[MAX_BUTTONS];
        //stworzenie prostokąta wspierajacego tę teksturę
        private Rectangle[] menuButtonsRect = new Rectangle[MAX_BUTTONS];

        //dodanie myszki
        private MouseState mouseState, oldMouseState;
        private Rectangle mouseStateRect;

        internal override void LoadContent(ContentManager Content)
        {
            mouseCursor = Content.Load<Texture2D>("Textures/cursor1");

            //załadowanie czcionki
            gameFont = Content.Load<SpriteFont>("Fonts/TextFont");

            // załadowanie tego powyzej do gierki
            backgroundMenu = Content.Load<Texture2D>("Textures/MenuBackground1");
            backgroundMenuRect = new Rectangle(0, 0, Data.ScreenWid, Data.ScreenHei);
                        
            //tworzenie przycisków ale z gotowych bloczklów bez wpisywania napisów do środka
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i] = Content.Load<Texture2D>($"Textures/bttnSum{i}"); //"$nazwa_pliku{i}" //"Textures/$bttn{i}"
                menuButtonsRect[i] = new Rectangle((Data.ScreenWid / 2 - menuButtons[i].Width /2 - 30) + 60 * i + menuButtons[i].Width /2 * i, 570, menuButtons[i].Width / 2, menuButtons[i].Height / 2);
            }
        }

        internal override void Update(GameTime gameTime)
        {
            //klikanie
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseStateRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);


            //wyjście z gry - przycisk pierwszy (1)
            if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(menuButtonsRect[0]))
            {
                Data.CurrentState = Data.Scenes.Menu;
            }
            //wejście do gry - przycisk ostatni (2)
            else if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(menuButtonsRect[1]))
            {
                if(level == 1)
                {
                    Data.CurrentState = Data.Scenes.Story;
                }
                else if(level == 2)
                {
                    Data.CurrentState = Data.Scenes.Story;
                }
                else if (level == 3)
                {
                    Data.CurrentState = Data.Scenes.Menu;
                }

            }
                        
            
                        

        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            //rysowanie tła
            spriteBatch.Draw(backgroundMenu, backgroundMenuRect, Color.White);

            //rysowanie przycisków w pętli w zależnosci od poziomu gry
            if(level == 3)
            {
                spriteBatch.Draw(menuButtons[1], menuButtonsRect[1], Color.White);
                if (mouseStateRect.Intersects(menuButtonsRect[1]))
                {
                    spriteBatch.Draw(menuButtons[1], menuButtonsRect[1], Color.Gray);
                }
            }
            else
            {
                for (int i = 0; i < menuButtons.Length; i++)
                {
                    spriteBatch.Draw(menuButtons[i], menuButtonsRect[i], Color.White);
                    if (mouseStateRect.Intersects(menuButtonsRect[i]))
                    {
                        spriteBatch.Draw(menuButtons[i], menuButtonsRect[i], Color.Gray);
                    }
                }
            }
            

            //rysowanie liczby punktów w zależności od ukończonego poziomu
            spriteBatch.DrawString(gameFont, "Wynik: ", new Vector2(Data.ScreenWid / 2, 310), Color.Black);
            spriteBatch.DrawString(gameFont, "Czas: ", new Vector2(Data.ScreenWid / 2, 380), Color.Black);

            if (level == 1)
            {
                spriteBatch.DrawString(gameFont, scoreSum1.ToString(), new Vector2(Data.ScreenWid / 2, 345), Color.Black);
                spriteBatch.DrawString(gameFont, timeSum1.ToString(), new Vector2(Data.ScreenWid / 2, 415), Color.Black);
            }
            else if (level == 2)
            {
                spriteBatch.DrawString(gameFont, scoreSum2.ToString(), new Vector2(Data.ScreenWid / 2, 345), Color.Black);
                spriteBatch.DrawString(gameFont, timeSum2.ToString(), new Vector2(Data.ScreenWid / 2, 415), Color.Black);
            }
            else if (level == 3)
            {
                spriteBatch.DrawString(gameFont, (scoreSum1 + scoreSum2 + scoreSum3).ToString(), new Vector2(Data.ScreenWid / 2, 345), Color.Black);
                spriteBatch.DrawString(gameFont, (timeSum1 + timeSum2 + timeSum3).ToString(), new Vector2(Data.ScreenWid / 2, 415), Color.Black);
            }

            //rysowanie kursora myszy
            spriteBatch.Draw(mouseCursor, new Vector2(mouseState.X, mouseState.Y), Color.White);
        }

    }
}

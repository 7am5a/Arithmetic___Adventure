using Arithmetic___Adventure.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arithmetic___Adventure.Scenes
{/// <summary>
/// Klasa do obsługi menu gry
/// </summary>
    internal class MenuScene : Component
    {
        /// <summary>
        /// Zmienna do obsługi grafiki kursora myszy
        /// </summary>
        Texture2D mouseCursor;
        //tło
        /// <summary>
        /// Zmienne do obsługi tła
        /// </summary>
        private Texture2D backgroundMenu;
        private Rectangle backgroundMenuRect;

        //ograniczenie liczby przycisków odgórnie
        private const int MAX_BUTTONS = 3;

        //dodanie przycisków
        /// <summary>
        /// Utworzenie tablicy przycisków w menu
        /// </summary>
        private Texture2D[] menuButtons = new Texture2D[MAX_BUTTONS];
        //stworzenie prostokąta wspierajacego tę teksturę
        private Rectangle[] menuButtonsRect = new Rectangle[MAX_BUTTONS];

        //dodanie myszki
        /// <summary>
        /// Zmienne do obsługi myszy
        /// </summary>
        private MouseState mouseState, oldMouseState;
        private Rectangle mouseStateRect;

        /// <summary>
        /// Załadowanie grafik kursora, tła menu, oraz przycisków
        /// </summary>
        /// <param name="Content"></param>
        internal override void LoadContent(ContentManager Content)
        {
            // załadowanie tego powyzej do gierki
            mouseCursor = Content.Load<Texture2D>("Textures/cursor1");

            backgroundMenu = Content.Load<Texture2D>("Textures/MenuBackground2");
            backgroundMenuRect = new Rectangle(0, 0, Data.ScreenWid, Data.ScreenHei);

            const int INCREMENT_VALUE = 125;
            //tworzenie przycisków ale z gotowych bloczklów bez wpisywania napisów do środka
            for(int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i] = Content.Load<Texture2D>($"Textures/bttn{i}"); //"$nazwa_pliku{i}" //"Textures/$bttn{i}"
                menuButtonsRect[i] = new Rectangle(Data.ScreenWid /2 - menuButtons[i].Width /4, 200 + INCREMENT_VALUE * i, menuButtons[i].Width /2, menuButtons[i].Height /2);
            }
        }
        /// <summary>
        /// Aktualizowanie w czasie rzeczywistym stanu przycisków i wykonywanie akcj im przypisanych
        /// </summary>
        /// <param name="gameTime"></param>
        internal override void Update(GameTime gameTime)
        {
            //klikanie
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseStateRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            //wejście do gry - przycisk pierwszy (1)
            if(mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(menuButtonsRect[0]))
            {
                Data.CurrentState = Data.Scenes.Story;
                
            }

            //wejście do informacji o sterowaniu - przycisk drugi (2)
            else if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(menuButtonsRect[1]))
            {
                Data.CurrentState = Data.Scenes.HowToPlay;

            }

            //wyjście z gry - przycisk ostatni (3)
            else if(mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(menuButtonsRect[2]))
            {
                Data.Exit = true;
            }

        }
        /// <summary>
        /// Wyrysowanie grafik przycisków, tła oraz kursora myszy
        /// </summary>
        /// <param name="spriteBatch"></param>
        internal override void Draw(SpriteBatch spriteBatch)
        {
            
            //rysowanie tła
            spriteBatch.Draw(backgroundMenu,backgroundMenuRect,Color.White);
            
            //rysowanie przycisków w pętli
            for (int i = 0; i < menuButtons.Length; i++)
            {
                spriteBatch.Draw(menuButtons[i], menuButtonsRect[i], Color.White);
                if (mouseStateRect.Intersects(menuButtonsRect[i]))
                {
                    spriteBatch.Draw(menuButtons[i], menuButtonsRect[i], Color.Gray);
                }
            }

            //rysowanie kursora myszy
            spriteBatch.Draw(mouseCursor, new Vector2(mouseState.X, mouseState.Y), Color.White);
        }

    }
}

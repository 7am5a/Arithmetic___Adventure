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
    class InfoScene : Component
    {
        //kursor myszy
        private Texture2D mouseCursor;

        //ekran informacyjny
        private Texture2D infoSample;
        private Rectangle infoSampleRect;

        //tło
        private Texture2D backgroundMenu;
        private Rectangle backgroundMenuRect;
               
        //dodanie przycisków
        private Texture2D menuButtons;
        //stworzenie prostokąta wspierajacego tę teksturę
        private Rectangle menuButtonsRect;

        //dodanie myszki
        private MouseState mouseState, oldMouseState;
        private Rectangle mouseStateRect;

        internal override void LoadContent(ContentManager Content)
        {
            // załadowanie tego powyzej do gierki
            mouseCursor = Content.Load<Texture2D>("Textures/cursor1");

            infoSample = Content.Load<Texture2D>("Textures/zasady");
            infoSampleRect = new Rectangle(100,50, Data.ScreenWid - 200, Data.ScreenHei - 180);

            backgroundMenu = Content.Load<Texture2D>("Textures/MenuBackground2");
            backgroundMenuRect = new Rectangle(0, 0, Data.ScreenWid, Data.ScreenHei);

            menuButtons = Content.Load<Texture2D>($"Textures/bttnSum0"); //"$nazwa_pliku{i}" //"Textures/$bttn{i}"
            menuButtonsRect = new Rectangle(Data.ScreenWid / 2 - menuButtons.Width /4, 600, menuButtons.Width / 2, menuButtons.Height / 2);
            
        }

        internal override void Update(GameTime gameTime)
        {
            //klikanie
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseStateRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            //wyjście do menu

            if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(menuButtonsRect))
            {
                Data.CurrentState = Data.Scenes.Menu;
            }

        }

        internal override void Draw(SpriteBatch spriteBatch)
        {

            //rysowanie tła
            spriteBatch.Draw(backgroundMenu, backgroundMenuRect, Color.White);

            //rysowanie przycisku
            
                spriteBatch.Draw(menuButtons, menuButtonsRect, Color.White);
                if (mouseStateRect.Intersects(menuButtonsRect))
                {
                    spriteBatch.Draw(menuButtons, menuButtonsRect, Color.Gray);
                }

            //rysowanie pola z informacjami
            spriteBatch.Draw(infoSample, infoSampleRect, Color.White);

            //rysowanie kursora myszy
            spriteBatch.Draw(mouseCursor, new Vector2(mouseState.X, mouseState.Y), Color.White);
        }

    }
}

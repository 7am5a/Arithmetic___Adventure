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
            backgroundMenu = Content.Load<Texture2D>("Textures/MenuBackground1");
            backgroundMenuRect = new Rectangle(0, 0, Data.ScreenWid, Data.ScreenHei);

            menuButtons = Content.Load<Texture2D>($"Textures/bttn1"); //"$nazwa_pliku{i}" //"Textures/$bttn{i}"
            menuButtonsRect = new Rectangle(Data.ScreenWid / 2 - menuButtons.Height / 2, 600, menuButtons.Width / 2, menuButtons.Height / 2);
            
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
            


        }

    }
}

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
    internal class GameScene : Component
    {

        //tło
        private Texture2D tabInfo;
        private Rectangle tabInfoRect;

        //lista z informacjami
        private Texture2D listInfo;
        private Rectangle listInfoRect;

        //napisy na pasku
        private SpriteFont gameFont;
        //private Rectangle gameNameRect;

        //menu na dole--------------------------------
        //ograniczenie liczby przycisków odgórnie
        private const int MAX_BUTTONS = 3;

        //dodanie przycisków
        private Texture2D[] menuButtons = new Texture2D[MAX_BUTTONS];
        //stworzenie prostokąta wspierajacego tę teksturę
        private Rectangle[] menuButtonsRect = new Rectangle[MAX_BUTTONS];

        //dodanie myszki
        private MouseState mouseState, oldMouseState;
        private Rectangle mouseStateRect;
        //-----------------------------------------


        internal override void LoadContent(ContentManager Content)
        {
            // załadowanie grafiki z tabelką do gierki 1280 x 75 px
            tabInfo = Content.Load<Texture2D>("Textures/tab_info");
            tabInfoRect = new Rectangle(0, Data.ScreenHei-75, Data.ScreenWid, 75);

            // załadowanie grafiki z listą do gierki 400 x 400 px
            listInfo = Content.Load<Texture2D>("Textures/zwoj_info");
            listInfoRect = new Rectangle(1280-250, -20, 250, 250);

            //załadowanie czcionki
            gameFont = Content.Load<SpriteFont>("Fonts/TextFont");

            //przyciski na dole-----------------------
            
            //tworzenie przycisków ale z gotowych bloczklów bez wpisywania napisów do środka
            menuButtons[0] = Content.Load<Texture2D>("Textures/pusty_0");
            menuButtonsRect[0] = new Rectangle(Data.ScreenWid * 2 / 5 + 25, Data.ScreenHei - 77, 190, 75);

            menuButtons[1] = Content.Load<Texture2D>($"Textures/pusty_1");
            menuButtonsRect[1] = new Rectangle(Data.ScreenWid * 2 / 5 + 37 + 190 - 5, Data.ScreenHei - 77, 246, 75);
            
            menuButtons[2] = Content.Load<Texture2D>($"Textures/pusty_2"); 
            menuButtonsRect[2] = new Rectangle(Data.ScreenWid * 2 / 5 + 45 + 190*2 - 5 + 57, Data.ScreenHei - 77 , 283, 75);
            
            //---------------------------------------
        }
        
        internal override void Update(GameTime gameTime)
        {
            //menu na dole--------------
            //klikanie
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseStateRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            
            //wejscie do menu
            if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(menuButtonsRect[0]))
            {
                Data.CurrentState = Data.Scenes.Menu;
            }

            //wyjście z gry - przycisk drugi (2)
            else if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(menuButtonsRect[1]))
            {
                Data.Exit = true;
            }

            //rozpoczęcie od nowa - przycisk trzeci (3)
            else if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(menuButtonsRect[2]))
            {
                Data.CurrentState = Data.Scenes.Game;
            }
            
            //--------------------------
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {

            //rysowanie tabelki
            spriteBatch.Draw(tabInfo, tabInfoRect, Color.White);

            //rysowanie tekstu na tebelce
            spriteBatch.DrawString(gameFont, "Arithmetic Adventure", new Vector2(160, 720 - 47), Color.Black);

            spriteBatch.DrawString(gameFont, "Menu", new Vector2(605, 720 - 47), Color.Black);

            spriteBatch.DrawString(gameFont, "Koniec gry", new Vector2(810, 720 - 47), Color.Black);

            spriteBatch.DrawString(gameFont, "Zacznij od nowa", new Vector2(1055, 720 - 47), Color.Black);


            //rysowanie listy
            spriteBatch.Draw(listInfo, listInfoRect, Color.White);

            //rysowanie przycisków w pętli----------------------------
            for (int i = 0; i < 3; i++)
            {
                spriteBatch.Draw(menuButtons[i], menuButtonsRect[i], Color.White);
                if (mouseStateRect.Intersects(menuButtonsRect[i]))
                {
                    spriteBatch.Draw(menuButtons[i], menuButtonsRect[i], Color.Gray);
                }
            }
            //-------------------------------------------------------

        }
                
    }
}

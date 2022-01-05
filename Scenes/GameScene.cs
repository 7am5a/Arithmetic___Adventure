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
        private Texture2D backgroundGame;
        private Rectangle backgroundGameRect;

        //tabelka
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

        //dodanie timera
        double timer = 0;


        const int LEVEL = 7; 
        //dodanie cegieł---------------------------        
        private Texture2D[] brick = new Texture2D[LEVEL];
        //stworzenie prostokąta wspierajacego tę teksturę
        private Rectangle[] brickRect = new Rectangle[LEVEL];
        //-----------------------------------------

        internal override void LoadContent(ContentManager Content)
        {
            timer = 0;

            // załadowanie tego powyzej do gierki
            backgroundGame = Content.Load<Texture2D>("Textures/tlo1");
            backgroundGameRect = new Rectangle(0, 0, Data.ScreenWid, Data.ScreenHei);

            // załadowanie grafiki z tabelką do gierki 1280 x 75 px
            tabInfo = Content.Load<Texture2D>("Textures/tab_info");
            tabInfoRect = new Rectangle(0, Data.ScreenHei-75, Data.ScreenWid, 75);

            // załadowanie grafiki z listą do gierki 400 x 400 px ale 250 x 250
            listInfo = Content.Load<Texture2D>("Textures/zwoj_info");
            listInfoRect = new Rectangle(1280-250, -20, 250, 250);

            //załadowanie czcionki
            gameFont = Content.Load<SpriteFont>("Fonts/TextFont");

            //przyciski na dole-----------------------
            
            //tworzenie przycisków ale z gotowych bloczklów bez wpisywania napisów do środka
            menuButtons[0] = Content.Load<Texture2D>("Textures/pusty_0");
            menuButtonsRect[0] = new Rectangle(Data.ScreenWid * 2 / 5 + 25, Data.ScreenHei - 77, 190, 75);

            menuButtons[1] = Content.Load<Texture2D>("Textures/pusty_1");
            menuButtonsRect[1] = new Rectangle(Data.ScreenWid * 2 / 5 + 37 + 190 - 5, Data.ScreenHei - 77, 246, 75);
            
            menuButtons[2] = Content.Load<Texture2D>("Textures/pusty_2"); 
            menuButtonsRect[2] = new Rectangle(Data.ScreenWid * 2 / 5 + 45 + 190*2 - 5 + 57, Data.ScreenHei - 77 , 283, 75);
            //---------------------------------------

            // załadowanie cegieł szer 70 lub 140 x 40
            //brick = Content.Load<Texture2D>("Textures/ceg1");
            //brickRect = new Rectangle(400, 400, 70, brick.Height);

            
            Random randBrickGen = new Random();
            int randBrick;

            int brickLvlUp = 0;
            int brickLicznik = 0;
            
            for (int i = 0; i < LEVEL; i++)
            {

                randBrick = randBrickGen.Next(1, 4);
                brick[i] = Content.Load<Texture2D>($"Textures/ceg{randBrick}");

                //zwiększanie poziomu budowania--------------------------------------------
                
                //rysowanie nieparzystych rzędów (liczymy od 0 :p)
                if(brickLvlUp % 2 == 0)
                {
                    brickRect[i] = new Rectangle(250 + 6 * (i % 5) + ((i % 5) * brick[i].Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 140, 40);
                    brickLicznik += 1;

                    if(brickLicznik == 5)
                    {
                        brickLvlUp += 1;
                        brickLicznik = 0;
                    }
                }

                //rysowanie parzystych rzędów----------------
                else if (brickLvlUp % 2 == 1)
                {
                    if (brickLicznik == 1)
                    {
                        brickRect[i] = new Rectangle(250 + 6 * (i % 6) + ((i % 6) * brick[i].Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                        brickLicznik += 1;
                    }
                    else if(brickLicznik == 0)
                    {
                        brickRect[i] = new Rectangle(180 + 6 * (i % 6) + ((i % 6) * brick[i].Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                        brickLicznik += 1;

                        if (brickLicznik == 6)
                        {
                            brickLvlUp += 1;
                            brickLicznik = 0;
                        }
                    }
                    
                    else 
                    {
                        brickRect[i] = new Rectangle(320 + 6 * (i % 6) + ((i % 6) * brick[i].Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 140, 40);
                        brickLicznik += 1;
                    }

                }

            }

        }
        
        internal override void Update(GameTime gameTime)
        {
            //menu na dole---------------------------
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
                //Data.CurrentState = Data.Scenes.Menu;
                Data.CurrentState = Data.Scenes.Game;
                timer = 0;
            }
            //----------------------------------

            //timer
            timer = timer + gameTime.ElapsedGameTime.TotalSeconds;

        }

        internal override void Draw(SpriteBatch spriteBatch)
        {

            //rysowanie tła
            spriteBatch.Draw(backgroundGame, backgroundGameRect, Color.White);


            //rysowanie tabelki
            spriteBatch.Draw(tabInfo, tabInfoRect, Color.White);

            //rysowanie tekstu na tebelce
            spriteBatch.DrawString(gameFont, "Arithmetic Adventure", new Vector2(160, 720 - 47), Color.Black);

            spriteBatch.DrawString(gameFont, "Menu", new Vector2(605, 720 - 47), Color.Black);

            spriteBatch.DrawString(gameFont, "Koniec gry", new Vector2(810, 720 - 47), Color.Black);

            spriteBatch.DrawString(gameFont, "Zacznij od nowa", new Vector2(1055, 720 - 47), Color.Black);

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

            //rysowanie cegieł
            for(int i = 0; i < LEVEL; i++)
            {
                spriteBatch.Draw(brick[i], brickRect[i], Color.White);
            }
            
            
            //rysowanie listy
            spriteBatch.Draw(listInfo, listInfoRect, Color.White);

            //rysowanie licznika
            spriteBatch.DrawString(gameFont, "Czas gry: ", new Vector2(1115, 35), Color.Black);
            spriteBatch.DrawString(gameFont, Math.Ceiling(timer).ToString(), new Vector2(1145, 70), Color.Black);

            //rysowanie liczby punktów
            spriteBatch.DrawString(gameFont, "Punkty: ", new Vector2(1115, 110), Color.Black);

        }
                
    }
}

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
    class GameScene3 : Component
    {
        
        //zmienna do wyświetlania wyniku
        public int level = 3;

        //zmienna do zmiany sceny
        int sceneChange = 0;

        //losowanie pocisku
        Random randBulletGen = new Random();
        int randBullet;

        //zmienne do punktów
        int score = 0;
        const int BASIC_SCORE_VALUE = 20;
        int preTimer = 0;
        bool mousesReleased = true;

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


        const int LEVEL = 11;
        const int LEVEL_X = 6;
        const int LEVEL_Y = 10;
        //dodanie cegieł---------------------------
        //utworzenie tablicy
        private Brick[] brickCastle = new Brick[LEVEL];
        private EquationGenerator[] eqGen = new EquationGenerator[LEVEL];


        //-----------------------------------------

        internal override void LoadContent(ContentManager Content)
        {
            //wygenerowanie pierwszej zmiennej
            randBullet = randBulletGen.Next(0, LEVEL);


            //brickCastle = new List<Brick>();


            timer = 0;

            // załadowanie tła
            backgroundGame = Content.Load<Texture2D>("Textures/tlo1");
            backgroundGameRect = new Rectangle(0, 0, Data.ScreenWid, Data.ScreenHei);

            // załadowanie grafiki z tabelką do gierki 1280 x 75 px
            tabInfo = Content.Load<Texture2D>("Textures/tab_info");
            tabInfoRect = new Rectangle(0, Data.ScreenHei - 75, Data.ScreenWid, 75);

            // załadowanie grafiki z listą do gierki 400 x 400 px ale 250 x 250
            listInfo = Content.Load<Texture2D>("Textures/zwoj_info");
            listInfoRect = new Rectangle(1280 - 250, -20, 250, 250);

            //załadowanie czcionki
            gameFont = Content.Load<SpriteFont>("Fonts/TextFont");

            //przyciski na dole-----------------------

            //tworzenie przycisków ale z gotowych bloczklów bez wpisywania napisów do środka
            menuButtons[0] = Content.Load<Texture2D>("Textures/pusty_0");
            menuButtonsRect[0] = new Rectangle(Data.ScreenWid * 2 / 5 + 25, Data.ScreenHei - 77, 190, 75);

            menuButtons[1] = Content.Load<Texture2D>("Textures/pusty_1");
            menuButtonsRect[1] = new Rectangle(Data.ScreenWid * 2 / 5 + 37 + 190 - 5, Data.ScreenHei - 77, 246, 75);

            menuButtons[2] = Content.Load<Texture2D>("Textures/pusty_2");
            menuButtonsRect[2] = new Rectangle(Data.ScreenWid * 2 / 5 + 45 + 190 * 2 - 5 + 57, Data.ScreenHei - 77, 283, 75);
            //---------------------------------------
            //zmienna do losowego wybierania tekstury cegły            
            Random randBrickGen = new Random();
            int randBrick;

            int brickLvlUp = 0;
            int brickLicznik = 0;




            for (int i = 0; i < LEVEL; i++)
            {
                brickCastle[i] = new Brick(false, true);
                eqGen[i] = new EquationGenerator();

                randBrick = randBrickGen.Next(1, 4);
                brickCastle[i].brickTexture = Content.Load<Texture2D>($"Textures/ceg{randBrick}");

                //rysowanie nieparzystych rzędów (liczymy od 0 :p)
                if (brickLvlUp % 2 == 0)
                {
                    brickCastle[i].brickRect = new Rectangle(250 + 7 * (i % 5) + ((i % 5) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 140, 40);
                    brickLicznik += 1;

                    if (brickLicznik == 5)
                    {
                        brickLvlUp += 1;
                        brickLicznik = 0;
                    }
                }
                //rysowanie parzystych rzędów----------------
                else if (brickLvlUp % 2 == 1)
                {
                    if (brickLicznik == 0)
                    {
                        brickCastle[i].brickRect = new Rectangle(250 + 7 * (brickLicznik % 6) + ((brickLicznik % 6) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                        brickLicznik += 1;
                    }
                    else if (brickLicznik == 5)
                    {
                        brickCastle[i].brickRect = new Rectangle(180 + 3 + 7 * (brickLicznik % 6) + ((brickLicznik % 6) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                        brickLicznik += 1;

                        if (brickLicznik == 6)
                        {
                            brickLvlUp += 1;
                            brickLicznik = 0;
                        }
                    }
                    else
                    {
                        brickCastle[i].brickRect = new Rectangle(180 + 7 * (brickLicznik % 6) + ((brickLicznik % 6) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 140, 40);
                        brickLicznik += 1;
                    }
                }

                //przypisanie równań i rozwiązań odpowiednim cegłom
                if (brickCastle[i].exist == true)
                {
                    brickCastle[i].equationAnswer = eqGen[i].GenereEQ(3);
                    brickCastle[i].equation = eqGen[i].StringEQ();
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
            else if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(menuButtonsRect[2]) && mousesReleased == true)
            {
                Data.CurrentState = Data.Scenes.Game3;
                timer = 0;
                score = 0; for (int i = 0; i < LEVEL; i++)
                {
                    brickCastle[i].isClicked = false;
                    brickCastle[i].exist = true;
                    randBullet = randBulletGen.Next(0, LEVEL);

                    brickCastle[i].equationAnswer = eqGen[i].GenereEQ(3);
                    brickCastle[i].equation = eqGen[i].StringEQ();
                    mousesReleased = false;
                }

            }
            //----------------------------------

            //timer
            timer = timer + gameTime.ElapsedGameTime.TotalSeconds;

            //strzelanie-----------------------------------------------------------------------                        
            //wystrzelenie, jeśli wynik się zgadza to + pkt
            if (mouseState.LeftButton == ButtonState.Pressed && mousesReleased == true)
            {
                for (int i = 0; i < LEVEL; i++)
                {

                    if (brickCastle[randBullet].exist == false)
                    {
                        randBullet = randBulletGen.Next(0, LEVEL);
                    }
                    //zdobycie punktu, gdy wynik się zgadza
                    else if (mouseStateRect.Intersects(brickCastle[i].brickRect) && brickCastle[i].isClicked == false && brickCastle[randBullet].equationAnswer == brickCastle[i].equationAnswer)
                    {
                        //losowanie następnego wyniku
                        randBullet = randBulletGen.Next(0, LEVEL);

                        //przyznanie punktów w zależności od czasu
                        if ((int)Math.Ceiling(timer) - preTimer <= 7)
                        {
                            score = score + BASIC_SCORE_VALUE;
                            //przypisanie do zmiennej pomocniczej obecnego stanu timera
                            preTimer = (int)Math.Ceiling(timer);
                        }
                        else if ((int)Math.Ceiling(timer) - preTimer <= 12)
                        {
                            score = score + BASIC_SCORE_VALUE * 3 / 4;
                            preTimer = (int)Math.Ceiling(timer);
                        }
                        else if ((int)Math.Ceiling(timer) - preTimer <= 16)
                        {
                            score = score + BASIC_SCORE_VALUE / 2;
                            preTimer = (int)Math.Ceiling(timer);
                        }
                        else
                        {
                            score = score + BASIC_SCORE_VALUE / 4;
                            preTimer = (int)Math.Ceiling(timer);
                        }


                        //zabezpieczenie przed multiclickiem
                        mousesReleased = false;

                        brickCastle[i].isClicked = true;
                        brickCastle[i].exist = false;

                        //inkrementacja zmiennej wykorzystywanej do zmiany sceny                        
                        if (brickCastle[i].exist == false)
                        {
                            sceneChange++;
                        }

                        while (brickCastle[randBullet].exist == false && sceneChange != LEVEL)
                        {
                            randBullet = randBulletGen.Next(0, LEVEL);
                        }
                    }
                }
            }

            //zwolnienie zabezpieczenia multiclick
            if (mouseState.LeftButton == ButtonState.Released)
            {
                mousesReleased = true;
            }
            //------------------------------------------------------------------------------------------------------------

            //zmiana sceny po zniknięciu wszystkich cegiełek
            if (sceneChange == LEVEL)
            {
                SummaryScene.scoreSum3 = score;
                SummaryScene.level = level;
                //wyczyszczenie poziomu
                timer = 0;
                score = 0;
                for (int i = 0; i < LEVEL; i++)
                {
                    brickCastle[i].isClicked = false;
                    brickCastle[i].exist = true;
                    randBullet = randBulletGen.Next(0, LEVEL);

                    brickCastle[i].equationAnswer = eqGen[i].GenereEQ(1);
                    brickCastle[i].equation = eqGen[i].StringEQ();
                    mousesReleased = false;
                }
                sceneChange = 0;

                Data.CurrentState = Data.Scenes.Summary;
            }

    }

        //RYSOWANIE------------------------------------------------------------------------------------------------------------------------------------------
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
            for (int i = 0; i < LEVEL; i++)
            {
                if (brickCastle[i].exist == true)
                {
                    spriteBatch.Draw(brickCastle[i].brickTexture, brickCastle[i].brickRect, Color.White);
                    spriteBatch.DrawString(gameFont, brickCastle[i].equation, new Vector2(brickCastle[i].brickRect.X + brickCastle[i].brickRect.Width / 2 - (gameFont.MeasureString(brickCastle[i].equation).X) / 2, brickCastle[i].brickRect.Y + (gameFont.MeasureString(brickCastle[i].equation).Y) / 2), Color.White);
                }
            }



            //ryswoanie wartości równania na pocisku
            spriteBatch.DrawString(gameFont, brickCastle[randBullet].equationAnswer.ToString(), new Vector2(10, 10), Color.Black);



            //rysowanie listy
            spriteBatch.Draw(listInfo, listInfoRect, Color.White);

            //rysowanie licznika
            spriteBatch.DrawString(gameFont, "Czas gry: ", new Vector2(1115, 35), Color.Black);
            spriteBatch.DrawString(gameFont, Math.Ceiling(timer).ToString(), new Vector2(1125, 70), Color.Black);

            //rysowanie liczby punktów
            spriteBatch.DrawString(gameFont, "Punkty: Ale 3 poziom ", new Vector2(1115, 110), Color.Black);
            spriteBatch.DrawString(gameFont, score.ToString(), new Vector2(1125, 145), Color.Black);

        }

    }
}

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
/// Druga grywalna scena
/// </summary>
    class GameScene2 : Component
    {/// <summary>
     /// Zmienna do obsługi grafiki kursora
     /// </summary>
        Texture2D mouseCursor;
        /// <summary>
        /// Zmienna do obsługi grafiki pocisku
        /// </summary>
        Texture2D ammoSprite;
        /// <summary>
        /// Zmienna do obsługi grafiki trebusza
        /// </summary>
        Texture2D trebuchetSprite;

        //zmienna do wyświetlania wyniku
        /// <summary>
        /// Zmienna do określania poziomu rozgrywki
        /// </summary>
        public int level = 2;

        /// <summary>
        /// Zmienna do zmiany sceny
        /// </summary>
        //zmienna do zmiany sceny
        int sceneChange = 0;

        //losowanie pocisku
        /// <summary>
        /// Losowanie wartości na pocisku
        /// </summary>
        Random randBulletGen = new Random();
        int randBullet;

        //zmienne do punktów
        /// <summary>
        /// Zmienne do obsługi punktów
        /// </summary>
        int score = 0;
        const int BASIC_SCORE_VALUE = 20;
        int preTimer = 0;
        bool mousesReleased = true;

        /// <summary>
        /// Zmienne do obsługi grafiki tła
        /// </summary>
        //tło
        private Texture2D backgroundGame;
        private Rectangle backgroundGameRect;

        //tabelka
        /// <summary>
        /// Zmienne do obsługi grafiki dolnej tabelki z przyciskami
        /// </summary>
        private Texture2D tabInfo;
        private Rectangle tabInfoRect;

        //lista z informacjami
        /// <summary>
        /// Zmienne do obsługi grafiki listy informacyjnej
        /// </summary>
        private Texture2D listInfo;
        private Rectangle listInfoRect;

        /// <summary>
        /// Zmienne do obsługi czcionki gry
        /// </summary>
        //napisy na pasku
        private SpriteFont gameFont;
        private SpriteFont ammoFont;

        //menu na dole--------------------------------
        //ograniczenie liczby przycisków odgórnie
        private const int MAX_BUTTONS = 3;

        /// <summary>
        /// Tablica przycisków na dolnym pasku
        /// </summary>
        //dodanie przycisków
        private Texture2D[] menuButtons = new Texture2D[MAX_BUTTONS];
        //stworzenie prostokąta wspierajacego tę teksturę
        private Rectangle[] menuButtonsRect = new Rectangle[MAX_BUTTONS];

        //dodanie myszki
        /// <summary>
        /// Zmienne do obsługi myszki
        /// </summary>
        private MouseState mouseState, oldMouseState;
        private Rectangle mouseStateRect;
        //-----------------------------------------

        //dodanie timera
        /// <summary>
        /// Zmienna do obsługi czasu rozgrywki
        /// </summary>
        double timer = 0;

        /// <summary>
        /// Liczba aktywnych cegiełek - z równaniamu
        /// </summary>
        //ustalenie liczby aktywnych cegiełek
        const int LEVEL = 35; //35+5

        //dodanie cegieł---------------------------
        //utworzenie tablicy
        /// <summary>
        /// Tablica cegiełek i dodatkowych elementów bez równań
        /// </summary>
        private Brick[] brickCastle = new Brick[LEVEL+5];
        private EquationGenerator[] eqGen = new EquationGenerator[LEVEL+5];


        //-----------------------------------------
        /// <summary>
        /// Załadowanie grafik kuli, celownika (kursor myszy), trebusza, tła, cegiełek i dodatkowych elementów, przycisków, listy informacyjnej, paska i czcionek. Rozlokowanie cegiełek i dodatkowych elementów
        /// </summary>
        /// <param name="Content"></param>
        internal override void LoadContent(ContentManager Content)
        {
            mouseCursor = Content.Load<Texture2D>("Textures/target");
            ammoSprite = Content.Load<Texture2D>("Textures/kula");
            trebuchetSprite = Content.Load<Texture2D>("Textures/trebusz");

            //wygenerowanie pierwszej zmiennej
            //randBullet = randBulletGen.Next(0, LEVEL);

            //wyzerowanie timera
            timer = 0;

            // załadowanie tła
            backgroundGame = Content.Load<Texture2D>("Textures/tlo2");
            backgroundGameRect = new Rectangle(0, 0, Data.ScreenWid, Data.ScreenHei);

            // załadowanie grafiki z tabelką do gierki 1280 x 75 px
            tabInfo = Content.Load<Texture2D>("Textures/tab_info");
            tabInfoRect = new Rectangle(0, Data.ScreenHei - 75, Data.ScreenWid, 75);

            // załadowanie grafiki z listą do gierki 400 x 400 px ale 250 x 250
            listInfo = Content.Load<Texture2D>("Textures/zwoj_info");
            listInfoRect = new Rectangle(1280 - 250, -20, 250, 250);

            //załadowanie czcionki
            gameFont = Content.Load<SpriteFont>("Fonts/TextFont");
            ammoFont = Content.Load<SpriteFont>("Fonts/AmmoFont");


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




            for (int i = 0; i < LEVEL+5; i++)
            {
                brickCastle[i] = new Brick(false, true);
                eqGen[i] = new EquationGenerator();

                if (i >= LEVEL)
                {
                    brickCastle[i].brickTexture = Content.Load<Texture2D>("Textures/dach");
                }
                else
                {
                    randBrick = randBrickGen.Next(1, 5);
                    brickCastle[i].brickTexture = Content.Load<Texture2D>($"Textures/ceg{randBrick}");
                }

                //pierwsze dwa rzędy

                if(i <= 10)
                {
                    //rysowanie nieparzystych rzędów (liczymy od 0 :p)
                    if (brickLvlUp % 2 == 0)
                    {
                        brickCastle[i].brickRect = new Rectangle(20 + 7 * (i % 5) + ((i % 5) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 140, 40);
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
                            brickCastle[i].brickRect = new Rectangle(20 + 7 * (brickLicznik % 6) + ((brickLicznik % 6) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                            brickLicznik += 1;
                        }
                        else if (brickLicznik == 5)
                        {
                            brickCastle[i].brickRect = new Rectangle(-50 +3 + 7 * (brickLicznik % 6) + ((brickLicznik % 6) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                            brickLicznik += 1;

                            if (brickLicznik == 6)
                            {
                                brickLvlUp += 1;
                                brickLicznik = 0;
                            }
                        }
                        else
                        {
                            brickCastle[i].brickRect = new Rectangle(-50 +7 * (brickLicznik % 6) + ((brickLicznik % 6) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 140, 40);
                            brickLicznik += 1;
                        }
                    }
                }
                else if (i <= 19)
                {
                    //rysowanie nieparzystych rzędów (liczymy od 0 :p)
                    if (brickLvlUp % 2 == 0)
                    {
                        brickCastle[i].brickRect = new Rectangle(20 + 7 * (brickLicznik % 4) + ((brickLicznik % 4) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 140, 40);
                        brickLicznik += 1;

                        if (brickLicznik == 4)
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
                            brickCastle[i].brickRect = new Rectangle(20 + 7 * (brickLicznik % 5) + ((brickLicznik % 5) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                            brickLicznik += 1;
                        }
                        else if (brickLicznik == 4)
                        {
                            brickCastle[i].brickRect = new Rectangle(-50 + 3 + 7 * (brickLicznik % 5) + ((brickLicznik % 5) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                            brickLicznik += 1;

                            if (brickLicznik == 5)
                            {
                                brickLvlUp += 1;
                                brickLicznik = 0;
                            }
                        }
                        else
                        {
                            brickCastle[i].brickRect = new Rectangle(-50 + 7 * (brickLicznik % 5) + ((brickLicznik % 5) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 140, 40);
                            brickLicznik += 1;
                        }
                    }
                }
                else if (i <= 26)
                {
                    //rysowanie nieparzystych rzędów (liczymy od 0 :p)
                    if (brickLvlUp % 2 == 0)
                    {
                        brickCastle[i].brickRect = new Rectangle(20 + 7 * (brickLicznik % 3) + ((brickLicznik % 3) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 140, 40);
                        brickLicznik += 1;

                        if (brickLicznik == 3)
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
                            brickCastle[i].brickRect = new Rectangle(20 + 7 * (brickLicznik % 4) + ((brickLicznik % 4) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                            brickLicznik += 1;
                        }
                        else if (brickLicznik == 3)
                        {
                            brickCastle[i].brickRect = new Rectangle(-50 + 3 + 7 * (brickLicznik % 4) + ((brickLicznik % 4) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                            brickLicznik += 1;

                            if (brickLicznik == 4)
                            {
                                brickLvlUp += 1;
                                brickLicznik = 0;
                            }
                        }
                        else
                        {
                            brickCastle[i].brickRect = new Rectangle(-50 + 7 * (brickLicznik % 4) + ((brickLicznik % 4) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 140, 40);
                            brickLicznik += 1;
                        }
                    }
                }
                else if (i <= 32)
                {
                    //rysowanie nieparzystych rzędów (liczymy od 0 :p)
                    if (brickLvlUp % 2 == 0)
                    {
                        brickCastle[i].brickRect = new Rectangle(20 + 7 * (brickLicznik % 2) + ((brickLicznik % 2) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 140, 40);
                        brickLicznik += 1;

                        if (brickLicznik == 2)
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
                            brickCastle[i].brickRect = new Rectangle(20 + 7 * (brickLicznik % 3) + ((brickLicznik % 3) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                            brickLicznik += 1;
                        }
                        else if (brickLicznik == 2)
                        {
                            brickCastle[i].brickRect = new Rectangle(-50 + 3 + 7 * (brickLicznik % 3) + ((brickLicznik % 3) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                            brickLicznik += 1;

                            if (brickLicznik == 3)
                            {
                                brickLvlUp += 1;
                                brickLicznik = 0;
                            }
                        }
                        else
                        {
                            brickCastle[i].brickRect = new Rectangle(-50 + 7 * (brickLicznik % 3) + ((brickLicznik % 3) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 140, 40);
                            brickLicznik += 1;
                        }
                    }
                }
                else if (i <= 34)
                {                    
                    if (brickLicznik == 0)
                    {
                        brickCastle[i].brickRect = new Rectangle(20 + 7 * (brickLicznik % 2) + ((brickLicznik % 2) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                        brickLicznik += 1;

                    }
                    else if (brickLicznik == 1)
                    {
                        brickLvlUp += 1;
                        brickCastle[i].brickRect = new Rectangle(-50 + 3 + 7 * (brickLicznik % 2) + ((brickLicznik % 2) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30, 70, 40);
                        brickLicznik = 0;
                    }
                }

                //przypisanie równań i rozwiązań odpowiednim cegłom
                if (brickCastle[i].exist == true && i != 35 && i != 36 && i != 37 && i != 38 && i != 39)
                {

                    brickCastle[i].equationAnswer = eqGen[i].GenereEQ(2);
                    brickCastle[i].equation = eqGen[i].StringEQ();
                }
            }

            //dorysowanie pustych elementów
            brickCastle[35].brickRect = new Rectangle(20, 120 - 2 * 9 - 2 * 30 - 2, 140, 140);
            brickCastle[36].brickRect = new Rectangle(20 + 3 + (brickCastle[36].brickTexture.Width * 2 / 3 - 5), 200 - 2 * 9 - 2 * 30, 140, 140);
            brickCastle[37].brickRect = new Rectangle(20 + 3 + 2 * (brickCastle[37].brickTexture.Width * 2 / 3 - 5), 280 - 2 * 9 - 2 * 30 - 3, 140, 140);
            brickCastle[38].brickRect = new Rectangle(20 + 3 + 3 * (brickCastle[38].brickTexture.Width * 2 / 3 - 4), 360 - 2 * 9 - 2 * 30 - 5, 140, 140);
            brickCastle[39].brickRect = new Rectangle(20 + 3 + 4 * (brickCastle[39].brickTexture.Width * 2 / 3 - 4), 440 - 2 * 9 - 2 * 30 - 7, 140, 140);

        }
        /// <summary>
        /// Aktualizowanie w czasie rzeczywistym przycisków i ich realizacja ich funkcji, zliczanie punktów, czasu, losowanie wyników oraz wykonanie przejścia do kolejnej sceny
        /// </summary>
        /// <param name="gameTime"></param>
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
                Data.CurrentState = Data.Scenes.Game2;
                timer = 0;
                score = 0;
                sceneChange = 0;

                for (int i = 0; i < LEVEL +5 ; i++)
                {
                    brickCastle[i].isClicked = false;
                    brickCastle[i].exist = true;
                    randBullet = randBulletGen.Next(0, LEVEL);

                    brickCastle[i].equationAnswer = eqGen[i].GenereEQ(2);
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
                for (int i = 0; i < LEVEL+5; i++)
                {

                    //skiknięcie w pusty obiekt
                    if (i >= LEVEL && mouseStateRect.Intersects(brickCastle[i].brickRect) && brickCastle[i].isClicked == false)
                    {
                        
                        //zabezpieczenie przed multiclickiem
                        mousesReleased = false;

                        brickCastle[i].isClicked = true;
                        brickCastle[i].exist = false;

                        //inkrementacja zmiennej wykorzystywanej do zmiany sceny                        
                        sceneChange++;

                        //zabezpieczenie przed nieskończonym losowaniem wyniku
                        if (sceneChange != LEVEL + 5)
                        {
                            while (brickCastle[randBullet].exist == false && sceneChange != LEVEL+5)
                            {
                                randBullet = randBulletGen.Next(0, LEVEL);
                            }
                        }
                    }

                    //zdobycie punktu, gdy wynik się zgadza
                    else if (mouseStateRect.Intersects(brickCastle[i].brickRect) && brickCastle[i].isClicked == false && brickCastle[randBullet].equationAnswer == brickCastle[i].equationAnswer)
                    {
                        randBullet = randBulletGen.Next(0, LEVEL);

                        //przyznanie punktów w zależności od czasu
                        if ((int)Math.Ceiling(timer) - preTimer <= 4)
                        {
                            score = score + BASIC_SCORE_VALUE;
                            //przypisanie do zmiennej pomocniczej obecnego stanu timera
                            preTimer = (int)Math.Ceiling(timer);
                        }
                        else if ((int)Math.Ceiling(timer) - preTimer <= 7)
                        {
                            score = score + BASIC_SCORE_VALUE * 3 / 4;
                            preTimer = (int)Math.Ceiling(timer);
                        }
                        else if ((int)Math.Ceiling(timer) - preTimer <= 9)
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
                        sceneChange++;

                        //zabezpieczenie przed nieskończonym losowaniem wyniku
                        if (sceneChange != LEVEL + 5)
                        {
                            while (brickCastle[randBullet].exist == false && sceneChange != LEVEL+5)
                            {
                                randBullet = randBulletGen.Next(0, LEVEL);
                            }
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
            if (sceneChange == LEVEL+5)
            {
                SummaryScene.scoreSum2 = score;
                SummaryScene.timeSum2 = (int)timer;
                SummaryScene.level = level;
                StoryScene.level = level;
                //wyczyszczenie poziomu
                timer = 0;
                score = 0;
                for (int i = 0; i < LEVEL+5; i++)
                {
                    brickCastle[i].isClicked = false;
                    brickCastle[i].exist = true;
                    randBullet = randBulletGen.Next(0, LEVEL);

                    brickCastle[i].equationAnswer = eqGen[i].GenereEQ(1);
                    brickCastle[i].equation = eqGen[i].StringEQ();
                    mousesReleased = false;
                }

                sceneChange = 0;

                //zmiana sceny
                Data.CurrentState = Data.Scenes.Summary;
            }

        }

        //RYSOWANIE------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Wyrysowanie grafik kuli, celownika (kursor myszy), trebusza, tła, cegiełek i dodatkowych elementów, przycisków, listy informacyjnej, paska i czcionek. Rozlokowanie cegiełek i dodatkowych elementów
        /// </summary>
        /// <param name="Content"></param>
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
            for (int i = 0; i < LEVEL + 5; i++)
            {
                if (brickCastle[i].exist == true && i < LEVEL)
                {
                    spriteBatch.Draw(brickCastle[i].brickTexture, brickCastle[i].brickRect, Color.White);
                    spriteBatch.DrawString(gameFont, brickCastle[i].equation, new Vector2(brickCastle[i].brickRect.X + brickCastle[i].brickRect.Width / 2 - (gameFont.MeasureString(brickCastle[i].equation).X) / 2, brickCastle[i].brickRect.Y + (gameFont.MeasureString(brickCastle[i].equation).Y) / 2), Color.White);
                }
                else if (brickCastle[i].exist == true && i >= LEVEL)
                {
                    spriteBatch.Draw(brickCastle[i].brickTexture, brickCastle[i].brickRect, Color.White);
                }
            }



            //ryswoanie wartości równania na pocisku
            spriteBatch.Draw(trebuchetSprite, new Vector2(Data.ScreenWid - 160 - 350 / 2, Data.ScreenHei - 210 - 257 / 2), Color.White);
            spriteBatch.Draw(ammoSprite, new Vector2(-25 + Data.ScreenWid - 55 - 30, -25 + Data.ScreenHei - 110 - 25), Color.White);
            spriteBatch.DrawString(ammoFont, brickCastle[randBullet].equationAnswer.ToString(), new Vector2(Data.ScreenWid - 59 - gameFont.MeasureString(brickCastle[randBullet].equationAnswer.ToString()).X / 2 - 30, -13 + Data.ScreenHei - 100 - gameFont.MeasureString(brickCastle[randBullet].equationAnswer.ToString()).Y / 2 - 25), Color.White);



            //rysowanie listy
            spriteBatch.Draw(listInfo, listInfoRect, Color.White);

            //rysowanie licznika
            spriteBatch.DrawString(gameFont, "Czas gry: ", new Vector2(1115, 35), Color.Black);
            spriteBatch.DrawString(gameFont, Math.Ceiling(timer).ToString(), new Vector2(1125, 70), Color.Black);

            //rysowanie liczby punktów
            spriteBatch.DrawString(gameFont, "Punkty: ", new Vector2(1115, 110), Color.Black);
            spriteBatch.DrawString(gameFont, score.ToString(), new Vector2(1125, 145), Color.Black);

            //rysowanie kursora myszy
            spriteBatch.Draw(mouseCursor, new Vector2(mouseState.X - 20, mouseState.Y - 20), Color.White);
        }

    }
}

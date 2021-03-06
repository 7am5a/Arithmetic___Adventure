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
/// Trzecia grywalna scena
/// </summary>
    class GameScene3 : Component
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
        public int level = 3;

        //zmienna do zmiany sceny
        /// <summary>
        /// Zmienna do zmiany sceny
        /// </summary>
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

        //tło
        /// <summary>
        /// Zmienne do obsługi grafiki tła
        /// </summary>
        private Texture2D backgroundGame;
        private Rectangle backgroundGameRect;

        //tabelka
        /// <summary>
        /// Zmienne do obsługi grafiki dolnej tabelki z przyciskami
        /// </summary>
        private Texture2D tabInfo;
        private Rectangle tabInfoRect;

        /// <summary>
        /// Zmienne do obsługi grafiki listy informacyjnej
        /// </summary>
        //lista z informacjami
        private Texture2D listInfo;
        private Rectangle listInfoRect;

        //napisy na pasku
        /// <summary>
        /// Zmienne do obsługi czcionki gry
        /// </summary>
        private SpriteFont gameFont;
        private SpriteFont ammoFont;

        //menu na dole--------------------------------
        //ograniczenie liczby przycisków odgórnie
        private const int MAX_BUTTONS = 3;

        //dodanie przycisków
        /// <summary>
        /// Tablica przycisków na dolnym pasku
        /// </summary>
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

        //ustalenie liczby aktywnych cegiełek
        /// <summary>
        /// Liczba aktywnych cegiełek - z równaniamu
        /// </summary>
        const int LEVEL = 38; //38+5

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
            randBullet = randBulletGen.Next(0, LEVEL);


            //brickCastle = new List<Brick>();


            timer = 0;

            // załadowanie tła
            backgroundGame = Content.Load<Texture2D>("Textures/tlo3");
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
                    if(i == 38 || i == 39)
                    {
                        brickCastle[i].brickTexture = Content.Load<Texture2D>("Textures/dach");
                    }
                    else
                    {
                        brickCastle[i].brickTexture = Content.Load<Texture2D>("Textures/murek");
                    }
                }
                else
                {
                    randBrick = randBrickGen.Next(1, 5);
                    brickCastle[i].brickTexture = Content.Load<Texture2D>($"Textures/ceg{randBrick}");
                }

                //rysowanie lewej wieży 
                if (i <= 11)
                {
                    //rysowanie nieparzystych rzędów (liczymy od 0 :p)
                    if (brickLvlUp % 2 == 0)
                    {
                        if (brickLicznik == 0)
                        {
                            brickCastle[i].brickRect = new Rectangle(5 + 7 * (brickLicznik % 3) + ((brickLicznik % 3) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30-20, 70, 40);
                            brickLicznik += 1;
                        }
                        else if(brickLicznik == 2)
                        {
                            brickCastle[i].brickRect = new Rectangle(-65 + 7 * (brickLicznik % 3) + ((brickLicznik % 3) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30-20, 140, 40);
                            brickLicznik += 1;

                            if (brickLicznik == 3)
                            {
                                brickLvlUp += 1;
                                brickLicznik = 0;
                            }
                        }
                        else
                        {
                            brickCastle[i].brickRect = new Rectangle(-65 + 7 * (brickLicznik % 3) + ((brickLicznik % 3) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30-20, 140, 40);
                            brickLicznik += 1;
                        }
                    }
                    //rysowanie parzystych rzędów----------------
                    else if (brickLvlUp % 2 == 1)
                    {
                        if (brickLicznik == 0)
                        {
                            brickCastle[i].brickRect = new Rectangle(5 + 7 * (brickLicznik % 3) + ((brickLicznik % 3) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30-20, 140, 40);
                            brickLicznik += 1;
                        }
                        else if (brickLicznik == 2)
                        {                            
                            brickCastle[i].brickRect = new Rectangle(5 + 7 * (brickLicznik % 3) + ((brickLicznik % 3) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30-20, 70, 40);
                            brickLicznik += 1;

                            if (brickLicznik == 3)
                            {
                                brickLvlUp += 1;
                                brickLicznik = 0;
                            }
                        }
                        else
                        {
                            brickCastle[i].brickRect = new Rectangle(5 + 7 * (brickLicznik % 3) + ((brickLicznik % 3) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30-20, 140, 40);
                            brickLicznik += 1;
                        }
                    }
                }
                else if(i <= 17)
                {
                    if (brickLvlUp % 2 == 0)
                    {
                        if (brickLicznik == 0)
                        {
                            brickCastle[i].brickRect = new Rectangle(5 + 7 * (brickLicznik % 2) + ((brickLicznik % 2) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30-20, 70, 40);
                            brickLicznik += 1;

                        }
                        else if (brickLicznik == 1)
                        {
                            brickCastle[i].brickRect = new Rectangle(-65 + 3 + 7 * (brickLicznik % 2) + ((brickLicznik % 2) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30-20, 140, 40);
                            brickLicznik += 1;

                            if (brickLicznik == 2)
                            {
                                brickLvlUp += 1;
                                brickLicznik = 0;
                            }
                        }
                    }
                    else if (brickLvlUp % 2 == 1)
                    {
                        if (brickLicznik == 0)
                        {                            
                            brickCastle[i].brickRect = new Rectangle(5  + 7 * (brickLicznik % 2) + ((brickLicznik % 2) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30 - 20, 140, 40);
                            brickLicznik += 1;
                        }
                        else if (brickLicznik == 1)
                        {
                            brickCastle[i].brickRect = new Rectangle(5 + 3 + 7 * (brickLicznik % 2) + ((brickLicznik % 2) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30 - 20, 70, 40);
                            brickLicznik += 1;

                            if (brickLicznik == 2)
                            {
                                brickLvlUp += 1;
                                brickLicznik = 0;
                            }
                        }
                    }
                }
                //rysowanie prawej wieży 
                else if(i <= 29)
                {
                    if (i == 18)
                        brickLvlUp = 0;

                    if (brickLvlUp % 2 == 0)
                    {
                        if (brickLicznik == 0)
                        {
                            brickCastle[i].brickRect = new Rectangle(380+5 + 7 * (brickLicznik % 4) + ((brickLicznik % 4) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30 - 20, 70, 40);
                            brickLicznik += 1;

                        }
                        else if (brickLicznik == 3)
                        {
                            brickCastle[i].brickRect = new Rectangle(380 - 65 + 3 + 7 * (brickLicznik % 4) + ((brickLicznik % 4) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30 - 20, 140, 40);
                            brickLicznik += 1;

                            if (brickLicznik == 4)
                            {
                                brickLvlUp += 1;
                                brickLicznik = 0;
                            }
                        }
                        else
                        {
                            brickCastle[i].brickRect = new Rectangle(380 - 65 + 3 + 7 * (brickLicznik % 4) + ((brickLicznik % 4) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30 - 20, 140, 40);
                            brickLicznik += 1;
                        }
                    }
                    else if (brickLvlUp % 2 == 1)
                    {
                        if (brickLicznik == 0)
                        {
                            brickCastle[i].brickRect = new Rectangle(380 + 5 + 7 * (brickLicznik % 4) + ((brickLicznik % 4) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30 - 20, 140, 40);
                            brickLicznik += 1;
                        }
                        else if (brickLicznik == 3)
                        {
                            brickCastle[i].brickRect = new Rectangle(380 + 5 +3+ 7 * (brickLicznik % 4) + ((brickLicznik % 4) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30 - 20, 70, 40);
                            brickLicznik += 1;

                            if (brickLicznik == 4)
                            {
                                brickLvlUp += 1;
                                brickLicznik = 0;
                            }
                        }
                        else
                        {
                            brickCastle[i].brickRect = new Rectangle(380 + 5 + 3 + 7 * (brickLicznik % 4) + ((brickLicznik % 4) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30 - 20, 140, 40);
                            brickLicznik += 1;
                        }
                    }
                }
                else if(i <= 37)
                {
                    if (brickLvlUp % 2 == 0)
                    {
                        if (brickLicznik == 0)
                        {
                            brickCastle[i].brickRect = new Rectangle(380 +143 + 5 + 7 * (brickLicznik % 2) + ((brickLicznik % 2) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30 - 20, 70, 40);
                            brickLicznik += 1;

                        }
                        else if (brickLicznik == 1)
                        {
                            brickCastle[i].brickRect = new Rectangle(315 + 143 + 3 + 7 * (brickLicznik % 2) + ((brickLicznik % 2) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30 - 20, 140, 40);
                            brickLicznik += 1;

                            if (brickLicznik == 2)
                            {
                                brickLvlUp += 1;
                                brickLicznik = 0;
                            }
                        }
                    }
                    else if (brickLvlUp % 2 == 1)
                    {
                        if (brickLicznik == 0)
                        {
                            brickCastle[i].brickRect = new Rectangle(380 + 143 + 5 + 7 * (brickLicznik % 2) + ((brickLicznik % 2) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30 - 20, 140, 40);
                            brickLicznik += 1;
                        }
                        else if (brickLicznik == 1)
                        {
                            brickCastle[i].brickRect = new Rectangle(380 + 143 + 5 + 3 + 7 * (brickLicznik % 2) + ((brickLicznik % 2) * brickCastle[i].brickTexture.Width * 2 / 3), 535 - brickLvlUp * 9 - brickLvlUp * 30 - 20, 70, 40);
                            brickLicznik += 1;

                            if (brickLicznik == 2)
                            {
                                brickLvlUp += 1;
                                brickLicznik = 0;
                            }
                        }
                    }
                }

                //przypisanie równań i rozwiązań odpowiednim cegłom
                if (brickCastle[i].exist == true)
                {
                    brickCastle[i].equationAnswer = eqGen[i].GenereEQ(3);
                    brickCastle[i].equation = eqGen[i].StringEQ();
                }
            }

            //dorysowanie pustych elementów
            //daszek
            brickCastle[38].brickRect = new Rectangle(5, 535 - 9 * 9 - 9 * 30 - 45, 217, 140);
            brickCastle[39].brickRect = new Rectangle(5 + 380 + 143, 535 - 9 * 9 - 9 * 30 - 45, 217, 140);
            //murek
            brickCastle[40].brickRect = new Rectangle(215 + 3, 535 - 4 * 9 - 4 * 30 - 20, 140, 40);
            brickCastle[41].brickRect = new Rectangle(5 + 380, 535 - 3 * 9 - 3 * 30 - 20, 140, 40);
            brickCastle[42].brickRect = new Rectangle(5 + 380 + 356, 535 - 3 * 9 - 3 * 30 - 20, 140, 40); //czy te-45 czy -25

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
                Data.CurrentState = Data.Scenes.Game3;
                timer = 0;
                score = 0;
                sceneChange = 0;

                for (int i = 0; i < LEVEL+5; i++)
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
                            while (brickCastle[randBullet].exist == false && sceneChange != LEVEL)
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
                SummaryScene.scoreSum3 = score;
                SummaryScene.timeSum3 = (int)timer;
                SummaryScene.level = level;
                StoryScene.level = level;
                StoryScene.timerSecurity = 0;
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

                //zmiana sceny
                Data.CurrentState = Data.Scenes.Story;
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

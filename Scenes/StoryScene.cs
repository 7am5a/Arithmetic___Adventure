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
/// Klasa wypisująca teksty fabulkarne w przejściach między scenami gry
/// </summary>
    class StoryScene : Component
    {

        //zmienna do zabezpieczenia przed zbyt szybkim przyspieszeniem scenki fabularnej
        /// <summary>
        /// Zmienna czasu - zabezpieczenie przed zbyt szybkim pominięciem
        /// </summary>
        public static double timerSecurity = 0;
        /// <summary>
        /// Zmienna czasu - służy do przewinięcia tekstu i przełączenia między scenami
        /// </summary>
        public static double sceneTimer = 0;

        //zmienna do sterowania wyświetlaniem kontentu
        /// <summary>
        /// Zmienna do sterowania wyświetaniem kontentu
        /// </summary>
        public static int level = 0;
        //tło
        /// <summary>
        /// Zmienne do obsługi tła
        /// </summary>
        private Texture2D backgroundMenu;
        private Rectangle backgroundMenuRect;

        //dodanie tekstu
        /// <summary>
        /// Zmienne do obsługi teksów fabularnych
        /// </summary>
        private Texture2D scene1a;
        private Rectangle scene1aRect;
        private Texture2D scene1b;
        private Rectangle scene1bRect;
        private Texture2D scene2;
        private Rectangle scene2Rect;
        private Texture2D scene3;
        private Rectangle scene3Rect;
        private Texture2D scene4a;
        private Rectangle scene4aRect;
        private Texture2D scene4b;
        private Rectangle scene4bRect;

        //dodanie myszki
        /// <summary>
        /// Zmienne do obsługi myszki
        /// </summary>
        private MouseState mouseState, oldMouseState;
        private Rectangle mouseStateRect;

        /// <summary>
        /// Załadowanie grafik fabularnych i tła sceny
        /// </summary>
        /// <param name="Content"></param>
        internal override void LoadContent(ContentManager Content)
        {
            // załadowanie tego powyzej do gierki
            
            backgroundMenu = Content.Load<Texture2D>("Textures/MenuBackground1");
            backgroundMenuRect = new Rectangle(0, 0, Data.ScreenWid, Data.ScreenHei);
            scene1a = Content.Load<Texture2D>("Textures/Scene1a");
            scene1aRect = new Rectangle(200, 100, Data.ScreenWid-400, Data.ScreenHei-200);
            scene1b = Content.Load<Texture2D>("Textures/Scene1b");
            scene1bRect = new Rectangle(200, 100, Data.ScreenWid - 400, Data.ScreenHei - 200);
            scene2 = Content.Load<Texture2D>("Textures/Scene2");
            scene2Rect = new Rectangle(200, 100, Data.ScreenWid - 400, Data.ScreenHei - 200);
            scene3 = Content.Load<Texture2D>("Textures/Scene3");
            scene3Rect = new Rectangle(200, 100, Data.ScreenWid - 400, Data.ScreenHei - 200);
            scene4a = Content.Load<Texture2D>("Textures/Scene4a");
            scene4aRect = new Rectangle(200, 100, Data.ScreenWid - 400, Data.ScreenHei - 200);
            scene4b = Content.Load<Texture2D>("Textures/Scene4b");
            scene4bRect = new Rectangle(200, 100, Data.ScreenWid - 400, Data.ScreenHei - 200);
        }
        /// <summary>
        /// Aktualizowanie w czasie rzeczywistym timerów oraz obsługa zmian sceny
        /// </summary>
        /// <param name="gameTime"></param>
        internal override void Update(GameTime gameTime)
        {
            timerSecurity = timerSecurity + gameTime.ElapsedGameTime.TotalSeconds;
            sceneTimer = sceneTimer + gameTime.ElapsedGameTime.TotalSeconds;

            //klikanie
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseStateRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            //przyspieszenie przejścia sceny

            if (sceneTimer > 20 || (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(backgroundMenuRect) && timerSecurity > 0.2))
            {
                if (level == 0)
                {
                    Data.CurrentState = Data.Scenes.Game1;
                    timerSecurity = 0;
                    sceneTimer = 0;
                }
                else if (level == 1)
                {
                    Data.CurrentState = Data.Scenes.Game2;
                    timerSecurity = 0;
                    sceneTimer = 0;
                }
                else if (level == 2)
                {
                    Data.CurrentState = Data.Scenes.Game3;
                    timerSecurity = 0;
                    sceneTimer = 0;
                }
                else if (level == 3)
                {
                    level = 0;
                    Data.CurrentState = Data.Scenes.Summary;
                    sceneTimer = 0;
                    timerSecurity = 0;
                }
            }

        }
        /// <summary>
        /// Wyrysowanie grafik fabularnych i tła
        /// </summary>
        /// <param name="spriteBatch"></param>
        internal override void Draw(SpriteBatch spriteBatch)
        {

            //rysowanie tła
            spriteBatch.Draw(backgroundMenu, backgroundMenuRect, Color.White);
            
            if (level == 0)
            {
                if(sceneTimer <= 6)
                {
                    spriteBatch.Draw(scene1a, scene1aRect, Color.White);
                }
                else
                {
                    spriteBatch.Draw(scene1b, scene1bRect, Color.White);
                }
            }
            else if (level == 1)
            {
                spriteBatch.Draw(scene2, scene2Rect, Color.White);
            }
            else if (level == 2)
            {
                spriteBatch.Draw(scene3, scene3Rect, Color.White);
            }
            else if (level == 3)
            {
                if (sceneTimer <= 10)
                {
                    spriteBatch.Draw(scene4a, scene4aRect, Color.White);
                }
                else
                {
                    spriteBatch.Draw(scene4b, scene4bRect, Color.White);
                }
            }
        }
    }
}

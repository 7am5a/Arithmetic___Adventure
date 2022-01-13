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
    class StoryScene : Component
    {
        //zmienna do zabezpieczenia przed zbyt szybkim przyspieszeniem scenki fabularnej
        public static double timerSecurity = 0;

        //zmienna do sterowania wyświetlaniem kontentu
        public static int level = 0;
        //tło
        private Texture2D backgroundMenu;
        private Rectangle backgroundMenuRect;

        //dodanie myszki
        private MouseState mouseState, oldMouseState;
        private Rectangle mouseStateRect;

        internal override void LoadContent(ContentManager Content)
        {
            // załadowanie tego powyzej do gierki
            backgroundMenu = Content.Load<Texture2D>("Textures/MenuBackground1");
            backgroundMenuRect = new Rectangle(0, 0, Data.ScreenWid, Data.ScreenHei);
        }

        internal override void Update(GameTime gameTime)
        {
            timerSecurity = timerSecurity + gameTime.ElapsedGameTime.TotalSeconds;


            //klikanie
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseStateRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            //przyspieszenie przejścia sceny

            if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(backgroundMenuRect) && timerSecurity > 1)
            {
                if (level == 0)
                {
                    Data.CurrentState = Data.Scenes.Game1;
                    timerSecurity = 0;
                }
                else if (level == 1)
                {
                    Data.CurrentState = Data.Scenes.Game2;
                    timerSecurity = 0;
                }
                else if (level == 2)
                {
                    Data.CurrentState = Data.Scenes.Game3;
                    timerSecurity = 0;
                }
                else if (level == 3)
                {
                    Data.CurrentState = Data.Scenes.Summary;
                }
            }

        }

        internal override void Draw(SpriteBatch spriteBatch)
        {

            //rysowanie tła
            spriteBatch.Draw(backgroundMenu, backgroundMenuRect, Color.White);

        }
    }
}

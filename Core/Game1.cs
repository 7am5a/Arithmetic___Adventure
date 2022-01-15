using Arithmetic___Adventure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arithmetic___Adventure.Core
{
    public class Game1 : Game
    {
        //ogarnianie grafik, okna itp
        public static GraphicsDeviceManager graphics;
        //rysuwanie tego co wyzej
        private SpriteBatch spriteBatch;

        //tło
        private Texture2D backgroundMenu;
        private Rectangle backgroundMenuRect;

        //zarządzanie stanem gry
        private GameStateManager gStateManager; 

        //konstruktor
        public Game1()
        {
            //inicjalizacja pola graficznego
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //widoczny zwykły kursor myszy
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // wczytanie muzyka, czcionki, sprite, tutaj wszystko, tylko raz jest robione podczas trwania programu 

            //zmiana wielkości ekranu
            graphics.PreferredBackBufferWidth = Data.ScreenWid;
            graphics.PreferredBackBufferHeight = Data.ScreenHei;
            

            //podaje szerokość i wysokoćć ekrenu  jak oprostokąt?
            //graphics.GraphicsDevice.Viewport.Bounds


            graphics.ApplyChanges();

            //zamknięcie z alt + f4
            Window.AllowAltF4 = true;

            //zmiana nazwy okna
            Window.Title = "Arithmetic Adventure";

            //
            gStateManager = new GameStateManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // załadowanie tego powyzej do gierki
            backgroundMenu = Content.Load<Texture2D>("Textures/MenuBackground1");
            backgroundMenuRect = new Rectangle(0,0,graphics.GraphicsDevice.Viewport.Width,graphics.GraphicsDevice.Viewport.Height);

            gStateManager.LoadContent(Content);

        }

        protected override void Update(GameTime gameTime)
        {

            gStateManager.Update(gameTime);

            if (Data.Exit)
            {
                Exit();
            }

            base.Update(gameTime);
        }

        //rysowanie w okienku
        protected override void Draw(GameTime gameTime)
        {
            //to ważne bo co 1 fps czysci ekran z clearem
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //między tymi dwoma
            spriteBatch.Begin();
            gStateManager.Draw(spriteBatch);
            //spriteBatch.Draw(backgroundMenu,backgroundMenuRect,Color.White);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}

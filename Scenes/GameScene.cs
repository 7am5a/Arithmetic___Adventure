using Arithmetic___Adventure.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        internal override void LoadContent(ContentManager Content)
        {
            // załadowanie grafiki z tabelką do gierki 1280 x 75px
            tabInfo = Content.Load<Texture2D>("Textures/tab_info");
            tabInfoRect = new Rectangle(0, Data.ScreenHei-75, Data.ScreenWid, 75);
        }

        internal override void Update(GameTime gameTime)
        {
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {

            //rysowanie tła
            spriteBatch.Draw(tabInfo, tabInfoRect, Color.White);

        }
                
    }
}

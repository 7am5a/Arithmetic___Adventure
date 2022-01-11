using Arithmetic___Adventure.Core;
using Arithmetic___Adventure.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arithmetic___Adventure.Managers
{
    internal partial class GameStateManager : Component
    {

        private MenuScene ms = new MenuScene();
        private InfoScene ins = new InfoScene();
        private GameScene1 gs1 = new GameScene1();
        private GameScene2 gs2 = new GameScene2();
        private GameScene3 gs3 = new GameScene3();
        private SummaryScene ss = new SummaryScene();
        



        internal override void LoadContent(ContentManager Content)
        {
            ms.LoadContent(Content);
            ins.LoadContent(Content);
            gs1.LoadContent(Content);
            gs2.LoadContent(Content);
            gs3.LoadContent(Content);
            ss.LoadContent(Content);
            

        }

        internal override void Update(GameTime gameTime)
        {
            switch (Data.CurrentState)
            {
                case Data.Scenes.Menu:
                    ms.Update(gameTime);
                    break;
                case Data.Scenes.Game1:
                    gs1.Update(gameTime);
                    break;
                case Data.Scenes.Game2:
                    gs2.Update(gameTime);
                    break;
                case Data.Scenes.Game3:
                    gs3.Update(gameTime);
                    break;
                case Data.Scenes.HowToPlay:
                    ins.Update(gameTime);
                    break;
                case Data.Scenes.Summary:
                    ss.Update(gameTime);
                    break;              
                    
            }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            switch (Data.CurrentState)
            {
                case Data.Scenes.Menu:
                    ms.Draw(spriteBatch);
                    break;
                case Data.Scenes.Game1:
                    gs1.Draw(spriteBatch);
                    break;
                case Data.Scenes.Game2:
                    gs2.Draw(spriteBatch);
                    break;
                case Data.Scenes.Game3:
                    gs3.Draw(spriteBatch);
                    break;
                case Data.Scenes.HowToPlay:
                    ins.Draw(spriteBatch);
                    break;
                case Data.Scenes.Summary:
                    ss.Draw(spriteBatch);
                    break;                
            }

        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arithmetic___Adventure.Core
{
    /// <summary>
    /// Klasa wykorzystywana do generowania cegiełek wraz z przypisanymi im danymi
    /// </summary>
    class Brick : Component
    {
        public Texture2D brickTexture;

        public SpriteFont brickFont;

        public Rectangle brickRect;

        public string equation = "a"; 
        public int equationAnswer = 0;

        public bool isClicked = false;
        public bool exist = true;
        /// <summary>
        /// Ustawienie domyślnych parametrów 
        /// </summary>
        /// <param name="_isClicked">czy dany obiekt był kliknięty</param>
        /// <param name="_exist">czy dany obiekt istnieje</param>
        public Brick(bool _isClicked, bool _exist)
        {
            exist = _exist;
            isClicked = _isClicked;
        }


        internal override void LoadContent(ContentManager Content)
        {            
            brickTexture = Content.Load<Texture2D>("Textures/ceg1");
            brickFont = Content.Load<SpriteFont>("Fonts/TextFont");
            brickRect = new Rectangle(0, 0, 0, 0);
        }

        internal override void Update(GameTime gameTime)
        {

        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arithmetic___Adventure.Core
{
    class Brick : Component
    {
        public Texture2D brickTexture;// { get; set; }

        public SpriteFont brickFont;// { get; set; }

        public Rectangle brickRect;// { get; set; }

        public string equation = "a"; 
        public int equationAnswer = 0;

        public bool isClicked = false;
        public bool exist = true;

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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arithmetic___Adventure.Core
{
   internal abstract class Component
    {
        //abstrakcyjne można inicjalizować, ale nie można mówić co mają robić  

        internal abstract void LoadContent(ContentManager Content);
        internal abstract void Update(GameTime gameTime);
        internal abstract void Draw(SpriteBatch spriteBatch);

        //nic więcej nie trzeba tu robić, a będzie używane dość często i gęsto jak bigos z grochem (beton)
    }
}

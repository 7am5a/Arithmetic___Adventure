using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arithmetic___Adventure.Core
{
    /// <summary>
    /// Klasa ogólna wykorzystywana przy tworzeniu innych klas
    /// </summary>
    internal abstract class Component
    {
        

        internal abstract void LoadContent(ContentManager Content);
        internal abstract void Update(GameTime gameTime);
        internal abstract void Draw(SpriteBatch spriteBatch);

        
    }
}

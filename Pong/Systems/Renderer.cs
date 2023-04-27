using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems
{
    class Renderer : System
    {
        RenderTarget2D _renderTarget;
        private readonly SpriteBatch _spriteBatch;
        private readonly Texture2D _backgroundTexture;

        public Renderer(RenderTarget2D renderTarget, SpriteBatch spriteBatch, Texture2D background) :
            base(typeof(Components.Sprite), typeof(Components.Position))
        {
            _renderTarget = renderTarget;
            _spriteBatch = spriteBatch;
            _backgroundTexture = background;
        }
        public override void Update(GameTime gameTime)
        {
            _spriteBatch.Draw(_backgroundTexture, _renderTarget.Bounds, Color.White);
            // Foreach entity draw them
        }
    }
}

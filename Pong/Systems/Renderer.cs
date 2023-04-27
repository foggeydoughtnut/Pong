using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly Texture2D _squareTexture;


        const int NUMBER_OF_BRICKS_FOR_NET = 40;

        public Renderer(RenderTarget2D renderTarget, SpriteBatch spriteBatch, Dictionary<string, Texture2D> textures) :
            base(typeof(Components.Sprite), typeof(Components.Position))
        {
            _renderTarget = renderTarget;
            _spriteBatch = spriteBatch;
            _backgroundTexture = textures["background"];
            _squareTexture = textures["square"];
        }
        public override void Update(GameTime gameTime)
        {
            _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _renderTarget.Width, _renderTarget.Height), Color.White);
            // Draw center net
            for (int i = 0; i < NUMBER_OF_BRICKS_FOR_NET; i++)
            {
                _spriteBatch.Draw(_squareTexture, new Vector2(_renderTarget.Width/2, (i * _squareTexture.Height) + 8), Color.White);
            }

            // Foreach entity draw them
        }
    }
}

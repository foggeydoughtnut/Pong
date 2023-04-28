using Entities;
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
        private readonly SpriteBatch _spriteBatch;




        public Renderer(SpriteBatch spriteBatch) :
            base(typeof(Components.Sprite), typeof(Components.Transform))
        {
            _spriteBatch = spriteBatch;
        }
        public override void Update(GameTime gameTime)
        {
            // Foreach entity draw them
            foreach (GameObject gameObject in _gameObjects.Values)
            {
                RenderGameObject(gameObject);
            }
        }

        private void RenderGameObject(GameObject gameObject)
        {
            Components.Sprite sprite = gameObject.GetComponent<Components.Sprite>();
            Components.Transform transform = gameObject.GetComponent<Components.Transform>();

            _spriteBatch.Draw(sprite.Texture, new Vector2(transform.Position.X, transform.Position.Y), null, Color.White, 0f, sprite.Origin, 1f, SpriteEffects.None, sprite.RenderDepth);
        }
    }
}

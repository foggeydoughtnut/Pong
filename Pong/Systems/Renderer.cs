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
            base(typeof(Components.Sprite), typeof(Components.Position))
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
            Components.Position position = gameObject.GetComponent<Components.Position>();

            _spriteBatch.Draw(sprite.Texture, new Vector2(position.X, position.Y), Color.White);
        }
    }
}

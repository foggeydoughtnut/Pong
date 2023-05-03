using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems
{
    public class TextRenderer : System
    {
        private readonly SpriteBatch _spriteBatch;
        public TextRenderer(SpriteBatch spriteBatch) : base(typeof(Components.Text), typeof(Components.Transform))
        {
            _spriteBatch = spriteBatch;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in _gameObjects.Values)
            {
                RenderGameObject(gameObject);
            }
        }
        private void RenderGameObject(GameObject gameObject)
        {
            Components.Text text = gameObject.GetComponent<Components.Text>();
            Components.Transform transform = gameObject.GetComponent<Components.Transform>();

            Components.Score score = gameObject.GetComponent<Components.Score>();
            if (score != null )
            {
                text.Message = "" + score.Points;
            }

            _spriteBatch.DrawString(text.Font, text.Message, transform.Position, Color.White, transform.Rotation, text.Origin, transform.Scale, SpriteEffects.None, text.RenderDepth);
        }

    }

}

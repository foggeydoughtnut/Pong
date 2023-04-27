using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class GameModel
    {
        private readonly RenderTarget2D _renderTarget;

        private List<GameObject> _removeThese = new();
        private List<GameObject> _addThese = new();


        private Systems.Renderer _renderer;

        public GameModel(RenderTarget2D renderTarget)
        {
            _renderTarget = renderTarget;
        }

        public void Initialize(ContentManager content, SpriteBatch spriteBatch)
        {
            // TODO create background texture and load it in

            Dictionary<string, Texture2D> textures = new()
            {
                { "background", content.Load<Texture2D>("Sprites/background") },
                { "square", content.Load<Texture2D>("Sprites/Square") }
            };


            _renderer = new Systems.Renderer(_renderTarget, spriteBatch, textures);

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            _renderer.Update(gameTime);
        }

        private void AddGameObject(GameObject gameObject)
        {

        }

        private void RemoveGameObject(GameObject gameObject)
        {

        }

        private void InitializePlayerOne(Texture2D playerTexture)
        {

        }

        private void InitializeBall(Texture2D ballTexture)
        {

        }
    }
}

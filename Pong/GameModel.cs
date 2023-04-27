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

        private List<GameObject> _removeThese = new();
        private List<GameObject> _addThese = new();

        private Systems.Renderer _renderer;

        public GameModel()
        {
        }

        public void Initialize(ContentManager content, SpriteBatch spriteBatch)
        {
            Dictionary<string, Texture2D> textures = new()
            {
                { "background", content.Load<Texture2D>("Sprites/Background") },
            };


            _renderer = new Systems.Renderer(spriteBatch);
            InitializeBackground(textures["background"]);
        }

        public void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in _removeThese)
            {
                RemoveGameObject(gameObject);
            }
            _removeThese.Clear();

            foreach (GameObject gameObject in _addThese)
            {
                AddGameObject(gameObject);
            }
            _addThese.Clear();
        }

        public void Draw(GameTime gameTime)
        {
            _renderer.Update(gameTime);
        }

        private void AddGameObject(GameObject gameObject)
        {
            _renderer.Add(gameObject);
        }

        private void RemoveGameObject(GameObject gameObject)
        {
            _renderer.Remove(gameObject.Id);
        }

        private void InitializeBackground(Texture2D backgroundTexture)
        {
            GameObject background = new();
            background.Add(new Components.Sprite(backgroundTexture));
            background.Add(new Components.Position(0, 0));
            AddGameObject(background);
        }

        private void InitializePlayerOne(Texture2D playerTexture)
        {

        }

        private void InitializeBall(Texture2D ballTexture)
        {

        }
    }
}

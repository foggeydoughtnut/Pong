using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class GameModel
    {
        RenderTarget2D _renderTarget;

        private List<GameObject> _removeThese = new();
        private List<GameObject> _addThese = new();

        private Systems.Renderer _renderer;
        private Systems.InputSystem _inputSystem;
        private Systems.MovementSystem _movementSystem;
        private Systems.PhysicsSystem _physicsSystem;

        public GameModel(RenderTarget2D renderTarget)
        {
            _renderTarget = renderTarget;
        }

        public void Initialize(ContentManager content, SpriteBatch spriteBatch)
        {
            Dictionary<string, Texture2D> textures = new()
            {
                { "background", content.Load<Texture2D>("Sprites/Background") },
                { "player", content.Load<Texture2D>("Sprites/Player") },
                { "roof", content.Load<Texture2D>("Sprites/Roof") },
                { "floor", content.Load<Texture2D>("Sprites/floor") },

            };


            _renderer = new(spriteBatch);
            _inputSystem = new();
            _movementSystem = new();
            _physicsSystem = new();


            InitializeBackground(textures["background"]);
            InitializeRoof(textures["roof"]);
            InitializeFloor(textures["floor"]);

            InitializePlayerTwo(textures["player"]);
            InitializePlayerOne(textures["player"]);

        }

        public void Update(GameTime gameTime)
        {
            _inputSystem.Update(gameTime);
            _movementSystem.Update(gameTime);
            _physicsSystem.Update(gameTime);

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
            _inputSystem.Add(gameObject);
            _movementSystem.Add(gameObject);
            _physicsSystem.Add(gameObject);
        }

        private void RemoveGameObject(GameObject gameObject)
        {
            _renderer.Remove(gameObject.Id);
            _inputSystem.Remove(gameObject.Id);
            _movementSystem.Remove(gameObject.Id);
            _physicsSystem.Remove(gameObject.Id);
        }

        private void InitializeBackground(Texture2D backgroundTexture)
        {
            GameObject background = new();
            background.Add(new Components.Sprite(backgroundTexture, Vector2.Zero));
            background.Add(new Components.Transform(0, 0));
            AddGameObject(background);
        }

        private void InitializeRoof(Texture2D roofTexture)
        {
            GameObject roof = new();
            roof.Add(new Components.Sprite(roofTexture, Vector2.Zero));
            roof.Add(new Components.Transform(0, -6));
            AddGameObject(roof);
        }
        private void InitializeFloor(Texture2D floorTexture)
        {
            GameObject floor = new();
            floor.Add(new Components.Sprite(floorTexture, Vector2.Zero));
            floor.Add(new Components.Transform(0, _renderTarget.Height-10));
            AddGameObject(floor);
        }

        private void InitializePlayerOne(Texture2D playerTexture)
        {
            // TODO read this from a json file
            Dictionary<string, Keys> controls = new()
            {
                {"up", Keys.W },
                {"down", Keys.S },
            };
            
            GameObject player1 = Player.Create(playerTexture, _renderTarget.Width / 8, _renderTarget.Height / 2, controls, 1);
            AddGameObject(player1);
        }

        private void InitializePlayerTwo(Texture2D playerTexture)
        {
            // TODO read this from a json file
            Dictionary<string, Keys> controls = new()
            {
                {"up", Keys.Up },
                {"down", Keys.Down },
            };

            GameObject player2 = Player.Create(playerTexture, 7 * _renderTarget.Width / 8, _renderTarget.Height / 2, controls, 2);
            AddGameObject(player2);
        }

        private void InitializeBall(Texture2D ballTexture)
        {

        }
    }
}

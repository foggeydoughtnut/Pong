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
        private Systems.TextRenderer _textRenderer;
        private Systems.InputSystem _inputSystem;
        private Systems.MovementSystem _movementSystem;
        private Systems.PhysicsSystem _physicsSystem;
        private Systems.TimerSystem _timerSystem;

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
                { "ball", content.Load<Texture2D>("Sprites/Ball") }
            };

            Dictionary<string, SpriteFont> fonts = new()
            {
                {"scoreFont", content.Load<SpriteFont>("Fonts/ScoreFont") },
            };



            _renderer = new(spriteBatch);
            _textRenderer = new(spriteBatch);
            _inputSystem = new();
            _movementSystem = new();
            _timerSystem = new((gameObject) =>
            { // This is the callback that happens when the timer ends to remove the timer
                _removeThese.Add(gameObject);
            });
            _physicsSystem = new((gameObject, direction) =>
            {
                // Remove the existing ball
                _removeThese.Add(gameObject);
                // Add new ball
                InitializeTimerEvent(2f, () =>
                {
                    _addThese.Add(Ball.Create(textures["ball"], _renderTarget.Width / 2, _renderTarget.Height / 2, direction));
                });
            });


            InitializeBackground(textures["background"]);
            InitializeRoof(textures["roof"]);
            InitializeFloor(textures["floor"]);

            InitializePlayerTwo(textures["player"], fonts["scoreFont"]);
            InitializePlayerOne(textures["player"], fonts["scoreFont"]);

            InitializeTimerEvent(2f, () =>
            {
                _addThese.Add(Ball.Create(textures["ball"], _renderTarget.Width / 2, _renderTarget.Height / 2, 1));
            });

            InitializeGoalOne();
            InitializeGoalTwo();

        }

        public void Update(GameTime gameTime)
        {
            _inputSystem.Update(gameTime);
            _movementSystem.Update(gameTime);
            _physicsSystem.Update(gameTime);
            _timerSystem.Update(gameTime);

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
            _textRenderer.Update(gameTime);
        }

        private void AddGameObject(GameObject gameObject)
        {
            _renderer.Add(gameObject);
            _textRenderer.Add(gameObject);
            _inputSystem.Add(gameObject);
            _movementSystem.Add(gameObject);
            _physicsSystem.Add(gameObject);
            _timerSystem.Add(gameObject);
        }

        private void RemoveGameObject(GameObject gameObject)
        {
            _renderer.Remove(gameObject.Id);
            _textRenderer.Remove(gameObject.Id);
            _inputSystem.Remove(gameObject.Id);
            _movementSystem.Remove(gameObject.Id);
            _physicsSystem.Remove(gameObject.Id);
            _timerSystem.Remove(gameObject.Id);
        }

        #region Initializing GameObjects
        private void InitializeBackground(Texture2D backgroundTexture)
        {
            GameObject background = new();
            background.Add(new Components.Sprite(backgroundTexture, Vector2.Zero));
            background.Add(new Components.Transform(0, 0));
            AddGameObject(background);
        }

        private void InitializeRoof(Texture2D roofTexture)
        {
            GameObject roof = new("border");
            roof.Add(new Components.Sprite(roofTexture, Vector2.Zero));
            roof.Add(new Components.Transform(0, -6));
            roof.Add(new Components.Rigidbody(Vector2.Zero, 0f));
            roof.Add(new Components.BoxCollider(new Vector2(0, -6), new Vector2(roofTexture.Width, roofTexture.Height)));
            AddGameObject(roof);
        }
        private void InitializeFloor(Texture2D floorTexture)
        {
            GameObject floor = new("border");
            floor.Add(new Components.Sprite(floorTexture, Vector2.Zero));
            floor.Add(new Components.Transform(0, _renderTarget.Height-10));
            floor.Add(new Components.Rigidbody(Vector2.Zero, 0f));
            floor.Add(new Components.BoxCollider(new Vector2(0, _renderTarget.Height - 10), new Vector2(floorTexture.Width, floorTexture.Height)));

            AddGameObject(floor);
        }

        private void InitializePlayerOne(Texture2D playerTexture, SpriteFont scoreFont)
        {
            // TODO read this from a json file
            Dictionary<string, Keys> controls = new()
            {
                {"up", Keys.W },
                {"down", Keys.S },
            };

            Tuple<GameObject, GameObject> objects = Player.Create(playerTexture, _renderTarget.Width / 8, _renderTarget.Height / 2, controls, 1, scoreFont, _renderTarget.Width/4, _renderTarget.Height/8);
            AddGameObject(objects.Item1);
            AddGameObject(objects.Item2);
        }

        private void InitializePlayerTwo(Texture2D playerTexture, SpriteFont scoreFont)
        {
            // TODO read this from a json file
            Dictionary<string, Keys> controls = new()
            {
                {"up", Keys.Up },
                {"down", Keys.Down },
            };

            Tuple<GameObject, GameObject> objects = Player.Create(playerTexture, 7 * _renderTarget.Width / 8, _renderTarget.Height / 2, controls, 2, scoreFont, 3*_renderTarget.Width/4, _renderTarget.Height/8);
            AddGameObject(objects.Item1);
            AddGameObject(objects.Item2);

        }

        private void InitializeBall(Texture2D ballTexture)
        {
            GameObject ball = Ball.Create(ballTexture, _renderTarget.Width / 2, _renderTarget.Height / 2, 1);
            AddGameObject(ball);
        }

        private void InitializeGoalOne()
        {
            GameObject goal = Goal.Create(7 * _renderTarget.Width / 8 + 24, 0, _renderTarget.Width / 8, _renderTarget.Height, 1);
            AddGameObject(goal);
        }

        private void InitializeGoalTwo()
        {
            GameObject goal = Goal.Create(-24, 0, _renderTarget.Width / 8, _renderTarget.Height, 2);
            AddGameObject(goal);
        }

        private void InitializeTimerEvent(float delay, Action onTimerEnd)
        {
            GameObject timerObject = new();
            timerObject.Add(new Components.Timer(0f, delay, onTimerEnd));
            AddGameObject(timerObject);
        }
        #endregion
    }
}

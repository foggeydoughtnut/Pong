using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Views;
using System.Collections.Generic;

namespace Pong
{
    public class Pong : Game
    {
        private GraphicsDeviceManager _graphics;
        private IGameState _currentState;
        private GameStateEnum _nextStateEnum = GameStateEnum.MainMenu;
        private Dictionary<GameStateEnum, IGameState> _states;

        public Pong()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            Window.AllowUserResizing = true;

            _states = new()
            {
                { GameStateEnum.MainMenu, new MainMenuView() },
                { GameStateEnum.GamePlay, new GamePlayView() },
                { GameStateEnum.Controls, new ControlsView() }
            };

            _currentState = _states[GameStateEnum.MainMenu];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach (KeyValuePair<GameStateEnum, IGameState> item in _states)
            {
                item.Value.Initialize(GraphicsDevice, _graphics, Window);
                item.Value.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            _nextStateEnum = _currentState.ProcessInput(gameTime);
            if (_nextStateEnum == GameStateEnum.Exit)
            {
                Exit();
            }
            _currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Render(gameTime);
            if (_currentState != _states[_nextStateEnum])
            {
                _currentState = _states[_nextStateEnum];
                _currentState.InitializeSession();
            }

            base.Draw(gameTime);
        }
    }
}
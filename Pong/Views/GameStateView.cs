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
    public abstract class GameStateView : IGameState
    {
        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;
        protected GameWindow _window;

        public virtual void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameWindow window)
        {
            _graphics = graphics;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _window = window;
        }

        public virtual void InitializeSession()
        {
        }

        public abstract void LoadContent(ContentManager contentManager);

        public abstract GameStateEnum ProcessInput(GameTime gameTime);

        public abstract void Render(GameTime gameTime);

        public abstract void Update(GameTime gameTime);
    }
}

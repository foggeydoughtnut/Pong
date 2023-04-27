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
    public interface IGameState
    {
        void InitializeSession();
        void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameWindow window);
        void LoadContent(ContentManager contentManager);
        GameStateEnum ProcessInput(GameTime gameTime);
        void Update(GameTime gameTime);
        void Render(GameTime gameTime);

    }
}

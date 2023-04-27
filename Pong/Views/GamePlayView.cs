using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Views
{
    public class GamePlayView : GameStateView
    {
        private SpriteFont _font;
        private const string MESSAGE = "Gameplay";
        KeyboardState previousKeyboardState;
        RenderTarget2D renderTarget;

        ContentManager _content;
        private GameModel _gameModel;


        public override void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameWindow window)
        {
            base.Initialize(graphicsDevice, graphics, window);
            renderTarget = new RenderTarget2D(
                _graphics.GraphicsDevice,
                360,
                270,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                _graphics.GraphicsDevice.PresentationParameters.MultiSampleCount,
                RenderTargetUsage.DiscardContents
            );
        }

        public override void InitializeSession()
        {
            _gameModel = new GameModel(renderTarget);
            _gameModel.Initialize(_content, _spriteBatch);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            _font = contentManager.Load<SpriteFont>("Fonts/MenuText");
            _content = contentManager;
        }

        public override GameStateEnum ProcessInput(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyUp(Keys.Escape) && previousKeyboardState.IsKeyDown(Keys.Escape))
            {
                previousKeyboardState = currentKeyboardState;
                return GameStateEnum.MainMenu;
            }
            previousKeyboardState = currentKeyboardState;
            return GameStateEnum.GamePlay;
        }

        public override void Render(GameTime gameTime)
        {
            _graphics.GraphicsDevice.SetRenderTarget(renderTarget);
            _graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            _gameModel.Draw(gameTime);

            _spriteBatch.End();
            _graphics.GraphicsDevice.SetRenderTarget(null);

            // Render render target to screen
            _spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(
                    renderTarget,
                    new Rectangle(_window.ClientBounds.Width / 8, 0, 4 * _window.ClientBounds.Height / 3, _window.ClientBounds.Height),
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    1f
                );
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            _gameModel.Update(gameTime);
        }
    }
}

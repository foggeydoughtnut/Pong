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
    public class MainMenuView : GameStateView
    {
        private SpriteFont _fontMenu;
        KeyboardState previousKeyboardState;
        RenderTarget2D renderTarget;


        private enum MenuState
        {
            NewGame,
            Controls,
            Quit
        }

        public override void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameWindow window)
        {
            base.Initialize(graphicsDevice, graphics, window);
            renderTarget = new RenderTarget2D(
                _graphics.GraphicsDevice,
                640,
                480,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                _graphics.GraphicsDevice.PresentationParameters.MultiSampleCount,
                RenderTargetUsage.DiscardContents
            );
        }

        private MenuState _currentSelection = MenuState.NewGame;

        public override void LoadContent(ContentManager contentManager)
        {
            _fontMenu = contentManager.Load<SpriteFont>("Fonts/MenuText");
        }

        public override GameStateEnum ProcessInput(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyUp(Keys.Up) && previousKeyboardState.IsKeyDown(Keys.Up) && _currentSelection - 1 >= 0)
                _currentSelection -= 1;

            if (currentKeyboardState.IsKeyUp(Keys.Down) && previousKeyboardState.IsKeyDown(Keys.Down) && _currentSelection + 1 <= MenuState.Quit)
                _currentSelection += 1;


            if (currentKeyboardState.IsKeyUp(Keys.Enter) && previousKeyboardState.IsKeyDown(Keys.Enter) && _currentSelection == MenuState.NewGame)
            {
                previousKeyboardState = currentKeyboardState;
                return GameStateEnum.GamePlay;
            }
            if (currentKeyboardState.IsKeyUp(Keys.Enter) && previousKeyboardState.IsKeyDown(Keys.Enter) && _currentSelection == MenuState.Controls)
            {
                previousKeyboardState = currentKeyboardState;
                return GameStateEnum.Controls;
            }
            if (currentKeyboardState.IsKeyUp(Keys.Enter) && previousKeyboardState.IsKeyDown(Keys.Enter) && _currentSelection == MenuState.Quit)
            {
                previousKeyboardState = currentKeyboardState;
                return GameStateEnum.Exit;
            }
            previousKeyboardState = currentKeyboardState;
            return GameStateEnum.MainMenu;

        }
        public override void Update(GameTime gameTime){}

        public override void Render(GameTime gameTime)
        {
            _graphics.GraphicsDevice.SetRenderTarget(renderTarget);
            _graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            // I split the first one's parameters on separate lines to help you see them better
            float bottom = DrawMenuItem(
                _fontMenu,
                "New Game",
                200,
               _currentSelection == MenuState.NewGame ? Color.Yellow : Color.Blue);
            bottom = DrawMenuItem(_fontMenu, "Controls", bottom, _currentSelection == MenuState.Controls ? Color.Yellow : Color.Blue);
            DrawMenuItem(_fontMenu, "Quit", bottom, _currentSelection == MenuState.Quit ? Color.Yellow : Color.Blue);

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

        private float DrawMenuItem(SpriteFont font, string text, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);
            _spriteBatch.DrawString(
                font,
                text,
                new Vector2(renderTarget.Width / 2 - stringSize.X / 2, y),
                color);

            return y + stringSize.Y + stringSize.Y/2;
        }

    }
}

﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public class ControlsView : GameStateView
    {
        private SpriteFont _font;
        private const string MESSAGE = "Controls";
        KeyboardState previousKeyboardState;
        RenderTarget2D renderTarget;


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

        public override void LoadContent(ContentManager contentManager)
        {
            _font = contentManager.Load<SpriteFont>("Fonts/MenuText");
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
            return GameStateEnum.Controls;
        }

        public override void Render(GameTime gameTime)
        {
            _graphics.GraphicsDevice.SetRenderTarget(renderTarget);
            _graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            Vector2 stringSize = _font.MeasureString(MESSAGE);
            _spriteBatch.DrawString(_font, MESSAGE,
                new Vector2(renderTarget.Width / 2 - stringSize.X / 2, renderTarget.Height / 2 - stringSize.Y), Color.Yellow);

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
        }
    }
}

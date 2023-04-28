using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Systems
{
    class InputSystem : System
    {
        public InputSystem() 
            : base(typeof(Components.Input))
        { }

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in _gameObjects.Values)
            {
                if (gameObject.ContainsComponent<KeyboardInput>())
                {
                    KeyboardInput keyboardInput = gameObject.GetComponent<KeyboardInput>();
                    KeyboardState keyboardState = Keyboard.GetState();
                    keyboardInput.previousActions = new(keyboardInput.actions);

                    foreach(string key in keyboardInput.actionKeyPairs.Keys)
                    {
                        if (!keyboardInput.actions.ContainsKey(key))
                        {
                            keyboardInput.actions.Add(key, false);
                            keyboardInput.previousActions.Add(key, false);
                        }
                        keyboardInput.actions[key] = keyboardState.IsKeyDown(keyboardInput.actionKeyPairs[key]);
                    }

                }

            }
        }
    }
}

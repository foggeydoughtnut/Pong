using Components;
using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Systems
{
    public class MovementSystem : System
    {
        float xDirection;
        float yDirection;
        float regularizerX;
        float regularizerY;

        public MovementSystem() : base(typeof(Components.Input), typeof(Components.Rigidbody), typeof(Components.Transform)) 
        {
            xDirection = 0f;
            yDirection = 0f;
            regularizerX = 1f;
            regularizerY = 1f;
        }

        public void OnMoveUp(float direction)
        {
            yDirection += -direction;
        }

        public void OnMoveDown(float direction)
        {
            yDirection += direction;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in _gameObjects.Values)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                KeyboardInput input = gameObject.ContainsComponent<KeyboardInput>() ? gameObject.GetComponent<KeyboardInput>() : null;

                if (input != null)
                {
                    foreach (string key in input.actions.Keys)
                    {
                        if (input.actions[key] != input.previousActions[key])
                        {
                            Type thisType = GetType();
                            MethodInfo theMethod = thisType.GetMethod($"On{key}");

                            if (theMethod != null)
                            {
                                if (!input.actions[key])
                                {
                                    object[] parameters = { -1f };
                                    theMethod.Invoke(this, parameters);
                                }
                                else
                                {
                                    object[] parameters = { 1f };
                                    theMethod.Invoke(this, parameters);
                                }
                            }
                        }
                    }
                }

                if (xDirection != 0f && yDirection != 0f)
                {
                    regularizerX = 0.7f;
                    regularizerY = 0.7f;
                }
                else
                {
                    regularizerX = 1f;
                    regularizerY = 1f;
                }

                rb.Direction = new Vector2(Math.Clamp(regularizerX * xDirection, -regularizerX, regularizerX), Math.Clamp(regularizerY * yDirection, -regularizerY, regularizerY));
               
            }




        }
    }
}

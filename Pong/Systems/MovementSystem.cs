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
        float xDirection1;
        float yDirection1;
        float regularizerX1;
        float regularizerY1;

        float xDirection2;
        float yDirection2;
        float regularizerX2;
        float regularizerY2;

        public MovementSystem() : base(typeof(Components.Input), typeof(Components.Rigidbody), typeof(Components.Transform)) 
        {
            xDirection1 = 0f;
            yDirection1 = 0f;
            regularizerX1 = 1f;
            regularizerY1 = 1f;

            xDirection2 = 0f;
            yDirection2 = 0f;
            regularizerX2 = 1f;
            regularizerY2 = 1f;
        }

        public void OnMoveUp1(float direction)
        {
            yDirection1 += -direction;
        }

        public void OnMoveDown1(float direction)
        {
            yDirection1 += direction;
        }

        public void OnMoveUp2(float direction)
        {
            yDirection2 += -direction;
        }

        public void OnMoveDown2(float direction)
        {
            yDirection2 += direction;
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _gameObjects.Count; i++) 
            {
                KeyValuePair<uint, GameObject> entry = _gameObjects.ElementAt(i);
                GameObject gameObject = entry.Value;
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
                                    object[] parameters = { -1f};
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

                SetRegularizer(i+1);
                SetRegularizer(i+1);
                
                if (i+1 == 1)
                {
                    rb.Direction = new Vector2(Math.Clamp(regularizerX1 * xDirection1, -regularizerX1, regularizerX1), Math.Clamp(regularizerY1 * yDirection1, -regularizerY1, regularizerY1));
                }
                if (i+1 == 2)
                {
                    rb.Direction = new Vector2(Math.Clamp(regularizerX2 * xDirection2, -regularizerX2, regularizerX2), Math.Clamp(regularizerY2 * yDirection2, -regularizerY2, regularizerY2));
                }
            }




        }

        private void SetRegularizer(int playerNumber)
        {
            if (playerNumber == 1)
            {
                if (xDirection1 != 0f && yDirection1 != 0f)
                {
                    regularizerX1 = 0.7f;
                    regularizerY1 = 0.7f;
                }
                else
                {
                    regularizerX1 = 1f;
                    regularizerY1 = 1f;
                }
            }
            if (playerNumber == 2)
            {
                if (xDirection2 != 0f && yDirection2 != 0f)
                {
                    regularizerX2 = 0.7f;
                    regularizerY2 = 0.7f;
                }
                else
                {
                    regularizerX2 = 1f;
                    regularizerY2 = 1f;
                }
            }
        }
    }
}

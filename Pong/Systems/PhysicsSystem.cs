using Components;
using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems
{
    public class PhysicsSystem : System
    {
        public PhysicsSystem() : base(typeof(Transform), typeof(Rigidbody))
        {
        }

        public override void Update(GameTime gameTime)
        {
            // Update the rigid body's position

            foreach (GameObject gameObject in _gameObjects.Values)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();

                Transform transform = gameObject.GetComponent<Transform>();

                transform.PreviousPosition = transform.Position;


                transform.Position.X += rb.Direction.X * rb.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                transform.Position.Y += rb.Direction.Y * rb.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;


            }
        }
    }
}

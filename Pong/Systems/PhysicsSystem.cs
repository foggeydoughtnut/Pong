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
        public PhysicsSystem() : base(typeof(Transform), typeof(Rigidbody), typeof(Collider))
        {
        }

        public override void Update(GameTime gameTime)
        {
            // Update the rigid body's position
            List<GameObject> movableObjects = FindMovable();

            foreach (GameObject gameObject in _gameObjects.Values)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();

                Transform transform = gameObject.GetComponent<Transform>();

                transform.PreviousPosition = transform.Position;

                float amountMovedX = rb.Direction.X * rb.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                float amountMovedY = rb.Direction.Y * rb.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                transform.Position.X += amountMovedX;
                transform.Position.Y += amountMovedY;



                BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();

                if (boxCollider != null && rb.Speed != 0f)
                {
                    boxCollider.Collider = new(new Point((int)transform.Position.X - boxCollider.Collider.Width / 2, (int)transform.Position.Y - boxCollider.Collider.Height / 2), boxCollider.Collider.Size);
                }



                // Check for collisions
                foreach (GameObject movableObject in movableObjects)
                {
                    if (Collides(gameObject, movableObject))
                    {
                        if (movableObject.Name.Contains("Player"))
                        {
                            movableObject.GetComponent<Rigidbody>().Direction = Vector2.Zero;
                            movableObject.GetComponent<Transform>().Position = movableObject.GetComponent<Transform>().PreviousPosition;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets all game objects that have a rigidbody on it
        /// </summary>
        /// <returns></returns>
        private List<GameObject> FindMovable()
        {
            List<GameObject> movableObjects = new();

            foreach(GameObject gameObject in _gameObjects.Values)
            {
                if (gameObject.ContainsComponent<Components.Rigidbody>() && gameObject.ContainsComponent<Components.Transform>() && gameObject.GetComponent<Rigidbody>().Speed > 0f)
                {
                    movableObjects.Add(gameObject);
                }
            }
            return movableObjects;
        }

        private bool Collides(GameObject a, GameObject b)
        {
            Components.BoxCollider aCollider = a.GetComponent<Components.BoxCollider>();
            Components.BoxCollider bCollider = b.GetComponent<Components.BoxCollider>();

            if (aCollider == bCollider)
            {
                return false;
            }
            return aCollider.Collider.Intersects(bCollider.Collider);
        }
    }
}

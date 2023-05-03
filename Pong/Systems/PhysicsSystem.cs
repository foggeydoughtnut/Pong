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


                if (!rb.CanMoveDown && rb.Direction.Y < 0) // Couldn't move down but they moved up
                {
                    rb.CanMoveDown = true;
                }
                else if (!rb.CanMoveUp && rb.Direction.Y > 0) // Couldn't move up but they moved down
                {
                    rb.CanMoveUp = true;
                }


                transform.Position.X += amountMovedX;

                if (rb.CanMoveDown && rb.Direction.Y > 0 || rb.CanMoveUp && rb.Direction.Y < 0 || rb.CanMoveUp && rb.CanMoveDown)
                {
                    transform.Position.Y += amountMovedY;
                }


                BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();

                if (boxCollider != null && rb.Speed != 0f)
                {
                    boxCollider.Collider = new(new Vector2(transform.Position.X - boxCollider.Collider.Width / 2, transform.Position.Y - boxCollider.Collider.Height / 2), boxCollider.Collider.Size);
                }



                // Check for collisions
                foreach (GameObject movableObject in movableObjects)
                {
                    if (Collides(gameObject, movableObject))
                    {
                        if (movableObject.Name.Contains("Player"))
                        {
                            BoxCollider movableObjectCollider = movableObject.GetComponent<BoxCollider>();
                            BoxCollider gameObjectCollider = gameObject.GetComponent<BoxCollider>();

                            if (movableObjectCollider.Collider.Top < gameObjectCollider.Collider.Bottom && movableObjectCollider.Collider.Bottom > gameObjectCollider.Collider.Bottom)
                            { // Hit bottom of roof
                                movableObject.GetComponent<Rigidbody>().CanMoveUp = false;
                                movableObject.GetComponent<Rigidbody>().Direction = Vector2.Zero;
                                movableObject.GetComponent<Transform>().Position = new Vector2(movableObject.GetComponent<Transform>().Position.X, gameObject.GetComponent<Transform>().Position.Y + movableObject.GetComponent<BoxCollider>().Collider.Height);
                            }
                            else if (movableObjectCollider.Collider.Bottom > gameObjectCollider.Collider.Top && movableObjectCollider.Collider.Top < gameObjectCollider.Collider.Top)
                            { // Hit top of floor
                                movableObject.GetComponent<Rigidbody>().CanMoveDown = false;
                                movableObject.GetComponent<Rigidbody>().Direction = Vector2.Zero;
                                movableObject.GetComponent<Transform>().Position = new Vector2(movableObject.GetComponent<Transform>().Position.X, gameObject.GetComponent<Transform>().Position.Y - gameObject.GetComponent<BoxCollider>().Collider.Height);
                            }



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

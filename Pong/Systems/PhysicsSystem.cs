﻿using Components;
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
                        if (movableObject.Name == "Ball")
                        {
                            if (gameObject.Name.Contains("Player"))
                            {
                                // TODO check the direction vector of player and ball and calculate the projection vector based on those
                                float yDirection = CalculateBallBounceYDirection(gameObject.GetComponent<Rigidbody>().Direction, movableObject.GetComponent<Rigidbody>().Direction, gameObject.GetComponent<Transform>().Position, movableObject.GetComponent<Transform>().Position, gameObject.GetComponent<BoxCollider>().Collider.Height/2);

                                Vector2 direction = new(-movableObject.GetComponent<Rigidbody>().Direction.X, yDirection);
                                direction.Normalize();

                                movableObject.GetComponent<Rigidbody>().Direction = direction;

                                if (gameObject.Name == "Player1") // Player 1 is on the left so move the ball to the right
                                    movableObject.GetComponent<Transform>().Position = new Vector2(gameObject.GetComponent<Transform>().Position.X + gameObject.GetComponent<BoxCollider>().Collider.Width, movableObject.GetComponent<Transform>().Position.Y);
                                else // Player 2 is on the right so move the ball to the left
                                    movableObject.GetComponent<Transform>().Position = new Vector2(gameObject.GetComponent<Transform>().Position.X - gameObject.GetComponent<BoxCollider>().Collider.Width, movableObject.GetComponent<Transform>().Position.Y);
                            }

                            if (gameObject.Name == "border")
                            {
                                movableObject.GetComponent<Rigidbody>().Direction.Y = -movableObject.GetComponent<Rigidbody>().Direction.Y;
                            }
                        }

                        if (movableObject.Name.Contains("Player") && gameObject.Name != "Ball")
                        {
                            BoxCollider playerCollider = movableObject.GetComponent<BoxCollider>();
                            BoxCollider gameObjectCollider = gameObject.GetComponent<BoxCollider>();

                            if (playerCollider.Collider.Top < gameObjectCollider.Collider.Bottom && playerCollider.Collider.Bottom > gameObjectCollider.Collider.Bottom)
                            { // Hit bottom of roof
                                movableObject.GetComponent<Rigidbody>().CanMoveUp = false;
                                movableObject.GetComponent<Rigidbody>().Direction = Vector2.Zero;
                                movableObject.GetComponent<Transform>().Position = new Vector2(movableObject.GetComponent<Transform>().Position.X, gameObject.GetComponent<Transform>().Position.Y + movableObject.GetComponent<BoxCollider>().Collider.Height);
                            }
                            else if (playerCollider.Collider.Bottom > gameObjectCollider.Collider.Top && playerCollider.Collider.Top < gameObjectCollider.Collider.Top)
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
        /// Calculates the Y value of the bounce for the ball when it hits a player's paddle
        /// </summary>
        /// <param name="playerDirection"></param>
        /// <param name="ballDirection"></param>
        /// <returns>A y value between -1 and 1</returns>
        private float CalculateBallBounceYDirection(Vector2 playerDirection, Vector2 ballDirection, Vector2 playerCenter, Vector2 hitPosition, float halfPaddleLength)
        {
            // Find distance away from center
            float length = MathF.Sqrt(MathF.Pow(hitPosition.Y - playerCenter.Y, 2));
            float percentageOfHalfPaddle = length / halfPaddleLength;
            float yDirection = 1 - percentageOfHalfPaddle;

            // If the ball is coming from the top then bounce back up if it hits above the half
            if (ballDirection.Y > 0 && hitPosition.Y < playerCenter.Y)
            {
                yDirection *= -1;
            }
            // else bounce down

            // If the ball is coming from the bottom then bounce back down if it hits below the half
            else if (ballDirection.Y < 0 && hitPosition.Y > playerCenter.Y) 
            {
                yDirection *= -1;
            }
            // else bounce up


            return yDirection;
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

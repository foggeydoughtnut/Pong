using Microsoft.Xna.Framework;

namespace Components
{
    public class Rigidbody : Component
    {
        public Vector2 Direction;
        public float Speed;

        public Rigidbody(Vector2 direction, float speed) 
        {
            Direction = direction;
            Speed = speed;
        }
    }
}

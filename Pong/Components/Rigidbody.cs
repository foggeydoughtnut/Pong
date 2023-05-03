using Microsoft.Xna.Framework;

namespace Components
{
    public class Rigidbody : Component
    {
        public Vector2 Direction;
        public float Speed;

        public bool CanMoveUp;
        public bool CanMoveDown;
        public Rigidbody(Vector2 direction, float speed) 
        {
            Direction = direction;
            Speed = speed;

            if (speed > 0)
            {
                CanMoveUp = true;
                CanMoveDown = true;
            }
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Components
{
    public class Transform : Component
    {
        public Vector2 PreviousPosition = new();
        public Vector2 Position = new();
        public float Rotation;
        public float Scale;




        public Transform(int x, int y)
        {
            Position = new Vector2(x, y);
            Rotation = 0f;
            Scale = 1f;
        }
        public Transform(int x, int y, float rotation, float scale)
        {
            Position = new Vector2(x, y);
            Rotation = rotation;
            Scale = scale;
            PreviousPosition = Position;
        }

        public override string ToString()
        {
            return $"X: {Position.X} Y: {Position.Y}";
        }
    }
}

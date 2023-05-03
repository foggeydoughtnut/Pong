using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class MyRectangle
    {
        public Vector2 Position; // Top left corner of rectangle
        public Vector2 Size; // Width and height of rectangle

        public float Width;
        public float Height;

        public float X;
        public float Y;

        public float Left;
        public float Right;
        public float Top;
        public float Bottom;

        public MyRectangle(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;

            X = position.X;
            Y = position.Y;

            Left = position.X;
            Right = position.X + size.X;
            Top = position.Y;
            Bottom = position.Y + size.Y;

            Width = size.X;
            Height = size.Y;
        }

        public bool Intersects(MyRectangle other)
        {
            if (other.Left < Right && Left < other.Right && other.Top < Bottom)
            {
                return Top < other.Bottom;
            }

            return false;
        }

    }
}

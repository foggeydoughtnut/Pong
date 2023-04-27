using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    public class Position : Component
    {

        public Point position = new();
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            position = new Point(x, y);
        }
    }
}

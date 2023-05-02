using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    public class BoxCollider : Collider
    {
        public Rectangle Collider;

        public BoxCollider(Point location, Point size) 
        {
            Collider = new(location, size);
        }

        public BoxCollider(int x, int y, int width, int height) 
        {
            Collider = new(x, y, width, height); 
        }

        public BoxCollider(Rectangle collider) 
        {
            Collider = collider;
        }
    }
}

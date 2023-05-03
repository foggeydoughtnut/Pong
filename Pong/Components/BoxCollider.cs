using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Components
{
    public class BoxCollider : Collider
    {
        public MyRectangle Collider;

        public BoxCollider(Vector2 location, Vector2 size) 
        {
            Collider = new(location, size);
        }

        public BoxCollider(MyRectangle collider) 
        {
            Collider = collider;
        }
    }
}

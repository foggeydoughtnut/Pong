using Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    public class Sprite : Component
    {
        public Texture2D Texture;

        public Sprite(Texture2D texture) 
        {
            Texture = texture;
        }
    }
}

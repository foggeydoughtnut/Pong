using Components;
using Microsoft.Xna.Framework;
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
        public Vector2 Origin;
        public float RenderDepth;

        public Sprite(Texture2D texture, float renderDepth=1f) 
        {
            Texture = texture;
            Origin = new(texture.Width/2, texture.Height/2);
            RenderDepth = renderDepth;
        }
        public Sprite(Texture2D texture, Vector2 origin, float renderDepth = 1f)
        {
            Texture = texture;
            Origin = origin;
            RenderDepth = renderDepth;
        }
    }
}

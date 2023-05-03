using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    public class Text : Component
    {
        public SpriteFont Font;
        public Vector2 Origin;
        public float RenderDepth;
        public string Message;

        public Text(SpriteFont font, string message, float renderDepth=1f) 
        {
            Font = font;
            Message = message;
            Vector2 size = font.MeasureString(message);
            Origin = new Vector2(size.X / 2, size.Y / 2);
            RenderDepth = renderDepth;
        }
        public Text(SpriteFont font, string message, Vector2 origin, float renderDepth = 1f)
        {
            Font = font;
            Message = message;
            Origin = origin;
            RenderDepth = renderDepth;
        }

    }
}

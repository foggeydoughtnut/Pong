using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public static class TextureCreation
    {
        public static GraphicsDevice device;

        public static Texture2D CreateTexture(int width, int height)
        {
            if (device == null)
            {
                throw new Exception("You haven't initialized the graphics device for texture creation");
            }

            Texture2D texture = new(device, width, height);

            Color[] data = new Color[width * height];

            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                data[pixel] = new Color(Color.Blue, 0.25f);
            }

            texture.SetData(data);

            return texture;
        }
    }
}

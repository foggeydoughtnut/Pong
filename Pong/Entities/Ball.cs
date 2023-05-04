using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Ball
    {
        public static GameObject Create(Texture2D texture, int x, int y, float xDirection)
        {
            float speed = 130f;
            GameObject ball = new("Ball");
            ball.Add(new Components.Sprite(texture));
            ball.Add(new Components.Transform(x, y));


            // Generate random value between -1 and 1
            Random random = new();
            float randomValue = (float)(random.NextDouble() * 2) - 1;

            Vector2 initialDirection = new(xDirection, randomValue);
            initialDirection.Normalize();

            ball.Add(new Components.Rigidbody(initialDirection, speed));
            ball.Add(new Components.BoxCollider(new Vector2(x - 4, y - 4), new Vector2(8, 8)));
            return ball;
        }
    }
}

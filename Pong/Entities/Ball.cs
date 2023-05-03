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
        public static GameObject Create(Texture2D texture, int x, int y)
        {
            float speed = 200f;
            GameObject ball = new("Ball");
            ball.Add(new Components.Sprite(texture));
            ball.Add(new Components.Transform(x, y));

            // TODO, make this random
            Vector2 initialDirection = new(1, 1);
            initialDirection.Normalize();

            ball.Add(new Components.Rigidbody(initialDirection, speed));
            ball.Add(new Components.BoxCollider(new Vector2(x - 4, y - 4), new Vector2(8, 8)));
            return ball;
        }
    }
}

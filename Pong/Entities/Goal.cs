using Entities;
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
    public class Goal
    {
        public static GameObject Create(int x, int y, int width, int height, ushort playerNum)
        {
            GameObject goal = new($"Goal{playerNum}");
            goal.Add(new Components.Transform(x, y));
            goal.Add(new Components.Rigidbody(Vector2.Zero, 0f));
            goal.Add(new Components.BoxCollider(new Vector2(x, y), new Vector2(width,height)));
            return goal;
        }

    }
}

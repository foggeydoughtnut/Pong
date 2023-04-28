﻿using Components;
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
    public class Player
    {
        public static GameObject Create(Texture2D texture, int x, int y, Dictionary<string, Keys> controls)
        {
            float speed = 200f;
            GameObject player = new();
            player.Add(new Components.Sprite(texture));
            player.Add(new Components.Transform(x, y));
            player.Add(new Components.Rigidbody(Vector2.Zero, speed));

            KeyboardInput keyboardInput = new();
            keyboardInput.actionKeyPairs.Add("MoveUp", controls["up"]);
            keyboardInput.actionKeyPairs.Add("MoveDown", controls["down"]);

            player.Add(keyboardInput);

            return player;
        }
    }
}

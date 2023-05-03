using Components;
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
        public static GameObject Create(Texture2D texture, int x, int y, Dictionary<string, Keys> controls, ushort playerNum)
        {
            float speed = 200f;
            GameObject player = new($"Player{playerNum}");
            player.Add(new Components.Sprite(texture));
            player.Add(new Components.Transform(x, y));
            player.Add(new Components.Rigidbody(Vector2.Zero, speed));
            player.Add(new Components.BoxCollider(new Vector2(x - 4, y - texture.Height/2), new Vector2(8, texture.Height)));

            KeyboardInput keyboardInput = new();
            keyboardInput.actionKeyPairs.Add($"MoveUp{playerNum}", controls["up"]);
            keyboardInput.actionKeyPairs.Add($"MoveDown{playerNum}", controls["down"]);

            player.Add(keyboardInput);

            return player;
        }
    }
}

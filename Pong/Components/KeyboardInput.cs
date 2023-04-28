using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    public class KeyboardInput : Input
    {
        public Dictionary<string, Keys> actionKeyPairs;
        public Dictionary<string, bool> actions;
        public Dictionary<string, bool> previousActions;
        public KeyboardInput() 
        {
            actionKeyPairs = new();
            actions = new();
            previousActions = new();
        }

    }
}

using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems
{
    public abstract class System
    {
        // Contains the gameObjects this system is interested in, keyed by their IDs
        protected Dictionary<uint, GameObject> _gameObjects = new();

        public System(params Type[] componentTypes)
        {
            ComponentTypes = componentTypes;
        }
        private Type[] ComponentTypes { get; set; }

        protected virtual bool IsInterested(GameObject gameObject)
        {
            foreach (Type type in ComponentTypes)
            {
                if (!gameObject.ContainsComponent(type))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Add(GameObject gameObject)
        {
            if (IsInterested(gameObject))
            {
                _gameObjects.Add(gameObject.Id, gameObject);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Remove(uint id)
        {
            return _gameObjects.Remove(id);
        }

        public abstract void Update(GameTime gameTime);
    }
}

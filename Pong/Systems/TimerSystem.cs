using Components;
using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems
{
    public class TimerSystem : System
    {
        private Action<GameObject> _timerComplete;


        public TimerSystem(Action<GameObject> timerComplete) : base(typeof(Components.Timer))
        {
            _timerComplete = timerComplete;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in _gameObjects.Values) 
            {
                gameObject.GetComponent<Timer>().ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (gameObject.GetComponent<Timer>().ElapsedTime > gameObject.GetComponent<Timer>().Delay)
                {
                    gameObject.GetComponent<Timer>().OnTimerEnd();
                    _timerComplete(gameObject);
                }
            }
        }
    }
}

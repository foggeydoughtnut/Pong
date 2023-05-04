using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    public class Timer : Component
    {
        public float ElapsedTime;
        public float Delay;
        public Action OnTimerEnd;
        public Timer(float elapsedTime, float delay, Action onTimerEnd) 
        {
            ElapsedTime = elapsedTime;
            Delay = delay;
            OnTimerEnd = onTimerEnd;
        }
    }
}

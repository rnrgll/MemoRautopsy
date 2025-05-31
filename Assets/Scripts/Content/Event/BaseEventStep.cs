using Event;
using UnityEngine;

namespace Event
{
    public abstract class BaseEventStep : ScriptableObject
    {
        public abstract void Run(EventSequenceRunner runner);
    }
}
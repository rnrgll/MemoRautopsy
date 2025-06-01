using Managers;
using UnityEngine;

namespace Event
{
    public class TriggerEventObject : MonoBehaviour
    {
        [SerializeField] private EventSequence sequence;

        public void Trigger()
        {
            Manager.Event.Runner.LoadSequence(sequence);
            Manager.Event.Runner.StartSequence();
        }
    }
}
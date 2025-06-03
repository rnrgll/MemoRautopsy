using System.Collections.Generic;
using Event;
using Managers;
using UnityEngine;

namespace Content.ETC
{
    public class RunEventSequence : MonoBehaviour
    {
        [SerializeField] private List<EventSequence> events;
        
        
        public void StartEvenSequence(int index)
        {
            if(index<0 || index>=events.Count) return;
            
            Manager.Event.Runner.LoadSequence(events[index]);
            Manager.Event.Runner.StartSequence();
        }
    }
}
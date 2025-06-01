using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "EventSequence", menuName = "Data/Event/EventSqeunce", order = -100)]
    public class EventSequence : ScriptableObject
    {
        public List<BaseEventStep> Steps;
    }
}
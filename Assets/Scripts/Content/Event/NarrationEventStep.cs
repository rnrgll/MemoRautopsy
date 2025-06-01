using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "Narration", menuName = "Data/Event/Narration")]
    public class NarrationEventStep : BaseEventStep
    {
        [TextArea(3, 10)]
        public List<string> dialogueLines;
        
        public override void Run(EventSequenceRunner runner)
        {
            Manager.UI.ShowDialouge(dialogueLines,
                ()=> runner.NextStep());
        }
    }
}
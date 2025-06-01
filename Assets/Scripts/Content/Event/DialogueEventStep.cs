using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Data/Event/Dialogue")]
    public class DialogueEventStep : BaseEventStep
    {
        public List<DialogueBlock> dialogueBlocks;
        
        
        public override void Run(EventSequenceRunner runner)
        {
            Manager.UI.ShowDialouge(dialogueBlocks,
                ()=> runner.NextStep());
        }
    }
}
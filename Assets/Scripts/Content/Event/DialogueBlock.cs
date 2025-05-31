using System;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    [Serializable]
    public class DialogueBlock
    {
        public string speakerName;
        [TextArea] public List<string> lines;
    }
}
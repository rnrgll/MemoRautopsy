using System;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    [Serializable]
    public class DialogueBlock
    {
        public string speakerName;
        [TextArea(2, 4)] public List<string> lines;
    }
}
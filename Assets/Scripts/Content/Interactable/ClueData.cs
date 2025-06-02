using System;
using UnityEngine;
using Utility;

namespace Content.Interactable
{
    public enum ClueSourceType { Autopsy, Memory, Other }
    
    
    [CreateAssetMenu(fileName = "ClueData", menuName = "Data/ClueData", order = 0)]
    [Serializable]
    public class ClueData : ScriptableObject
    {
        public Define.ClueId clueId;
        public int day;
        public ClueSourceType source;
        public string title;
        [TextArea(2,5)] public string description;
        [TextArea(2,5)]  public string playerThoughts;
        public Sprite clueImage;
    }
}
using System;
using UnityEngine;
using Utility;

namespace Content.Interactable
{
    [CreateAssetMenu(fileName = "ClueData", menuName = "Data/ClueData", order = 0)]
    [Serializable]
    public class ClueData : ScriptableObject
    {
        public Define.ClueId clueId;
        public int day;
        public string title;
        public string description;
        public string playerThoughts;
        public Sprite clueImage;
    }
}
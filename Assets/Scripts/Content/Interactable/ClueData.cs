using UnityEngine;

namespace Content.Interactable
{
    [CreateAssetMenu(fileName = "ClueData", menuName = "Data/ClueData", order = 0)]
    public class ClueData : ScriptableObject
    {
        public string clueId;
        public string title;
        public string description;
        public string playerThoughts;
        public Sprite clueImage;
    }
}
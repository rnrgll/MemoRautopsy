using UnityEngine;

namespace Content.UI
{
    public class SharedUI : MonoBehaviour
    {
        [field: SerializeField] public DialogueUI Dialogue { get; private set; }
        [field: SerializeField] public ClueNoteUI ClueNote { get; private set; }
        // [field: SerializeField] public SettingUI Setting { get; private set; }

    }
}
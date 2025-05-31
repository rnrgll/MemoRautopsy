using UnityEngine;

namespace System.Interfaces
{
    public interface IUIInteractable
    {
        string InteractText { get; }
        Transform GetUIPosition();
    }
}
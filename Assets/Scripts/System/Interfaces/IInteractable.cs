using Util;

namespace System.Interfaces
{
    
    public interface IInteractable
    {
        [field: Serializable] public string InteractText { get; protected set; }
        public Define.Scene ConnectScene { get; protected set; }
        public void Interact();
    }
}
using Utility;

namespace System.Interfaces
{
    
    public interface IInteractable
    { 
        public Define.Scene ConnectScene { get; protected set; }
        public void Interact();
    }
}
using Cinemachine;
using UnityEngine;

namespace Content.Interactable
{
    public class PaperObject : InteractObject
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCam;
        
        public override void Interact()
        {
            _virtualCam.gameObject.SetActive(true);
            base.Interact();
            
        }
    }
}
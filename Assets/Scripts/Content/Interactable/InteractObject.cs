using System.Collections.Generic;
using System.Interfaces;
using Content.UI;
using Event;
using Managers;
using UnityEngine;
using Utility;

namespace Content.Interactable
{
    //triggerObject
    //interactObject
    public class InteractObject : MonoBehaviour, IInteractable, IUIInteractable
    {
        [SerializeField] private List<string> _infoList;
        [SerializeField] private Transform _uiAnchor;
        
        private Define.Scene connectScene;
        
        public string InteractText { get; }
        public Transform GetUIPosition()
        {
            return _uiAnchor;
        }
        
        Define.Scene IInteractable.ConnectScene
        {
            get => connectScene;
            set => connectScene = value;
        }

        public void Interact()
        {
            Debug.Log("adfsf");
            //진짜 상호작용을 구현한다.
            GetComponent<TriggerEventObject>().Trigger();
        }

      
    }
}
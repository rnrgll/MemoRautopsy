using System.Collections.Generic;
using System.Interfaces;
using UnityEngine;
using Util;

namespace Content.Interactable
{
    public class BodyDummy : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<string> infoList;
        
        private string interactText;
        private Define.Scene connectScene;

        string IInteractable.InteractText
        {
            get => interactText;
            set => interactText = value;
        }

        Define.Scene IInteractable.ConnectScene
        {
            get => connectScene;
            set => connectScene = value;
        }

        public void Interact()
        {
            Debug.Log(infoList[0]);
        }
    }
}
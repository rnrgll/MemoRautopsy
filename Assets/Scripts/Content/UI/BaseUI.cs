using System;
using Managers;
using UnityEngine;

namespace Content.UI
{
    public abstract class BaseUI : MonoBehaviour
    {
        private void Awake() => Init();

        private void Init()
        {
            Manager.UI.SetCanvas(gameObject);
        }

        public virtual void OnClicked()
        {
            
        }

        public virtual void ClosedUI()
        {
            Manager.UI.CloseUI(this);
        }
    }
}
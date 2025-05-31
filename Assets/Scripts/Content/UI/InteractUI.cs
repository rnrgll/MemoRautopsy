    using System;
    using DesignPattern;
    using UnityEngine;

    namespace Content.UI
    {
        public class InteractUI : PooledObject
        {
            private Transform _cameraTransform;
            
            //private void Awake() => Init();

            private void LateUpdate() => SetUIForward(_cameraTransform.forward);

            
            public void SetCamera()
            {
                _cameraTransform = Camera.main.transform;
            }
            
            private void SetUIForward(Vector3 target)
            {
                if (target == null || !gameObject.activeSelf ) return;
                transform.forward = target;
            }
        }
    }
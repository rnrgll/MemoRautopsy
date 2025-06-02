using System;
using System.Interfaces;
using Content.Interactable;
using Content.UI;
using UnityEngine;

namespace Managers
{
    public class InteractionManager : MonoBehaviour
    {
        [field: SerializeField] public bool IsControlActive { get; set; } = true; //제어
        
        private Vector3 rayOrigin;
        private IInteractable currentTarget;
        private IInteractable lastTarget;
        private InteractUI currentUI;

        [SerializeField] private KeyCode keyCode;
        
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] [Range(2f, 5f)] private float rayLength = 2f;
        

        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();

        private void Start()
        {  
            IsControlActive = !Manager.UI.IsUIActive.Value;
            
         
        }

        private void Update()
        {
            if (!IsControlActive) return;
            
            currentTarget = ShootRay();
            
            if (currentTarget != lastTarget)
            {
                //이전 ui 제거(풀로 돌려보내기)
                if (currentUI != null)
                {
                    currentUI.ReturnToPool();
                    currentUI = null;
                }

                if (currentTarget is IUIInteractable uiTarget)
                {
                    currentUI = Manager.UI.ShowInteractUI(uiTarget.GetUIPosition());
                }
                
                lastTarget = currentTarget;
            }

            //상호작용
            if (currentTarget != null && Input.GetKeyDown(keyCode))
            {
                if (currentUI != null)
                {
                    currentUI.ReturnToPool();
                    currentUI = null;
                }
                lastTarget = null;
                currentTarget.Interact();
            }
            
        }

        
        //Ray를 쏜다(마우스 포인터 기준으로)
        private IInteractable ShootRay()
        {
            Vector3 screenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * rayLength,  Color.green);
            if (Physics.Raycast(ray,  out hit, rayLength, targetLayer))
            {
                return hit.transform.GetComponent<IInteractable>();
            }

            return null;
        }
        
        private void SubscribeEvents()
        {
           Manager.UI.IsUIActive.Subscribe(SetControlActive);
        }

        private void UnsubscribeEvents()
        {
           Manager.UI.IsUIActive.Unsubscribe(SetControlActive);
        }

        private void SetControlActive(bool value) => IsControlActive = !value;


    }
}
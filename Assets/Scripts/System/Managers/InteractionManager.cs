using System;
using System.Interfaces;
using UnityEngine;

namespace Managers
{
    public class InteractionManager : MonoBehaviour
    {
        
        public bool IsControlActive { get; set; } = true; //제어
        
        private Vector3 rayOrigin;
        private IInteractable currentTarget;
        
        
        [SerializeField] private LayerMask targetLayer;


        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();


        private void Update()
        {
            if (!IsControlActive) return;
            
            currentTarget = ShootRay();
            

            if (Input.GetKeyDown(KeyCode.Mouse0) && currentTarget != null)
            {
                Debug.Log("마우스 클릭");
                currentTarget.Interact();
            }
            
        }

        
        //Ray를 쏜다(마우스 포인터 기준으로)
        private IInteractable ShootRay()
        {
            Vector3 screenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100f,  Color.green);
            if (Physics.Raycast(ray,  out hit, 100f, targetLayer))
            {
                return hit.transform.GetComponent<IInteractable>();
            }

            return null;
        }
        
        //오브젝트가 iinteractable이면 outline을 만든다
        
        //플레이어가 f를 누르면 interact를 호출한다.
        
        
        
        private void SubscribeEvents()
        {
            Debug.Log("subscribe호출");
           Manager.UI.IsUIActive.Subscribe(SetControlActive);
        }

        private void UnsubscribeEvents()
        {
           Manager.UI.IsUIActive.Unsubscribe(SetControlActive);
        }

        private void SetControlActive(bool value) => IsControlActive = !value;


    }
}
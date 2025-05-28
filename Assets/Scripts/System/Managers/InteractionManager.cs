using System;
using System.Interfaces;
using UnityEngine;

namespace Managers
{
    public class InteractionManager : MonoBehaviour
    {
        private Vector3 rayOrigin;
        private IInteractable currentTarget;
        [SerializeField] private LayerMask targetLayer;
        
        //Ray를 쏜다(마우스 포인터 기준으로)
        private void Update()
        {
            currentTarget = ShootRay();

            if (Input.GetKeyDown(KeyCode.Mouse0) && currentTarget != null)
            {
                currentTarget.Interact();
            }
            
        }

        private IInteractable ShootRay()
        {
            Vector3 screenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100f,  Color.green);
            if (Physics.Raycast(ray, out hit, targetLayer))
            {
                return hit.transform as IInteractable;
            }

            return null;
        }
        
        //오브젝트가 iinteractable이면 outline을 만든다
        
        //플레이어가 f를 누르면 interact를 호출한다.
        
        
        
        
        
    }
}
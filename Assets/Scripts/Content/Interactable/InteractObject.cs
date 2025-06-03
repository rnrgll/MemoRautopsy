using System;
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

        [Header("상호작용 ID")]
        public string interactionId;
        public string nextInteractionId;
        
        [Header("상호작용 전후 상태 설정")]
        [SerializeField] private bool hideBeforeInteraction = false;
        [SerializeField] private bool hideAfterInteraction = true;
        [SerializeField] private bool isStartingInteraction = false;
        
        [Header("다음 상호작용 오브젝트")]
        public InteractObject nextInteractTarget;
        
        
        public bool isInteractable;
        private void Start()
        {
            if (Manager.Data.IsCompleted(interactionId))
            {
                HandleAlreadyCompleted();
            }
            else
            {
                if (isStartingInteraction)
                {
                    EnableInteraction(); // 시작 지점은 무조건 상호작용 가능하게
                }
                else
                {
                    HandleNotCompletedYet();
                    
                }
            }
            
            
        }
        

        private void HandleAlreadyCompleted()
        {
            isInteractable = false;

            if (hideAfterInteraction)
                gameObject.SetActive(false);
            else
            {
                gameObject.SetActive(true);
                //콜라이더를 꺼서 레이 감지안되게 하기
                GetComponent<Collider>().enabled = false;
            }
            

        }
        private void HandleNotCompletedYet()
        {
            if (hideBeforeInteraction)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                GetComponent<Collider>().enabled = false;
            }
        }
        
        public void EnableInteraction()
        {
            gameObject.SetActive(true);
            GetComponent<Collider>().enabled = true;
            isInteractable = true;
        }
        
        
        public Transform GetUIPosition()
        {
            return _uiAnchor;
        }
        
        public virtual void Interact()
        {
            if (!isInteractable) return;
            
            GetComponent<TriggerEventObject>().Trigger();
            
            Manager.Data.SetCompleted(interactionId);
            
            isInteractable = false;
            GetComponent<Collider>().enabled = false;
            
            
            if (hideAfterInteraction)
                gameObject.SetActive(false);
            if (nextInteractTarget != null)
            {
                nextInteractTarget.EnableInteraction();
            }
          
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("인트로/아웃트로는 트리거로 실행됩니다.");
                Interact();
            }
        }
    }
}
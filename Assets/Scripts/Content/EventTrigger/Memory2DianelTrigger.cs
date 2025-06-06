using Event;
using Managers;
using UnityEngine;

namespace Content.EventTrigger
{
    public class Memory2DianelTrigger : MonoBehaviour
    {
        [SerializeField] private bool isTriggered = false;
        [SerializeField] private Collider triggerCollider;
        [SerializeField] private LayerMask detectLayer;

        [SerializeField] private EventSequence eventSequence;
        [SerializeField] private Animator anim;

        private Transform playerTransform;
        private void OnTriggerEnter(Collider other)
        {
            if(isTriggered) return;
            if (((1 << other.gameObject.layer) & detectLayer) != 0)
            {
                isTriggered = true;
                triggerCollider.enabled = false;

                playerTransform = other.transform;
                anim.SetTrigger("Stand");
                
            }
        }
    
        
        //stand 애니메이션 event에서 쓰기
        public void LookAtPlayer()
        {
            Transform target = playerTransform;
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0; // 수평만

            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = targetRot; // 또는 부드럽게 Slerp 처리
        
            Debug.Log("플레이어 바라보기");
        }

        public void PlayEvent()
        {
            LookAtPlayer();
            
            Manager.Event.Runner.LoadSequence(eventSequence);
            Manager.Event.Runner.StartSequence();
        }
    }
}
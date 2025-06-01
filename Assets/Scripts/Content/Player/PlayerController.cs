using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public bool IsControlActive { get; set; } = true; //제어

        private PlayerStatus _status;
        private PlayerMovement _movement;
        private Animator _animator;
        [SerializeField] private CinemachineVirtualCamera _sightCamera;
        
        
        private void Awake() => Init();
        private void Update() => HandlePlayerControl();
        private void OnEnable() => SubscribeEvents();

        private void OnDisable() => UnsubscribeEvents();

        
        //초기 설정 및 셋팅 ------
        private void Init()
        {
            _status = GetComponent<PlayerStatus>();
            _movement = GetComponent<PlayerMovement>();
            _animator = GetComponent<Animator>();

            SetControlActive(false); //반대로 넣어주기

        }

        public void SetInitRotation(Vector3 eulerAngle)
        {
            _movement.SetInitRotation(eulerAngle);
        }
        

        
        // 컨트롤 ----------
        private void HandlePlayerControl()
        {
            if (!IsControlActive) return;
            HandleMovement();
            
        }

        private void HandleMovement()
        {
            //마우스 회전 방향으로 회전시키기
            Vector3 sightDir = _movement.SetRotation();
            
            //움직이기
            Vector3 moveDir = _movement.SetMove();
            _status.IsMoving.Value = (moveDir != Vector3.zero);
            
        }
        
        
        //프로퍼티 구독 -----
        private void SubscribeEvents()
        {
            Manager.UI.IsUIActive.Subscribe(SetControlActive);
            
            
            // _status.IsControlActive.Subscribe(SetCursorLock);
            // _status.IsControlActive.Subscribe(SetMoveStop);
          
        }

        private void UnsubscribeEvents()
        {
            Manager.UI.IsUIActive.Unsubscribe(SetControlActive);
            // _status.IsControlActive.Unsubscribe(SetCursorLock);
            // _status.IsControlActive.Unsubscribe(SetMoveStop);
        }

        
        //property 구독
        //플레이어 컨트롤 관련 메서드
        public void SetControlActive(bool value)
        {
            IsControlActive = !value;
            SetCursorLock(IsControlActive);
            SetMoveStop(IsControlActive);
        }
        private void SetCursorLock(bool value)
        {
            Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !value;
        }

        private void SetMoveStop(bool value)
        {
            if(!value)
                _movement.SetStop();
        }
        
    }

}

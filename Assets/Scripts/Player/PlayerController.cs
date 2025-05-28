using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        //public bool IsControlActive { get; set; } = true; //제어

        private PlayerStatus _status;
        private PlayerMovement _movement;
        private Animator _animator;
        [SerializeField] private CinemachineVirtualCamera _sightCamera;
        
        
        private void Awake() => Init();
        private void OnEnable() => SubscribeEvents();
        private void Update() => HandlePlayerControl();

        private void OnDisable() => UnsubscribeEvents();

        
        private void Init()
        {
            _status = GetComponent<PlayerStatus>();
            _movement = GetComponent<PlayerMovement>();
            _animator = GetComponent<Animator>();
            
            SetCursorLock(true);

        }

        private void HandlePlayerControl()
        {
            if (!_status.IsControlActive.Value) return;
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
        
        
        private void SubscribeEvents()
        {
            _status.IsControlActive.Subscribe(SetCursorLock);
            _status.IsControlActive.Subscribe(SetMoveStop);
            
           
          
        }

        private void UnsubscribeEvents()
        {
            _status.IsControlActive.Unsubscribe(SetCursorLock);
            _status.IsControlActive.Unsubscribe(SetMoveStop);
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        //플레이어는 앞뒤양옆 이동, 회전은 마우스 시야 회전으로 처리
        //위 아래 시야 회전은 카메라만

        [SerializeField] private Transform _sight;
        
        private Rigidbody _rigidbody;
        private PlayerStatus _playerStatus;
        
        [Header("Mouse Config")] 
        [SerializeField][Range(-90, 0)] private float _minPitch; //최소 각도
        [SerializeField][Range(0, 90)] private float _maxPitch; //최대 각도
        [SerializeField][Range(0, 5)] private float _mouseSensitivity = 1;
        
        private Vector2 _currentRotation;

        private void Awake() => Init();
        
        private void Init()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerStatus = GetComponent<PlayerStatus>();
        }

        public Vector3 SetMove()
        {
            float moveSpeed = _playerStatus.MoveSpeed;
            Vector3 moveDirection = GetMoveDirection();
            Vector3 velocity = _rigidbody.velocity;

            velocity.x = moveDirection.x * moveSpeed;
            velocity.z = moveDirection.z * moveSpeed;

            _rigidbody.velocity = velocity;
            

            return moveDirection;
        }

        public void SetStop()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        public Vector3 SetRotation()
        {
            Vector2 mouseDir = GetMouseDirection();

            _currentRotation.x += mouseDir.x;
            _currentRotation.y = Mathf.Clamp(_currentRotation.y + mouseDir.y, _minPitch, _maxPitch);
            
            //좌우 회전은 전체
            transform.rotation = Quaternion.Euler(0,_currentRotation.x, 0);
            
            //상하 회전은 sight만
            Vector3 sightEulerAngle = _sight.localEulerAngles;
            _sight.localEulerAngles = new Vector3(_currentRotation.y, sightEulerAngle.y, sightEulerAngle.z);
            
            //회전 방향 벡터 반환
            Vector3 rotateDir = transform.forward;
            rotateDir.y = 0;
            return rotateDir.normalized;
        }

        public void SetInitRotation(Vector3 eulerAngle)
        {
            _currentRotation.x = eulerAngle.y; //좌우 회전값
            _currentRotation.y = eulerAngle.x; //위아래 회전값
        }

        public Vector3 GetMoveDirection()
        {
            Vector3 input = GetInputDirection();
            Vector3 moveDir =
                transform.right * input.x
                + transform.forward * input.z;
            return moveDir.normalized;
        }

        public Vector3 GetInputDirection()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            return new Vector3(h, 0, v);
        }


        public Vector2 GetMouseDirection()
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

            return new Vector2(mouseX, -mouseY);

        }
        
    }

}

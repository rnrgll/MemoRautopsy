using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        //이동 속도, 회전속도
        [field: SerializeField]
        [field: Range(0, 10)]
        public float MoveSpeed { get; set; }
        
        [field: SerializeField]
        [field: Range(0, 10)]
        public float RotateSpeed { get; set; }
        
        
        //플레이어 상태
        public ObservableProperty<bool> IsMoving { get; private set; } = new();
        public ObservableProperty<bool> IsInvestigating = new ObservableProperty<bool>(); // 단서 확인, UI 조사 등
        public ObservableProperty<bool> IsSolvingPuzzle = new ObservableProperty<bool>(); // 단서 조합, 추리 인터페이스 등
        public ObservableProperty<bool> IsInCutscene = new ObservableProperty<bool>();    // 연출 컷신 등
        public ObservableProperty<bool> IsControlActive = new();


        private void OnEnable()
        {
            IsInCutscene.Subscribe(UpdateControlState);
            IsInvestigating.Subscribe(UpdateControlState);
            IsSolvingPuzzle.Subscribe(UpdateControlState);
            
            UpdateControlState(false);
        }

        private void OnDisable()
        {
            IsInCutscene.Unsubscribe(UpdateControlState);
            IsInvestigating.Unsubscribe(UpdateControlState);
            IsSolvingPuzzle.Unsubscribe(UpdateControlState);
        }


        private void UpdateControlState(bool _)
        {
            IsControlActive.Value =
                !IsInCutscene.Value &&
                !IsInvestigating.Value &&
                !IsSolvingPuzzle.Value;
        }
        
        
        
        
    }

}

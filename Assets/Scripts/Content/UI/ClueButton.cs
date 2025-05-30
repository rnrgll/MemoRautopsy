using System;
using Content.Interactable;
using DesignPattern;
using UnityEngine;
using UnityEngine.UI;

namespace Content.UI
{
    public class ClueButton : PooledObject
    {
        [SerializeField] private Image _btnImg;
        [SerializeField] private Animator _anim;
        
        private ClueData _clueData;
        private ClueNoteUI _parentUI;
        private readonly int HOVER_TO_PRESSED = Animator.StringToHash("Hover to Pressed");
        private readonly int PRESSED_TO_NORMAL = Animator.StringToHash("Pressed to Normal");
        
        //observable property---
        public ObservableProperty<bool> IsSelected = new();
       
        
        private void Awake() => Init();

        private void Init()
        {
            _anim = GetComponent<Animator>();
        }

        private void OnEnable() => SetSubscribe();
        private void OnDisable() => SetUnsubscribe();


        public void SetParent(ClueNoteUI parent) => _parentUI = parent;

        public void SetData(ClueData clueData)
        {
            _clueData = clueData;
            _btnImg.sprite = _clueData.clueImage;
        }

        public ClueData GetData() => _clueData;

        public void ClickTest()
        {
            _parentUI.OnClueButtonClicked(this);
        }


        private void SetSubscribe()
        {
            IsSelected.Subscribe(SetBtnAnimation);
        }

        private void SetUnsubscribe()
        {
            IsSelected.Unsubscribe(SetBtnAnimation);
        }

        private void SetBtnAnimation(bool value)
        {
            if(value) _anim.Play(HOVER_TO_PRESSED);
            else _anim.Play(PRESSED_TO_NORMAL);
        }
    }
}
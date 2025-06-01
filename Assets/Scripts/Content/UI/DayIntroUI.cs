using System;
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Content.UI
{
  
    public class DayIntroUI : BaseUI
    {
        [SerializeField] private Text _text;
        [SerializeField] private float _delay;
        [SerializeField] private Animator _anim;
        [SerializeField] private float _animSpeed;
        private Action _onComplete;

        protected override void Init()
        {
            base.Init();
            // _anim = GetComponent<Animator>();
            
        }

        private void OnEnable()
        {
            //_anim.speed = _animSpeed;
            _anim.Play("In");
            
        }

        public void SetData(string text, float delay, Action onComplete = null)
        {
            _text.text = text;
            _delay = delay;
            _onComplete = onComplete;
            
        }

        public void StartIntro()
        {
            StartCoroutine(FadeOutAfterDelay());
        }
        
      

        private IEnumerator FadeOutAfterDelay()
        {
            yield return new WaitForSeconds(_delay);
            
            _anim.Play("Out");
            // 상태가 "Out"으로 바뀔 때까지 대기
            yield return new WaitUntil(() =>
                _anim.GetCurrentAnimatorStateInfo(0).IsName("Out"));
            yield return new WaitWhile(() =>
                _anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
            Manager.UI.CloseUI(this);
            _onComplete?.Invoke();
        }
    }
}
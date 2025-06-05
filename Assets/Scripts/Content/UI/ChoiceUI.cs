using System;
using Michsky.UI.Dark;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Content.UI
{
    public class ChoiceUI : BaseUI
    {
        [SerializeField] private TMP_Text _question;
        
        [SerializeField] private ButtonManager _optionA;
        [SerializeField] private ButtonManager _optionB;
        
        
        private CanvasGroup cg;
        private Action<int> OnComplete;
        
        protected override void Init()
        {
            base.Init();
            cg = GetComponent<CanvasGroup>();
            OnComplete = null;
        }

        private void OnEnable()
        {
            _optionA.clickEvent.AddListener(()=> OnComplete?.Invoke(0));
            _optionB.clickEvent.AddListener(()=> OnComplete?.Invoke(1));

        }

        private void OnDestroy()
        {
            _optionA.clickEvent.RemoveAllListeners();
            _optionB.clickEvent.RemoveAllListeners();
        }

        private void OnDisable()
        {
            OnComplete = null;
        }
        
        
        public void SetData(string q, string oa, string ob, Action<int> onComplete)
        {
            //텍스트 설정
            _question.text = q;
            _optionA.buttonText = oa;
            _optionB.buttonText = ob;
            _optionA.UpdateUI();
            _optionB.UpdateUI();
            
            OnComplete = (index) =>
            {
                Util.UIDisable(cg);
                onComplete?.Invoke(index); 
            };
        }

        public void ShowChoice()
        {
            Util.UIEnable(cg);
        }
        
        
        
    }
}
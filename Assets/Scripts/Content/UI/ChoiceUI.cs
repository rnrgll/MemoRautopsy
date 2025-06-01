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
        
        [SerializeField] private Button _optionA;
        [SerializeField] private Button _optionB;
        
        
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
            _optionA.onClick.AddListener(()=> OnComplete?.Invoke(0));
            _optionB.onClick.AddListener(()=> OnComplete?.Invoke(1));

        }

        private void OnDestroy()
        {
            _optionA.onClick.RemoveAllListeners();
            _optionB.onClick.RemoveAllListeners();
        }

        private void OnDisable()
        {
            OnComplete = null;
        }
        
        
        public void SetData(string q, string oa, string ob, Action<int> onComplete)
        {
            _question.text = q;
            _optionA.GetComponent<ButtonManager>().buttonText = oa;
            _optionB.GetComponent<ButtonManager>().buttonText = ob;
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
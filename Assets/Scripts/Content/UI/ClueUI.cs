using System;
using System.Collections.Generic;
using Content.Interactable;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Utility;


namespace Content.UI
{
    public class ClueUI : BaseUI
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _image;

        [SerializeField] private List<CanvasGroup> _controlGroups;

        private void OnDisable() => HideUI();

        public void SetData(ClueData clueData)
        {
            if (clueData == null) return;

            _title.text = clueData.title;
            _description.text = clueData.description;
            _image.sprite = clueData.clueImage;
            
            ShowUI();
        }

        public override void OnClicked()
        {
            Debug.Log("clue UI 마우스 클림됨");
            base.ClosedUI();
        }

        private void HideUI()
        {
            foreach (var target in _controlGroups)
            {
                Util.UIDisable(target);
            }
        }

        private void ShowUI()
        {
            foreach (var target in _controlGroups)
            {
                Util.UIEnable(target);
            }
        }
    }
}
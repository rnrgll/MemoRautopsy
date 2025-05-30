using System;
using System.Collections.Generic;
using Content.Interactable;
using Managers;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;


namespace Content.UI
{
    public class ClueUI : BaseUI, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _image;

        [SerializeField] private List<CanvasGroup> _controlGroups;

        private Define.ClueId _clueId;
        private bool _isNew; //todo: 시간되면 새로운 단서는 ui 추가로 띄워주는거 만들기
        
        private void OnDisable() => HideUI();

        public void SetData(ClueData clueData)
        {
            if (clueData == null) return;

            _clueId = clueData.clueId;    
            _title.text = clueData.title;
            _description.text = clueData.description;
            _image.sprite = clueData.clueImage;
            
            ShowUI();
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

        public void OnPointerClick(PointerEventData eventData)
        {
            ClueManager.Instance.CloseClueUI(this, _clueId);
        }
    }
}
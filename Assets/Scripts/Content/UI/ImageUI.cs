using System;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

namespace Content.UI
{
    public class ImageUI : BaseUI, IPointerClickHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _cg;
        
        public event Action onClose;

        public void SetData(Sprite sprite)
        {
            _image.sprite = sprite;
            ShowUI();
        }

        private void ShowUI()
        {
            Util.UIEnable(_cg);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClose?.Invoke();
        }
    }
}
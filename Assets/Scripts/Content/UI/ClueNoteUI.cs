using System;
using System.Collections.Generic;
using Content.Interactable;
using DesignPattern;
using Managers;
using Michsky.UI.Dark;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Content.UI
{
    public class ClueNoteUI : BaseUI
    {
        [Serializable]
        public class NotePanel
        {
            public Transform GridContent;
            public TMP_Text ClueTitle;
            public TMP_Text ClueDescription;
            public TMP_Text PlayerThought;
        }

        [SerializeField] private List<NotePanel> _panels;
        [SerializeField] private ClueButton _clueBtnPrefab;
        [SerializeField] private Transform _poolTransform;
        [SerializeField] private MainPanelManager _targetPanel;
        
        
        private ObjectPool _clueBtnPool;
        private ClueButton _selectedBtn;
        private CanvasGroup _targetPanelCG; 
        private Animator _targetPanelAnim;
        
        
        
        [SerializeField] private List<ClueData> _clueDataByDay = new();
        
        //public ObservableProperty<int> clikedDay = new();
        private int _clickedDay = 0;
        [field: SerializeField]
        public int ClickedDay
        {
            get => _clickedDay;
            set
            {
                if(_clickedDay>=1 && _clickedDay<=_panels.Count)
                    UnloadClueButtons();
                
                _clickedDay = value;
                LoadClueDataByDay(_clickedDay);
                LoadClueButtons();
                
            }
        }
        

        protected override void Init()
        {
            base.Init();
            _clueBtnPool = new(_clueBtnPrefab,_poolTransform);
            _clueBtnPool.Init(12);
            
            _targetPanelCG = _targetPanel.GetComponent<CanvasGroup>();
            _targetPanelAnim = _targetPanel.GetComponent<Animator>();
            _targetPanelAnim.speed = 1.5f;
        }
        

        
        //저장된 수집 내역을 바탕으로 clue id 리스트 가져오기
        private void LoadClueDataByDay(int day)
        {
            var clueIds = Manager.Data.ClueCollection.GetCluesByDay(day);
            _clueDataByDay.Clear();

            if (clueIds == null) return;
            
            foreach (var id in clueIds)
            {
                _clueDataByDay.Add(Manager.Data.Clue.GetClueData(id));
            }
        }

        //가져온 clue id를 기반으로 pool에서 button을 꺼내고 데이터 넣어주기
        public void LoadClueButtons()
        {
            if(_clueDataByDay == null || _clueDataByDay.Count==0) return;
            foreach (var clueData in _clueDataByDay)
            {
                var clueBtn = _clueBtnPool.Get() as ClueButton;
                clueBtn.transform.SetParent(_panels[ClickedDay-1].GridContent);
                clueBtn.SetParent(this);
                clueBtn.transform.SetAsLastSibling();
                clueBtn.SetData(clueData);
            }
            
            OnClueButtonClicked(_panels[ClickedDay-1].GridContent.transform.GetChild(0).GetComponent<ClueButton>());

        }

        public void UnloadClueButtons()
        {
            Debug.Log(ClickedDay);
            foreach (Transform child in _panels[ClickedDay-1].GridContent)
            {
                if (child.TryGetComponent(out ClueButton btn))
                {
                    btn.IsSelected.Value = false;
                    btn.ReturnToPool();
                }
                   
            }
        }

        public void OnClueButtonClicked(ClueButton clikedBtn)
        {
            if (_selectedBtn != null && _selectedBtn != clikedBtn)
            {
                _selectedBtn.IsSelected.Value = false;
                
            }
            _selectedBtn = clikedBtn;
            _selectedBtn.IsSelected.Value = true;
            UpdateClueInfo(_selectedBtn.GetData());
            
        }

        private void UpdateClueInfo(ClueData clueData)
        {
            _panels[ClickedDay-1].ClueTitle.text = clueData.title;
            _panels[ClickedDay-1].ClueDescription.text = clueData.description;
            _panels[ClickedDay-1].PlayerThought.text = clueData.playerThoughts;

            _panels[ClickedDay - 1].ClueTitle.enabled = true;
            _panels[ClickedDay - 1].ClueDescription.enabled = true;
            _panels[ClickedDay - 1].PlayerThought.enabled = true;
        }


        public void ToggleUI()
        {
            if (_targetPanelCG.alpha == 0)
            {
                if (ClickedDay == 0) ClickedDay = 1;
                
                UnloadClueButtons();
                LoadClueDataByDay(ClickedDay);
                LoadClueButtons();
                _targetPanelAnim.Play("Panel In");
            }
            else _targetPanelAnim.Play("Panel Out");
        }
    }
}
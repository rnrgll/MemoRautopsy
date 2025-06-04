using System;
using System.Collections.Generic;
using System.Linq;
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
        
        //디버깅용--
        [SerializeField] private List<ClueData> _clueDataByDay = new();
        
        private ObjectPool _clueBtnPool;
        private ClueButton _selectedBtn;
        private CanvasGroup _targetPanelCG; 
        private Animator _targetPanelAnim;


        public ObservableProperty<bool> IsControlActive = new() { Value = true }; //제어
        public ObservableProperty<bool> IsOpened = new();
        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();

        
        //public ObservableProperty<int> clikedDay = new();
        private int _clickedDay = 0;
        [field: SerializeField]
        public int ClickedDay
        {
            get => _clickedDay;
            set
            {
                // if (_clickedDay == value) return; // 같은 값이면 무시
                int prevDay = _clickedDay;
                _clickedDay = value;
                
                // 이전에 로딩된 버튼 정리
                if (prevDay > 0 && prevDay <= _panels.Count)
                {
                    UnloadClueButtons(prevDay);
                }
                
                // if(_clickedDay>=1 && _clickedDay<=_panels.Count)
                //     UnloadClueButtons();
                //
                // _clickedDay = value;
                
                LoadClueDataByDay(_clickedDay);
                LoadClueButtons(_clickedDay);
                
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
        public void LoadClueButtons(int day)
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
            
            // 첫 번째 버튼 자동 선택
            var firstBtn = _panels[day - 1].GridContent
                .GetComponentsInChildren<ClueButton>()
                .FirstOrDefault();
            
            if (firstBtn != null)
                OnClueButtonClicked(firstBtn);
            
            // OnClueButtonClicked(_panels[ClickedDay-1].GridContent.transform.GetChild(0).GetComponent<ClueButton>());

        }

        public void UnloadClueButtons(int day)
        {
            Debug.Log(ClickedDay);
            // foreach (Transform child in _panels[day - 1].GridContent)
            // {
            //     if (child.TryGetComponent(out ClueButton btn))
            //     {
            //         btn.IsSelected.Value = false;
            //         btn.ReturnToPool();
            //     }
            //        
            // }
            var content = _panels[day - 1].GridContent;

            foreach (Transform child in content.Cast<Transform>().ToArray())
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
            if(!IsControlActive.Value) return;
            if (_targetPanelCG.alpha == 0)
            {
                IsOpened.Value = true;
                
                if (ClickedDay == 0) ClickedDay = 1;
                
                //강제 리로드
                UnloadClueButtons(ClickedDay);
                LoadClueDataByDay(ClickedDay);
                LoadClueButtons(ClickedDay);
                _targetPanelAnim.Play("Panel In");
            }
            else
            {
                IsOpened.Value = false;
                _targetPanelAnim.Play("Panel Out");
            }
        }
        
        
            
        private void SubscribeEvents()
        {
            Debug.Log("subscribe호출");
            Manager.UI.IsUIActive.Subscribe(SetControlActive);
        }

        private void UnsubscribeEvents()
        {
            Manager.UI.IsUIActive.Unsubscribe(SetControlActive);
        }

        private void SetControlActive(bool value) => IsControlActive.Value = !value;

    }
}
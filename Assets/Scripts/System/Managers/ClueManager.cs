using System;
using System.Collections;
using System.Collections.Generic;
using Content.Interactable;
using Content.UI;
using DesignPattern;
using UnityEngine;
using Utility;

namespace Managers
{
    public class ClueManager : Singleton<ClueManager>
    {
        [SerializeField] private ClueUI _clueUIPrefab;
        [SerializeField] private float _delay = 1.5f;
        
        private ClueObject _currentClue;
        
        private void Awake() => SingletonInit();

        public void ShowClueUI(ClueObject clueObject)
        {
            _currentClue = clueObject;
            
            ClueData clueData = Manager.Data.Clue.GetClueData(_currentClue.ClueId);
            
            //카메라 켜기
            _currentClue.VirtualCamera.gameObject.SetActive(true);
            
            //단서 추가해주기
            bool hasClue = Manager.Data.ClueCollection.HasClue(clueData.clueId);
            if (!hasClue)
            {
                Manager.Data.ClueCollection.CollectClue(clueData.day, clueData.clueId);
                PrintColleciton(clueData.day); //test code
            }
            
            
            //UI 켜기
            StartCoroutine(ShowClueAfterDelay(_delay, clueData));
            
           
            
            
        }

        public void CloseClueUI(ClueUI ui, Define.ClueId requestClueId)
        {
            if (requestClueId == _currentClue.ClueId)
            {
                Manager.UI.CloseUI(ui);
                _currentClue.VirtualCamera.gameObject.SetActive(false);
            }
                
            else
                Debug.Log("활성화된 단서와 일치하지 않습니다.");
        }
        
        
        IEnumerator ShowClueAfterDelay(float delay, ClueData clueData)
        {
            yield return new WaitForSeconds(delay);
            ClueUI ui = Manager.UI.ShowUI<ClueUI>(_clueUIPrefab);
            ui.SetData(clueData);
        }

        //test-----
        private void PrintColleciton(int day)
        {
            var lists = Manager.Data.ClueCollection.GetCluesByDay(day);
            foreach (var clueId in lists)
            {
                Debug.Log(clueId.ToString());
            }
        }
    }
    
    
    
    
}
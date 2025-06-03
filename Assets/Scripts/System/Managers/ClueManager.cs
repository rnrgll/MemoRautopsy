using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Content.Interactable;
using Content.UI;
using DesignPattern;
using Event;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Managers
{
    public class ClueManager : Singleton<ClueManager>
    {
        [SerializeField] private ClueUI _clueUIPrefab;
        [SerializeField] private float _delay = 1.5f;
        [SerializeField] private EventSequence _inspectComplete;
        
        
        private ClueObject _currentClue;
        private bool _isShowing = false;
        private bool isCollectAllClues = false;
        private void Awake() => SingletonInit();
        
        public void ShowClueUI(ClueObject clueObject)
        {
            if (_isShowing) return;
            _isShowing = true;
            
            
            Manager.UI.IsUIActive.Value = true;
            
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
            if(_currentClue==null) return;
            if (requestClueId == _currentClue.ClueId)
            {
                _currentClue.VirtualCamera.gameObject.SetActive(false);
                Manager.UI.CloseUI(ui);
                
                //이벤트 시퀀스 진행(단서 관련 다이얼로그)
                Manager.Event.Runner.LoadSequence(_currentClue.ClueEvent);
                
                Manager.Event.Runner.StartSequence();

                _currentClue = null;
                _isShowing = false; 
            }
            
                
            else
                Debug.Log("활성화된 단서와 일치하지 않습니다.");
            
        }

       
        private bool CollectAllClues()
        {
            int currentDay = Manager.Data.GameDay;
            var total = Manager.Data.Clue.GetClueIdsByDay(currentDay, ClueSourceType.Autopsy);
            var collected = Manager.Data.ClueCollection.GetCluesByDay(currentDay, ClueSourceType.Autopsy);
            
            foreach (var clue in total)
            {
                if (!collected.Contains(clue))
                    return false;
            }
            return true;
        }
        
        public void OnCompleteInspect()
        {
            isCollectAllClues = CollectAllClues();
            
            if (!isCollectAllClues) return;

            StartCoroutine(FinishInspect());
            
            //
            //
            // Manager.Event.Runner.LoadSequence(_inspectComplete);
            // Manager.Event.Runner.StartSequence();
            //
            
          
        }

        IEnumerator FinishInspect()
        {
            yield return new WaitForSeconds(2f);
   
            // // 완료 대사 출력 (DialogManager 등과 연동 가능)
            yield return Manager.UI.ShowDialouge(new List<string>{"...검시에서 확인할 수 있는 건 다 찾은 것 같아."}, () => StartCoroutine(RetrunToLab()));
        }

        IEnumerator RetrunToLab()
        {
            yield return new WaitForSeconds(1f);
            
            //현재 씬 언로드
            Scene currentScene = gameObject.scene; 
            AsyncOperation unload = SceneManager.UnloadSceneAsync(currentScene);
            
            // 이전 씬이 비활성화돼 있었다면, 다시 활성화
            Scene labScene = SceneManager.GetSceneByName("Lab Scene");
            if (labScene.IsValid())
            {
                SceneManager.SetActiveScene(labScene);
                Util.SetSceneObjectsActive(Define.Scene.Lab,true);
                Debug.Log("LabScene 복귀 완료");
                
                yield break;
            }
            
            
            // 씬 전환
            Manager.Scene.LoadScene(Define.Scene.Lab);
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
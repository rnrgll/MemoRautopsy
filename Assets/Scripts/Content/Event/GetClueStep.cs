using System;
using System.Collections;
using System.Collections.Generic;
using Content.Interactable;
using Content.UI;
using Managers;
using UnityEngine;
using Utility;

namespace Event
{
    [CreateAssetMenu(fileName = "GetClueStep", menuName = "Data/Event/GetClue")]
    public class GetClueStep : BaseEventStep
    {
        [SerializeField] private Define.ClueId clueId;
        
        public override void Run(EventSequenceRunner runner)
        {
            runner.StartCoroutine(ShowClueRoutine(runner));

        }

        private IEnumerator ShowClueRoutine(EventSequenceRunner runner)
        {
            
            Manager.UI.IsUIActive.Value = true;
            ClueData clueData = Manager.Data.Clue.GetClueData(clueId);
            bool hasClue = Manager.Data.ClueCollection.HasClue(clueData.clueId);
            if (!hasClue)
            {
                Manager.Data.ClueCollection.CollectClue(clueData.day, clueData.clueId);
            }
            
            //UI 켜기
            ClueUI ui = Manager.UI.ShowUI<ClueUI>("UI_CluePopUp");
            ui.SetData(clueData);
            
            bool isClosed = false;
            ui.onClose += () => { isClosed = true; };
            while (!isClosed)
                yield return null;
            
            Manager.UI.CloseUI(ui);
            Manager.UI.IsUIActive.Value = false;
        
            // 다음 스텝으로 진행
            runner.NextStep();
            
        }
        
    }
    

}
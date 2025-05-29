using System;
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

        [SerializeField] private ClueData testData;

        private void Awake() => SingletonInit();

        public void ShowClue(Define.ClueId clueId)
        {
            //todo: 데이터 찾아서 넘겨주기
            ClueData clueData = testData; 
            ClueUI ui = Manager.UI.ShowUI<ClueUI>(_clueUIPrefab);
            Debug.Log(ui.name);
            ui.SetData(clueData);
        }
    }
}
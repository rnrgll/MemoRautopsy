using System;
using System.Collections;
using System.Collections.Generic;
using Content.UI;
using Event;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes
{
    public class BadEndingScene : BaseScene
    {
        [SerializeField] private EventSequence _endingSequence;
        [SerializeField] private Button button;
        [SerializeField] private List<string> dialogueLines;

        protected override void Init()
        {
            button.onClick.AddListener(Manager.Scene.LoadTitleScene);
            base.Init();
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }

        public override void OnEnterScene()
        {
            Manager.UI.DestroySharedUI();
            base.OnEnterScene();
            
            // Manager.Event.Runner.LoadSequence(_endingSequence);
            // Manager.Event.Runner.StartSequence();
            
            StartBadEnding();
            
        }

        private void StartBadEnding()
        {
            DialogueUI ui = Manager.UI.ShowUI<DialogueUI>("UI_Dialogue"); 
            
            ui.SetData(dialogueLines, ShowButton);
            ui.PlayNarration();
        }

        private void ShowButton()
        {
            button.gameObject.SetActive(true);
        }
    }
}
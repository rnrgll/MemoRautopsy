using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Content.UI;
using Event;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Content.ETC
{
    public class EndingDialogue : MonoBehaviour
    {
        private DialogueUI _ui;
        public List<string> DialogueLine1;
        public List<string> DialogueLine2;
        public List<string> DialogueLine3;
        public List<DialogueBlock> DialogueBlocks;
        public CinemachineVirtualCamera phoneVC;
        public GameObject EndingCanvas;
        public Animator _fadePanel;
        public Button _button;
        private void Awake()
        {
            _ui = GetComponent<DialogueUI>();
            _button.onClick.AddListener(Manager.Scene.LoadTitleScene);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void StartNarration1()
        {
            _ui.SetData(DialogueLine1, () => StartCoroutine(StartPhoneRing()));   
            _ui.PlayNarration();
        }
        public void StartNarration2()
        {
            _ui.SetData(DialogueLine2, StartDialogue);   
            _ui.PlayNarration();
        }

        public void StartNarration3()
        {
            _ui.SetData(DialogueLine3, null);   
            _ui.PlayNarration();
        }


        public void StartDialogue()
        {
            _ui.SetData(DialogueBlocks, () => StartCoroutine(Ending()));   
            _ui.PlayDialogue();
        }


        private IEnumerator StartPhoneRing()
        {
            _fadePanel.Play("Panel In");
            yield return new WaitForSeconds(1f);
            yield return new WaitForSeconds(1.5f);
            _fadePanel.Play("Panel Out");
            phoneVC.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            
            StartNarration2();
        }

        private IEnumerator Ending()
        {
            yield return new WaitForSeconds(2f);
            _fadePanel.Play("Panel In");
            yield return new WaitForSeconds(1.5f);
            _fadePanel.Play("Panel Out");
            yield return new WaitForSeconds(0.5f);
            EndingCanvas.gameObject.SetActive(true);
        }
    }
}
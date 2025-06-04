using System;
using System.Collections;
using System.Collections.Generic;
using Event;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Utility;

namespace Content.UI
{
    public class DialogueUI : BaseUI
    {
        [Header("UI")]
        [SerializeField] private TMP_Text nameTxt;
        [SerializeField] private TMP_Text lineText;
        [SerializeField] private GameObject nameBox;
        
        
        [Header("Typing Control")]
        [SerializeField] private float typeSpeed = 0.04f;

        [SerializeField] private KeyCode nextKey1 = KeyCode.Space;
        
        private List<string> _dialogueLines;
        private List<DialogueBlock> _dialogueBlocks;

        private CanvasGroup cg;
        private Action OnComplete;
        private Coroutine currentRoutine;
        private bool skip = false;

        protected override void Init()
        {
            base.Init();
            cg = GetComponent<CanvasGroup>();
            _dialogueLines = new();
            OnComplete = null;
            
        }

        private void OnDisable()
        {
            OnComplete = null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(nextKey1))
            {
                skip = true;
            }
        }

        public void SetData(List<string> dialougeLines, Action onComplete)
        {
            _dialogueLines = dialougeLines;
            OnComplete = onComplete;
        }
        public void SetData(List<DialogueBlock> dialogueBlocks, Action onComplete)
        {
            _dialogueBlocks = dialogueBlocks;
            OnComplete = onComplete;
        }
        
        /// <summary>
        /// 나래이션 시작
        /// </summary>
        public void PlayNarration()
        {
            if (currentRoutine != null) StopCoroutine(currentRoutine);
            currentRoutine = StartCoroutine(RunDialogue());
        }

        public void PlayDialogue()
        {
            if (currentRoutine != null) StopCoroutine(currentRoutine);
            currentRoutine = StartCoroutine(RunDialogueBlock());
        }
        
        
        private IEnumerator RunDialogue()
        {
            if(_dialogueLines==null || _dialogueLines.Count ==0) 
                yield break;
            
            Util.UIEnable(cg);
            nameBox.SetActive(false);
            
            foreach (string line in _dialogueLines)
            {
                yield return StartCoroutine(TypeLine(line));
            }
            
            
            
            yield return new WaitForSeconds(0.1f); // 약간의 텀
            
            Util.UIDisable(cg);
            OnComplete?.Invoke();
        }
        
        private IEnumerator RunDialogueBlock()
        {
            if(_dialogueBlocks==null || _dialogueBlocks.Count ==0) 
                yield break;

            Util.UIEnable(cg);
            nameBox.SetActive(true);
            foreach (DialogueBlock block in _dialogueBlocks)
            {
                nameTxt.text = block.speakerName;

                foreach (string line in block.lines)
                {
                    yield return StartCoroutine(TypeLine(line));
                }
                
            }
            
            yield return new WaitForSeconds(0.1f); // 약간의 텀
            Util.UIDisable(cg);
            
            OnComplete?.Invoke();
        }
        

        private IEnumerator TypeLine(string line)
        {
            lineText.text = "";
            skip = false;
            for (int i = 0; i < line.Length; i++)
            {
                if (skip)
                {
                    //스킵
                    lineText.text = line;
                    break;
                }
                //
                // if (Input.GetKeyDown(nextKey1))
                // {
                //     
                // }
                
                lineText.text += line[i];
                
                yield return new WaitForSeconds(typeSpeed);
            }

            skip = false;
            yield return null; 
            
            //키 입력 대기
            yield return new WaitUntil(() => skip);
            
            //다음 프레임까지 대기해서 중복 입력 방지
            yield return null; 
            skip = false;
        }
        
    }
  
}
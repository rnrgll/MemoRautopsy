using System;
using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace Managers
{
    public class SceneManagerEx : Singleton<SceneManagerEx>
    {
        //현재 씬
        public BaseScene CurrentScene;

        //씬 전환 패널
        public List<GameObject> effectPanels = new ();
        
        [Header("Settings")]
        [Range(0, 1)] public float animationSmoothness = 0.25f;
        [Range(0.75f, 4)] public float animationSpeed = 1;

        private GameObject currentPanel;
        private Animator currentPanelAnimator;
        
        
        //Animator 
        private int PANEL_FADEIN = Animator.StringToHash("Panel In");
        private int PANEL_FADEOUT = Animator.StringToHash("Panel Out");

        private void Awake() => SingletonInit();

        //씬 이름 반환
        public string GetSceneName(Define.Scene scene)
        {
            Define.SceneNames.TryGetValue(scene, out string sceneName);
            return sceneName;
        }

        //씬 전환
        public void LoadScene(Define.Scene scene)
        {
            SceneManager.LoadScene(GetSceneName(scene));
        }

        //씬 비동기 전환
        public void AsncLoadScene(Define.Scene scene, float delay, bool useEffect = false, int loadSceneMode = 1)
        {
            StartCoroutine(CoLoadScene());
            
            
            IEnumerator CoLoadScene()
            {
                
                if (useEffect && effectPanels.Count > 0)
                {
                    yield return StartCoroutine(EffectIn(0));
                }
                
                yield return new WaitForSeconds(delay);
                
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(GetSceneName(scene), (LoadSceneMode)loadSceneMode);
                asyncOperation.allowSceneActivation = false;
                
                while (asyncOperation.progress < 0.9f)
                {
                    yield return null;
                }
                
                asyncOperation.allowSceneActivation = true;
                
                yield return null; //씬 전환 기다렸다가 effect in 처리

                if (useEffect)
                {
                    yield return StartCoroutine(EffectOut());
                }
                
            }
        }
        
        //씬 전환시 초기화 및 방지할 것들

        //씬 전환 후에 다시 활성화 시키기
        


        private IEnumerator EffectIn(int panelIndex)
        {
      
            currentPanel = effectPanels[panelIndex];
            currentPanelAnimator = currentPanel.GetComponent<Animator>();
            currentPanelAnimator.SetFloat("Anim Speed", animationSpeed);
            currentPanelAnimator.CrossFade(PANEL_FADEIN, animationSmoothness);
            
            yield return new WaitUntil(() =>
                currentPanelAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash == PANEL_FADEIN &&
                currentPanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
            );


        }

        private IEnumerator  EffectOut()
        {
            if (currentPanel == null) yield break;

            currentPanelAnimator = currentPanel.GetComponent<Animator>();
            currentPanelAnimator.SetFloat("Anim Speed", animationSpeed);
            currentPanelAnimator.CrossFade(PANEL_FADEOUT, animationSmoothness);

            // 애니메이션 완료 대기
            yield return new WaitUntil(() =>
                currentPanelAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash == PANEL_FADEOUT &&
                currentPanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
            );
            
            currentPanel = null;
        }
        
        

    }
}


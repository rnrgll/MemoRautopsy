using System;
using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Managers
{
    public class SceneManagerEx : Singleton<SceneManagerEx>
    {
        [Header("Scene Manage")]
        //현재 씬
        public BaseScene CurrentScene;
        //씬 전환 패널
        public List<GameObject> effectPanels = new ();
        private GameObject currentPanel;
        private Animator currentPanelAnimator;

        [SerializeField] private Define.Scene PlayGameScene;
        [SerializeField] private Define.Scene EndGameScene;
        
        
        [Header("Settings")]
        [Range(0, 1)] public float animationSmoothness = 0.25f;
        [Range(0.75f, 4)] public float animationSpeed = 1;
        
        
        //Animator 
        private int PANEL_FADEIN = Animator.StringToHash("Panel In");
        private int PANEL_FADEOUT = Animator.StringToHash("Panel Out");

        //----------
        private void Awake() => SingletonInit();

        #region Scene Load

          //씬 이름 반환
        public string GetSceneName(Define.Scene scene)
        {
            Define.SceneNames.TryGetValue(scene, out string sceneName);
            return sceneName;
        }

        //씬 전환
        public void LoadScene(Define.Scene scene)
        {
            //현재 씬 exit
            CurrentScene?.OnExitScene();
            
            //새 씬 로드
            SceneManager.LoadScene(GetSceneName(scene));
            
            //다음 프레임에서 OnEnter 호출
            //즉시하는 경우 새 씬의 Awake보다 빨라서 안됨
            StartCoroutine(WaitEnterNextScene());
        }

        //씬 비동기 전환
        /// <summary>
        /// 비동기 씬 전환
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="effectDelay"></param>
        /// <param name="useEffect"></param>
        /// <param name="loadSceneMode">Single(0) : Closes all current loaded Scenes and loads a Scene. / Additive(1) : Adds the Scene to the current loaded Scenes. </param>
        public void AsncLoadScene(Define.Scene scene, float effectDelay, bool useEffect = false, int loadSceneMode = 1)
        {
            StartCoroutine(CoLoadScene());
            
            
            IEnumerator CoLoadScene()
            {
                CurrentScene?.OnExitScene();
                
                if (useEffect && effectPanels.Count > 0)
                {
                    yield return StartCoroutine(EffectIn(0));
                }
                
                yield return new WaitForSeconds(effectDelay);
                
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(GetSceneName(scene), (LoadSceneMode)loadSceneMode);
                asyncOperation.allowSceneActivation = false;
                
                while (asyncOperation.progress < 0.9f)
                {
                    yield return null;
                }
                
                asyncOperation.allowSceneActivation = true;
                
                yield return null; //씬 전환 기다리기
                
                //fade 효과
                if (useEffect)
                {
                    yield return StartCoroutine(EffectOut());
                }
                
                //씬 진입 초기화
                Debug.Log(CurrentScene.SceneType.ToString());
                CurrentScene?.OnEnterScene();
                
            }
        }
        #endregion

        #region Scene Change

        public void LoadGameScene()
        {
            Manager.UI.CreateSharedUI();
            AsncLoadScene(PlayGameScene, 0.5f, true, 0);
        }
        public void LoadEndingScene()
        {
            Manager.UI.DestroySharedUI();
            AsncLoadScene(EndGameScene, 0.5f, true, 0);
        }

        #endregion

        #region Coroutine
        
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


        private IEnumerator WaitEnterNextScene()
        {
            yield return null; //다음 프레임까지 대기
            
            Debug.Log(CurrentScene.SceneType.ToString());
            CurrentScene?.OnEnterScene();
        }
        
        #endregion
    }
}


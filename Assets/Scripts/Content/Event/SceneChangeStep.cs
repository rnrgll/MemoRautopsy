using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Event
{
    [CreateAssetMenu(fileName = "SceneChangeStep", menuName = "Data/Event/SceneChangeStep")]
    public class SceneChangeStep : BaseEventStep
    {
        [SerializeField] private List<Define.Scene > scenes;
        [SerializeField] private bool isAsyncLoad;
        
        [Header("Async load setting")]
        [SerializeField] private bool effect;
        [SerializeField] private float delay;
        [SerializeField] private LoadSceneMode loadSceneMode;
        
        public override void Run(EventSequenceRunner runner)
        {
            if (!isAsyncLoad)
            {
                // 싱크 방식이면 첫 씬만 싱글/나머지는 Additive
                for (int i = 0; i < scenes.Count; i++)
                {
                    Manager.Scene.LoadScene(scenes[i]);
                }
            }
            
            else
            {
                // 비동기 방식
                runner.StartCoroutine(LoadMultipleScenes());
            }
            
           
        }
        private IEnumerator LoadMultipleScenes()
        {
            // 첫 씬만 이펙트 포함해서 로드
            bool first = true;

            foreach (var scene in scenes)
            {
                if (first)
                {
                    yield return Manager.Scene.CoLoadScene(scene, delay, effect, (int)loadSceneMode);
                    first = false;
                }
                else
                {
                    yield return Manager.Scene.CoLoadScene(scene, 0f, false, (int)loadSceneMode);
                }
                
            }

            //마지막은 fade out
            if (effect)
            {
                yield return Manager.Scene.EffectOut(0);
                Debug.Log("페이드 아웃 마지막에");
            }
        }
        
    }
}
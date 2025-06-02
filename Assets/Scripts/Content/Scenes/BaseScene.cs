using System.Collections.Generic;
using Managers;
using UnityEngine;
using Utility;

namespace Scenes
{
    public abstract class BaseScene : MonoBehaviour
    {
        public Define.SceneType SceneType { get; protected set; } = Define.SceneType.Unknown;

        [SerializeField] protected List<Define.Scene> nextScenes ;
        

        private void Awake() => Init();

        protected virtual void Init()
        {
            Debug.Log("CurrentScene 업데이트");
            Manager.Scene.CurrentScene = this; //씬 전환 후 awake때 SceneManager가 가지고 있는 정보(현재 씬) 업데이트 해주기
            OnEnterScene();
        }

        public virtual void OnEnterScene()
        {
            Debug.Log($"Onenter 호출!! : {SceneType}");
            Manager.UI.IsUIActive.Value = false; //입력 차단 해제
        }

        public virtual void OnExitScene()
        {
           if(Manager.UI.RootUI != null)
               Destroy(Manager.UI.RootUI);
            
            Manager.UI.IsUIActive.Value = true; //입력 차단 활성화
        }
    }
}
using Managers;
using UnityEngine;
using Utility;

namespace Scenes
{
    public class TitleScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();
            SceneType = Define.SceneType.Title;
        }
        
        public override void OnEnterScene()
        {
            
        }

        public override void OnExitScene()
        {
            base.OnExitScene();
            Debug.Log("타이틀 씬 종료");
        }

        public void PlayGame()
        {
            Manager.Data.GameDay = 1; //1일차부터 리셋하여 시작
            Manager.Scene.LoadGameScene();
        }
    }
}
using System.Collections.Generic;
using Player;
using UnityEngine;
using Utility;

namespace Scenes
{
    public class LabScene : BaseScene
    {
        [SerializeField] private List<Transform> _startPosition;
        [SerializeField] private PlayerController _player;
        
        
        protected override void Init()
        {
            base.SceneType = Define.SceneType.Inspect;
            base.Init();
            
           
            
        }
        public override void OnEnterScene()
        {
            base.OnEnterScene();
            SetStartPosition();
        }
        private void SetStartPosition()
        {
            //todo: 스토리 진행에 따라 스타트 포지션 변경해주기
            _player.transform.position = _startPosition[0].position;
        }

        public override void OnExitScene()
        {
            base.OnExitScene();
            //todo: clear구현
        }
    }
}
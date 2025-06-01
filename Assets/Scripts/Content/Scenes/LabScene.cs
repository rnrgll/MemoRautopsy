using System.Collections.Generic;
using Player;
using UnityEngine;
using Utility;

namespace Scenes
{
    public class LabScene : BaseScene
    {
        [SerializeField] private List<Transform> _startTransforms;
        [SerializeField] private PlayerController _player;
        
        
        protected override void Init()
        {
            base.SceneType = Define.SceneType.Main;
            base.Init();
            //test용
            OnEnterScene();
            
        }
        public override void OnEnterScene()
        {
            SetStartTransform();
            base.OnEnterScene();
        }
        private void SetStartTransform()
        {
            //todo: 스토리 진행에 따라 스타트 포지션 변경해주기
            _player.transform.position = _startTransforms[0].position;
            _player.SetInitRotation(_startTransforms[0].eulerAngles);
        }

        public override void OnExitScene()
        {
            base.OnExitScene();
            //todo: clear구현
        }
    }
}
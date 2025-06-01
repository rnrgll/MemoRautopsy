using System.Collections.Generic;
using Content.ETC;
using Event;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.Rendering;
using Utility;

namespace Scenes
{
    public class LabScene : BaseScene
    {
        [SerializeField] private List<Transform> _startTransforms;
        [SerializeField] private PlayerController _player;

        [SerializeField] private Volume _volume;

        [SerializeField] private EventSequence _eventSequence;
        
        [SerializeField] private List<VCam> virtualCams;
        
        protected override void Init()
        {
            base.SceneType = Define.SceneType.Main;
            
            //매니저에 등록
            Manager.Event.Volume = _volume;
            foreach (var pair in virtualCams)
            {
                Manager.Event.RegisterVCam(pair.key, pair.vCam);
            }
            
            
            
            base.Init();
            //test용
            //OnEnterScene();
            
        }
        public override void OnEnterScene()
        {
            SetStartTransform();
            base.OnEnterScene();
            
            Manager.Event.Runner.LoadSequence(_eventSequence);
            Manager.Event.Runner.StartSequence();
            
        }
        private void SetStartTransform()
        {
            //todo: 스토리 진행에 따라 스타트 포지션 변경해주기
            _player.transform.position = _startTransforms[0].position;
            _player.SetInitRotation(_startTransforms[0].eulerAngles);
        }

        public override void OnExitScene()
        {
            _player.SetControlActive(true);
            base.OnExitScene();
            //todo: clear구현
        }
    }
}
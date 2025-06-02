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

        private static int positionIdx = 0;
        
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
            if (positionIdx < 0 || positionIdx >= _startTransforms.Count) return;
            
            _player.transform.position = _startTransforms[positionIdx].position;
            _player.SetInitRotation(_startTransforms[positionIdx].eulerAngles);
            positionIdx++;
        }

        public override void OnExitScene()
        {
            _player.SetControlActive(true); //커서 보이게 처리
            base.OnExitScene();
            //todo: clear구현
        }
    }
}
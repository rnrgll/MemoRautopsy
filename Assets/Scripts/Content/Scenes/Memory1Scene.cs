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
    public class Memory1Scene : BaseScene
    {
        [Header("Settings")]
        [SerializeField] private List<Transform> _startTransforms;
        [SerializeField] private Volume _volume;
        [SerializeField] private List<VCam> virtualCams;
        
        [Header("Character")]
        [SerializeField] private PlayerController _player;

        [SerializeField] private Animator _ElliotGray;
        [SerializeField] private Animator _DanielBlake;

        [Header("Evnet Sequence")]
        [SerializeField] private EventSequence _eventSequence;
        
        
        private static int positionIdx = 0;
        
        
        protected override void Init()
        {
            base.SceneType = Define.SceneType.Memory;
            
            //매니저에 등록
            Manager.Event.Volume = _volume;
            foreach (var pair in virtualCams)
            {
                Manager.Event.RegisterVCam(pair.key, pair.vCam);
            }
            
            base.Init();
        }
        
        public override void OnEnterScene()
        {
            base.OnEnterScene();
            SetStartTransform();
            //1. 엘리엇그레이/다니엘블레이크 애니메이션 재생
            _ElliotGray.Play("Petting");
            _DanielBlake.SetBool("IsHolding", true);
            //2. 도입부 나래이션
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
        
        
    }
}
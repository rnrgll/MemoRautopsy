using System.Collections.Generic;
using Content.ETC;
using Event;
using Managers;
using Player;
using UnityEngine;
using Utility;

namespace Scenes
{
    public class Memory3_4Scene : BaseScene
    {
        [Header("Settings")] [SerializeField] private List<VCam> virtualCams;
        [SerializeField] private Animator _LOGAN;

        [Header("Evnet Sequence")] [SerializeField]
        private EventSequence _eventSequence;


        protected override void Init()
        {
            base.SceneType = Define.SceneType.Memory;


            foreach (var pair in virtualCams)
            {
                Manager.Event.RegisterVCam(pair.key, pair.vCam);
            }

            base.Init();
            
            StartSequence();
        }


        public void StartSequence()
        {
            _LOGAN.Play("Typing");

            Manager.Event.Runner.LoadSequence(_eventSequence);
            Manager.Event.Runner.StartSequence();
        }
    }
}
    

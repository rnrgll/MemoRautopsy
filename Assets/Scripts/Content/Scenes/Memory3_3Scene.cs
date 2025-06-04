using System.Collections.Generic;
using Content.ETC;
using Event;
using Managers;
using UnityEngine;
using Utility;

namespace Scenes
{
    public class Memory3_3Scene : BaseScene
    {
        [Header("Settings")] [SerializeField] private List<VCam> virtualCams;
        [SerializeField] private Animator _LOGAN;

        [Header("Evnet Sequence")] [SerializeField]
        private List<EventSequence> _eventSequence;


        protected override void Init()
        {
            base.SceneType = Define.SceneType.Memory;


            foreach (var pair in virtualCams)
            {
                Manager.Event.RegisterVCam(pair.key, pair.vCam);
            }

            base.Init();
            
            StartSequence(0);
        }


        public void StartSequence(int n)
        {
            //_LOGAN.Play("Typing");

            Manager.Event.Runner.LoadSequence(_eventSequence[n]);
            Manager.Event.Runner.StartSequence();
        }
    }
    }

using System;
using System.Collections.Generic;
using Content.ETC;
using Content.Interactable;
using Event;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.Rendering;
using Utility;

namespace Scenes
{
    [Serializable]
    public class DayObject
    {
        public GameObject BodyBag;
        public GameObject DocsTray;
        public GameObject EmptyTray;

        public void SetActiveAll(bool isActive)
        {
            BodyBag.SetActive(isActive);
            DocsTray.SetActive(isActive);
            EmptyTray.SetActive(isActive);
        }
    }
    
    public class LabScene : BaseScene
    {
        [SerializeField] private List<Transform> _startTransforms;
        [SerializeField] private PlayerController _player;

        [SerializeField] private Volume _volume;

        [SerializeField] private EventSequence _eventSequence;
        
        [SerializeField] private List<VCam> virtualCams;

        
        //-------- day1, 2, 3 시체, 서류더미 활성화 onoff
        public List<DayObject> DayObjects;
        //public InteractObject Day1Intro;

        public InteractObject Day1Brain;
        public InteractObject Day1Outtro;
        public InteractObject Day2Intro;
        
        public InteractObject Day2Brain;
        public InteractObject Day2Return;
        
        public InteractObject Day2Outtro;
        public InteractObject Day3Intro;
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
            
            //day에 따라 오브젝트 활성화
            int curday = Manager.Data.GameDay;
            for (int i = 0; i < DayObjects.Count; i++)
            {
                if (i == curday - 1)
                {
                    DayObjects[i].SetActiveAll(true);
                }
                else
                {
                    DayObjects[i].SetActiveAll(false);
                }
            }

            // if (!Manager.Data.IsCompleted(Day1Intro.interactionId))
            // {
            //     Debug.Log("day1 인트로 활성화합니다.");
            //     Day1Intro.EnableInteraction();
            // }
            
            //Manager.Event.Runner.LoadSequence(_eventSequence);
            //Manager.Event.Runner.StartSequence();
            
        }
        private void SetStartTransform()
        {
            _player.transform.position = _startTransforms[Manager.Data.GameDay-1].position;
            _player.SetInitRotation(_startTransforms[Manager.Data.GameDay-1].eulerAngles);
        }

        public override void OnExitScene()
        {
            _player.SetControlActive(true); //커서 보이게 처리
            base.OnExitScene();
            
            
        }

        private void ActivateTriggerEvent()
        {
            switch (Manager.Data.GameDay)
            {
                case 1:
                    if(Manager.Data.IsCompleted(Day1Brain.interactionId)&&!Manager.Data.IsCompleted(Day1Outtro.interactionId))
                        Day1Outtro.EnableInteraction();
                    break;
                case 2:
                    if(Manager.Data.IsCompleted(Day1Outtro.interactionId)&&!Manager.Data.IsCompleted(Day2Intro.interactionId))
                            Day2Outtro.EnableInteraction();
                    if(Manager.Data.IsCompleted(Day2Brain .interactionId)&&!Manager.Data.IsCompleted(Day2Return.interactionId))
                        Day2Return.EnableInteraction();
                    break;
                case 3:
                    if(Manager.Data.IsCompleted(Day2Intro.interactionId)&&!Manager.Data.IsCompleted(Day3Intro.interactionId))
                        Day3Intro.EnableInteraction();
                    break;
            }
        }
    }
}
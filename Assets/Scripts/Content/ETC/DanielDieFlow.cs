using System;
using Event;
using Managers;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Content.ETC
{
    public class DanielDieFlow : MonoBehaviour
    {
        [SerializeField] private PlayableDirector director;
        [SerializeField] private PlayableAsset timeline;
        
        [SerializeField] private EventSequence es;
        [SerializeField] private GameObject unknowCharacter;
            
        private void OnEnable()
        {
            director.playableAsset = timeline;
            director.Play();
            
            StartFlow();
        }

        private void StartFlow()
        {
            Manager.Event.Runner.LoadSequence(es);
            Manager.Event.Runner.StartSequence();
        }
    }
}
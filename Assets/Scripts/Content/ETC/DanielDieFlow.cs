using System;
using System.Collections;
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
        [SerializeField] private GameObject Daniel;
        
        [SerializeField] private GameObject unknowCharacter;
            
        private void OnEnable()
        {
            Daniel.SetActive(false);
            StartFlow();
        }

        private void StartFlow()
        {
            StartCoroutine(FlowRoutine());
            
        }

        private IEnumerator FlowRoutine()
        {
            
            yield return new WaitForSeconds(1.5f);
            
            director.playableAsset = timeline;
            director.Play();
            
            Manager.Event.Runner.LoadSequence(es);
            Manager.Event.Runner.StartSequence();
        }
    }
}
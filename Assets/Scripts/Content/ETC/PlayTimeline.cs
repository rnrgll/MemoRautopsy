using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Content.ETC
{
    public class PlayTimeline : MonoBehaviour
    {
        [SerializeField] private bool playOnAwake;
        [SerializeField] private PlayableDirector playableDirector;
        [SerializeField] private PlayableAsset playableAsset;
        
        private void Start()
        {
            playableDirector.Play(playableAsset);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Event;
using DesignPattern;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Managers
{
    public class EventSystemManager : Singleton<EventSystemManager>
    {
        public EventSequenceRunner Runner;
        public Volume Volume;

        private ChromaticAberration _chromaticAberration;
        
        private Dictionary<string, CinemachineVirtualCamera> _vCams = new();
        
        private void Awake()
        {
            SingletonInit();
        }

        public void SetChromatic(bool enable)
        {
            if (Volume.profile.TryGet(out ChromaticAberration chroma))
            {
                _chromaticAberration = chroma;
            }


            if (_chromaticAberration != null)
            {
                StopAllCoroutines(); // 이전 코루틴 중복 방지
                StartCoroutine(FadeChromatic(enable));
            }
        }
        
        public void RegisterVCam(string key, CinemachineVirtualCamera cam)
        {
            // if (!_vCams.ContainsKey(key))
            // {
            //     _vCams.Add(key, cam);
            // }
            _vCams[key] = cam; // 덮어쓰기 가능!
        }
        
        public CinemachineVirtualCamera GetVCam(string key)
        {
            _vCams.TryGetValue(key, out var cam);
            return cam;
        }

        private IEnumerator FadeChromatic(bool enable)
        {
            float duration = 0.3f;
            float t = 0f;

            float start = _chromaticAberration.intensity.value;
            float target = enable ? 1f : 0f;

            _chromaticAberration.active = true;

            while (t < duration)
            {
                t += Time.deltaTime;
                float lerp = Mathf.Lerp(start, target, t / duration);
                _chromaticAberration.intensity.value = lerp;
                yield return null;
            }
            
            _chromaticAberration.intensity.value = target;
            
            if (!enable)
                _chromaticAberration.active = false;
        }
            

        
        
        
        
        
        
    }
    
    

}
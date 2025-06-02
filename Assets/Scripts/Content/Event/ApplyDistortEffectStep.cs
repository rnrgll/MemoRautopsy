using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Event
{
    [CreateAssetMenu(fileName = "ApplyDistortEffect", menuName = "Data/Event/DistortEffectOn", order = 10)]
    public class ApplyDistortEffectStep : BaseEventStep
    {
        [SerializeField] private float duration = 2f;
        [SerializeField] private float maxIntensity = -0.4f;
        [SerializeField] private Vector2 centerXRange = new Vector2(0.3f, 0.7f);
        
        
        public override void Run(EventSequenceRunner runner)
        {
            runner.StartCoroutine(ApplyEffect(runner));
        }

        private IEnumerator ApplyEffect(EventSequenceRunner runner)
        {
            Manager.Event.Volume.profile.TryGet(out LensDistortion lens);

            if (lens == null)
            {
                runner.NextStep();
                yield break;
            }
            
            float elapsed = 0f;
            while (elapsed < duration)
            {
                float t = elapsed / duration;
                lens.intensity.value = Mathf.Lerp(0f, maxIntensity, t);
                lens.center.value = new Vector2(Mathf.Lerp(0.5f, Mathf.Lerp(centerXRange.x, centerXRange.y, t), t), 0.5f);

                elapsed += Time.deltaTime;
                yield return null;
            }

            lens.intensity.value = maxIntensity;
            lens.center.value = new Vector2(centerXRange.y, 0.5f);

            runner.NextStep();
        }
        
    }
}
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Event
{
    [CreateAssetMenu(fileName = "ResetDistortEffect", menuName = "Data/Event/DistortEffectOff", order = 10)]
    public class ResetDistortEffectStep : BaseEventStep
    {
        [SerializeField] private float duration = 1f;
        [SerializeField] private Vector2 fromCenterXRange = new Vector2(0.3f, 0.7f);
        [SerializeField] private float fromIntensity = -0.4f;
        
        
        public override void Run(EventSequenceRunner runner)
        {
            runner.StartCoroutine(ResetRoutine(runner));
        }

        private IEnumerator ResetRoutine(EventSequenceRunner runner)
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
                lens.intensity.value = Mathf.Lerp(fromIntensity, 0f, t);
                lens.center.value = new Vector2(Mathf.Lerp(fromCenterXRange.y, 0.5f, t), 0.5f);

                elapsed += Time.deltaTime;
                yield return null;
            }

            lens.intensity.value = 0f;
            lens.center.value = new Vector2(0.5f, 0.5f);
            runner.NextStep();
        }
        
    }
}
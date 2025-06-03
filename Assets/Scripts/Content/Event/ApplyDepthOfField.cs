using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Event
{
    [CreateAssetMenu(fileName = "ApplyDepthfOfField", menuName = "Data/Event/DepthOfField", order = 10)]
    public class ApplyDepthOfField : BaseEventStep
    {
        [SerializeField] private float duration = 2f;
        [SerializeField] private float focalLength = 52;
        public override void Run(EventSequenceRunner runner)
        {
            runner.StartCoroutine(ApplyEffect(runner));
        }

        private IEnumerator ApplyEffect(EventSequenceRunner runner)
        {
            Manager.Event.Volume.profile.TryGet(out DepthOfField depth);

            if (depth == null)
            {
                
                    runner.NextStep();
                    yield break;
            }
            
            float elapsed = 0f;
            while (elapsed < duration)
            {
                float t = elapsed / duration;
                depth.focalLength.value = Mathf.Lerp(0f, focalLength, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            depth.focalLength.value = focalLength;
            
            runner.NextStep();
        }
        }
    }

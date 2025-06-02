using System.Collections;
using Managers;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "WaitStep", menuName = "Data/Event/WaitStep")]
    public class WaitStep : BaseEventStep
    {
        [SerializeField] private float duration = 1f;

        public override void Run(EventSequenceRunner runner)
        {
            runner.StartCoroutine(WaitCoroutine(runner));
        }

        private IEnumerator WaitCoroutine(EventSequenceRunner runner)
        {
            Manager.UI.IsUIActive.Value = true;
            
            yield return new WaitForSeconds(duration);

            Manager.UI.IsUIActive.Value = false;
            runner.NextStep();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "ChromaticEffectStep", menuName = "Data/Event/ChromaticEffect", order = 0)]
    public class ChromaticEffectStep : BaseEventStep
    {
        [SerializeField] private float duration = 0.2f;

        public override void Run(EventSequenceRunner runner)
        {
            Manager.UI.IsUIActive.Value = true;
            Manager.Event.SetChromatic(true);
           runner.StartCoroutine(ChromaticRoutine(runner));

        }
        
        private IEnumerator ChromaticRoutine(EventSequenceRunner runner)
        {
            yield return new WaitForSeconds(duration);
            Manager.Event.SetChromatic(false);
            Manager.UI.IsUIActive.Value = false;
            runner.NextStep();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "ChromaticEffectStep", menuName = "Data/Event/ChromaticEffect", order = 10)]
    public class ChromaticEffectStep : BaseEventStep
    {
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private bool keepEffectOn = false;
        
        public override void Run(EventSequenceRunner runner)
        {
            Manager.UI.IsUIActive.Value = true;
            Manager.Event.SetChromatic(true);
            if(keepEffectOn == false)
                runner.StartCoroutine(ChromaticRoutine(runner));
            
            else
            {
                Manager.UI.IsUIActive.Value = false;
                runner.NextStep();
            }
            
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
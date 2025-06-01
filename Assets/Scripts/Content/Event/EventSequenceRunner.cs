using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public class EventSequenceRunner
    {
        private EventSequence currentSequence;
        private int currentIdx = 0;

        public void LoadSequence(EventSequence seq)
        {
            currentSequence = seq;
            currentIdx = 0;
        }


    public void StartSequence()
    {
        if (currentSequence == null || currentSequence.Steps.Count == 0) return;
            RunCurrentStep();
        }

        private void RunCurrentStep()
        {
            if (currentIdx >= currentSequence.Steps.Count) return;

            currentSequence.Steps[currentIdx].Run(this); //runner를 전달해서 nextstep을 호출할 수 있도록 한다.
        }

        public void NextStep()
        {
            currentIdx++;
            RunCurrentStep();
        }
    }
}
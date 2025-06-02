using Managers;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(menuName = "Data/Event/InspectComplete")]
    public class InspectCompleteStep :  BaseEventStep
    {
        public override void Run(EventSequenceRunner runner)
        {
            ClueManager.Instance.OnCompleteInspect();
            runner.NextStep(); // 바로 다음으로 넘어가기
        }
    }
}
using Managers;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "ChoiceEvent", menuName = "Data/Event/ChoiceEvent", order = 1)]
    public class ChoiceEventStep : BaseEventStep
    {
        [SerializeField] private string question;
        [SerializeField] private string optionA;
        [SerializeField] private string optionB;
        
        [SerializeField] private EventSequence nextIfA;
        [SerializeField] private EventSequence nextIfB;
        
        public override void Run(EventSequenceRunner runner)
        {
            Manager.UI.ShowChoice(question,
                optionA,
                optionB,
                (choice) =>
                {
                    if (choice == 0)
                        runner.LoadSequence(nextIfA);
                    else
                        runner.LoadSequence(nextIfB);

                    runner.StartSequence();
                });
        }
    }
}
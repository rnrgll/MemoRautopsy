using System;
using Managers;
using UnityEngine;
using UnityEngine.Events;

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

        public UnityEvent OnClickOptionA;
        public UnityEvent OnClickOptionB;
        
        
        public override void Run(EventSequenceRunner runner)
        {
            
            Manager.UI.ShowChoice(question,
                optionA,
                optionB,
                (choice) =>
                {
                    if (choice == 0)
                    {
                        OnClickOptionA?.Invoke();
                        runner.LoadSequence(nextIfA);
                    }
                    else
                    {
                        OnClickOptionB?.Invoke();
                        runner.LoadSequence(nextIfB);
                    }
                 

                    runner.StartSequence();
                });
        }
    }
}
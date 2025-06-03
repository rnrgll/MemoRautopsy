using Managers;
using UnityEngine;
using Utility;

namespace Event
{
    
    [CreateAssetMenu(fileName = "DayOuttroStep", menuName = "Data/Event/DayOuttro", order = 0)]
    public class DayOuttroStep : BaseEventStep
    {
        [SerializeField] private float duration = 0.5f;
        
        
        public override void Run(EventSequenceRunner runner)
        {
            Manager.Data.GameDay++;
            
            Manager.Scene.AsncLoadScene(Define.Scene.Lab, 1f,true,0);
            
            //runner.NextStep();
        }
    }
}
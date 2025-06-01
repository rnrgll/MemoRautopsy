using Content.UI;
using Managers;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "DayIntroStep", menuName = "Data/Event/DayIntro", order = 0)]
    public class DayIntroStep : BaseEventStep
    {
        [SerializeField] private DayIntroUI dayIntroUI;
        
        //[SerializeField] private float waitTime = 1.5f;
        [SerializeField] private float duration = 0.5f;
        
        [SerializeField] private string infoMsg;

        public override void Run(EventSequenceRunner runner)
        {
            DayIntroUI ui = Manager.UI.ShowUI<DayIntroUI>(dayIntroUI);
            ui.SetData(infoMsg, duration, () => runner.NextStep());
            ui.StartIntro();
        }
    }
}
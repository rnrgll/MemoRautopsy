using System.Collections;
using Content.UI;
using Managers;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "ShowUIStep", menuName = "Data/Event/ShowImage")]
    public class ShowImageStep : BaseEventStep
    {
        public Sprite sprite;
        public override void Run(EventSequenceRunner runner)
        {
            runner.StartCoroutine(ShowUIRoutine(runner));

        }

        private IEnumerator ShowUIRoutine(EventSequenceRunner runner)
        {
            var ui = Manager.UI.ShowUI<ImageUI>("UI_ImagePopUp");
            ui.SetData(sprite);
            
            bool isClosed = false;
            ui.onClose += () => { isClosed = true; };
            while (!isClosed)
                yield return null;
            
            Manager.UI.CloseUI(ui);
            runner.NextStep();
        }
    }
}
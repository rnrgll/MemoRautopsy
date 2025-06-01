using Cinemachine;
using Managers;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "VirtualCamTransition", menuName = "Data/Event/VitualCamTrans", order = 1)]
    public class VirtualCamTransitionStep : BaseEventStep
    {
        [SerializeField] private string vCamKey;
        [SerializeField] private bool setActive;
        
        
        public override void Run(EventSequenceRunner runner)
        {
            var cam = Manager.Event.GetVCam(vCamKey);
            if (cam != null)
                cam.gameObject.SetActive(setActive);
            else
                Debug.LogWarning($"Virtual camera with key '{vCamKey}' not found.");

            runner.NextStep();
        }
    }
}
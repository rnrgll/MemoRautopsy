using Cinemachine;
using Managers;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "CameraImpluse", menuName = "Data/Event/Impulse", order = 1)]
    public class CameraImpulse : BaseEventStep
    {
        [SerializeField] private string vCamKey;
        private CinemachineImpulseSource impulseSrc;

        public override void Run(EventSequenceRunner runner)
        {
            var cam = Manager.Event.GetVCam(vCamKey);
            impulseSrc = cam.GetComponent<CinemachineImpulseSource>();
            
            impulseSrc.GenerateImpulse();
        }
    }
}
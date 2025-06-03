using System.Collections;
using Cinemachine;
using Managers;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "VirtualCamTransition", menuName = "Data/Event/VitualCamTrans", order = 1)]
    public class VirtualCamTransitionStep : BaseEventStep
    {
        [SerializeField] private string vCamKey;
        [SerializeField] private bool controlSpeed = false;
        [SerializeField] private float speed;
        [SerializeField] private bool setActive;

        private float originSpeed;

        public override void Run(EventSequenceRunner runner)
        {
            runner.StartCoroutine(SwitchCamCoroutine(runner));
        }


        private IEnumerator SwitchCamCoroutine(EventSequenceRunner runner)
        {
            Camera main = Camera.main;
            CinemachineBrain cb = main.GetComponent<CinemachineBrain>();

            originSpeed = cb.m_DefaultBlend.m_Time;

            if (controlSpeed)
                cb.m_DefaultBlend.m_Time = speed;

            var cam = Manager.Event.GetVCam(vCamKey);
            if (cam != null)
                cam.gameObject.SetActive(setActive);
            else
                Debug.LogWarning($"Virtual camera with key '{vCamKey}' not found.");

            // blending이 끝날 때까지 대기
            yield return new WaitUntil(() => !cb.IsBlending);

            cb.m_DefaultBlend.m_Time = originSpeed;
            runner.NextStep();
        }
    }
}
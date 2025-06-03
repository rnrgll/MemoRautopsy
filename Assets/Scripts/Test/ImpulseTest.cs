using Cinemachine;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Test
{
    public class ImpulseTest : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cm;
        public void Impulse()
        {
            cm.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }
}
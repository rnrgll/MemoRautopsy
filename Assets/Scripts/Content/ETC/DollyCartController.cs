using UnityEngine;
using Cinemachine;

public class DollyCartAutoSwitch : MonoBehaviour
{
    [Header("Dolly Cart")]
    public CinemachineDollyCart dollyCart;

    [Header("Virtual Cameras")]
    public GameObject virtualCam1;
    public GameObject virtualCam2;

    [Header("도달 판단값 (0.99~1.0)")]
    [Range(0.9f, 1.0f)]
    public float arrivalThreshold = 0.99f;

    private bool switched = false;

    public void StartMove(float speed)
    {
        dollyCart.m_Speed = speed;
    }
    
    void Update()
    {
        if (!switched && dollyCart.m_Position >= arrivalThreshold)
        {
            // 한 번만 처리
            switched = true;

            // 멈춤
            dollyCart.m_Speed = 0f;

            // 가상카메라 전환
            if (virtualCam1 != null) virtualCam1.SetActive(false);
            if (virtualCam2 != null) virtualCam2.SetActive(true);
        }
    }
}
using System;
using UnityEngine;

namespace Content.ETC
{
    public class ArriveMailTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject mailImage;
        
        private void OnEnable()
        {
            mailImage.SetActive(true);
        }
    }
}
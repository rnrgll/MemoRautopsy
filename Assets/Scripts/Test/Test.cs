using System;
using Player;
using UnityEngine;

namespace Test
{
    public class Test : MonoBehaviour
    {
        public PlayerStatus _Status;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _Status.IsInvestigating.Value = !_Status.IsInvestigating.Value;
                Debug.Log($"isinvestigating : {_Status.IsInvestigating.Value}");
                Debug.Log($"iscontrolactive : {_Status.IsControlActive.Value}");

            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _Status.IsControlActive.Value = !_Status.IsControlActive.Value;
                Debug.Log($"iscontrolactive : {_Status.IsControlActive.Value}");
    
            }
        }
    }
}
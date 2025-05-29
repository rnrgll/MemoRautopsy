using System;
using Player;
using UnityEngine;
using Utility;
using Managers;

namespace Test
{
    public class Test : MonoBehaviour
    {
        public PlayerStatus _Status;
        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     _Status.IsInvestigating.Value = !_Status.IsInvestigating.Value;
            //     Debug.Log($"isinvestigating : {_Status.IsInvestigating.Value}");
            //     Debug.Log($"iscontrolactive : {_Status.IsControlActive.Value}");
            //
            // }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
               Manager.Scene.AsncLoadScene(Define.Scene.Test2,2f,true,1);
               
    
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Manager.Scene.AsncLoadScene(Define.Scene.Test3,2f,true,0);
               
    
            }
            
        }
    }
}
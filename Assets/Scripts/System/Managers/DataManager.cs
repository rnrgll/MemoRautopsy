using System;
using System.Data;
using DesignPattern;
using UnityEngine;

namespace Managers
{
    public class DataManager : Singleton<DataManager>
    {
        public ClueDatabase Clue = new();

        private void Awake() => Init();

        private void Init()
        {
            SingletonInit();

            Clue.Init();
        }
        
    }
}
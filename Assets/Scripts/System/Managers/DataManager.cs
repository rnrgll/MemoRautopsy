using System;
using System.Collections.Generic;
using System.Data;
using DesignPattern;
using Event;
using UnityEditor;
using UnityEngine;
using Utility;

namespace Managers
{
    public class DataManager : Singleton<DataManager>
    {
        public int GameDay = 1;
        public ClueDatabase Clue = new();
        public ClueCollection ClueCollection = new();
        
        private void Awake() => Init();

        private void Init()
        {
            SingletonInit();

            Clue.Init();
            ClueCollection.Init();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using Content.Interactable;
using DesignPattern;
using Event;
using UnityEditor;
using UnityEngine;
using Utility;

namespace Managers
{
    [Serializable]
    public class InteractionObj
    {
        public string interactionId;
        public bool isDone;
    }
    public class DataManager : Singleton<DataManager>
    {
        public int GameDay = 1;
        public ClueDatabase Clue = new();
        public ClueCollection ClueCollection = new();
        
        [SerializeField] private List<InteractionObj> interactionObjs;
        private Dictionary<string, bool> interactionDone = new();
        
        
        private void Awake() => Init();

        private void Init()
        {
            
            SingletonInit();

            Clue.Init();
            ClueCollection.Init();
            
            interactionDone.Clear();
            foreach (var obj in interactionObjs)
            {
                interactionDone[obj.interactionId] = obj.isDone;
            }
            
        }
        
        
        public void SetCompleted(string id) => interactionDone[id] = true;
        public bool IsCompleted(string id) => interactionDone.ContainsKey(id) && interactionDone[id];
        
        public string GetNextInteractionId(List<string> sequence)
        {
            foreach (var id in sequence)
            {
                if (!IsCompleted(id)) return id;
            }
            return null;
        }
        
        
    }
}
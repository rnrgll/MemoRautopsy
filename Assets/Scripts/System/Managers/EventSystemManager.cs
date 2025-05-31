using System;
using System.Collections.Generic;

using Event;
using DesignPattern;
using UnityEngine;

namespace Managers
{
    public class EventSystemManager : Singleton<EventSystemManager>
    {
        public EventSequenceRunner Runner;
        private void Awake()
        {
            SingletonInit();
            Runner = new EventSequenceRunner();
        }
        
        
        
        
        
        
    }
    
    

}
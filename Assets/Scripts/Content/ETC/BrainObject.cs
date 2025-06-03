using System;
using Event;
using UnityEngine;

namespace Content.Interactable
{
    public class BrainObject : MonoBehaviour
    {
       [SerializeField] private ChoiceEventStep choiceStep;

       private void OnEnable()
       {
           choiceStep.OnClickOptionA.AddListener(AutoDestroy); 
       }

       private void OnDisable()
       {
           choiceStep.OnClickOptionB.AddListener(AutoDestroy);
       }

       private void AutoDestroy()
        {
            Destroy(gameObject);
        }
    }
}
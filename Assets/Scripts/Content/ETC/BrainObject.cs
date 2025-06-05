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
           choiceStep.OnClickOptionA.AddListener(AutoInvisible); 
       }

       private void OnDisable()
       {
           choiceStep.OnClickOptionB.AddListener(AutoInvisible);
       }

       private void AutoInvisible()
        {
            gameObject.SetActive(false);
        }
    }
}
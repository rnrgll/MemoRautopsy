using System;
using System.Collections;
using System.Collections.Generic;
using System.Interfaces;
using Cinemachine;
using Content.Interactable;
using Content.UI;
using Managers;
using UnityEngine;
using Utility;

public class ClueObject : MonoBehaviour, IInteractable
{
    [field: SerializeField] public Define.ClueId ClueId { get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera VirtualCamera { get; private set; }
    
    [SerializeField] private string interactText;
   
    private Define.Scene connectScene;

    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        
    }
    
    
    Define.Scene IInteractable.ConnectScene
    {
        get => connectScene;
        set => connectScene = value;
    }

    public void Interact()
    {
        Debug.Log($"{ClueId} 발견");
        ClueManager.Instance.ShowClueUI(this);
        
    }
    
}

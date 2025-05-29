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
    [SerializeField] private Define.ClueId _clueID;
    [SerializeField] private float _delay = 1.5f;
    
    
    [SerializeField] private string interactText;
    [SerializeField] private CinemachineVirtualCamera _virtualcam;
    private Define.Scene connectScene;

    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string IInteractable.InteractText
    {
        get => interactText;
        set => interactText = value;
    }

    Define.Scene IInteractable.ConnectScene
    {
        get => connectScene;
        set => connectScene = value;
    }

    public void Interact()
    {
        Debug.Log($"{_clueID} 발견");
        _virtualcam.gameObject.SetActive(true);

        StartCoroutine(ShowClueAfterDelay(_delay));
        //todo: cluemanager

    }

    IEnumerator ShowClueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClueManager.Instance.ShowClue(_clueID);
    }
}

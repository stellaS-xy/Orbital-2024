using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*
public class NPCController : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    public void Interact() 
    {
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
    }
}
*/ 

public class NPCController : MonoBehaviour
{
    [TextArea(1,3)]
    public string[] lines;
    [SerializeField] public bool hasName;
    public Image image;


public void Interact() 
    {
        Debug.Log("NPC has interacted");
        StartCoroutine(DialogueManager.Instance.ShowDialogue(lines, hasName));
    }

}
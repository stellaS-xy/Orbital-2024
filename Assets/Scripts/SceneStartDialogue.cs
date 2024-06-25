using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneStartDialogue : MonoBehaviour
{
    [TextArea(1, 3)]
    public string[] startDialogueLines;
    public bool hasName;

    private void Start()
    {
        if (DialogueManager.Instance != null)
        {
            StartCoroutine(DialogueManager.Instance.ShowDialogue(startDialogueLines, hasName));
        }
        else
        {
            Debug.LogError("DialogueManager instance not found. Ensure you have a DialogueManager in the scene.");
        }
    }
}

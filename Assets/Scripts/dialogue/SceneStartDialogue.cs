using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SceneStartDialogue : MonoBehaviour
{
    [TextArea(1, 3)]
    public string[] startDialogueLines;
    public bool hasName;
    public bool hasDecision;
  
    public string[] choiceContents; // Contents for choices
    public string[] option1Dialogue;
    public string[] option2Dialogue;
    public string[] option3Dialogue;
    public string requiredKeyForOption2;
    public string requiredKeyForOption3;

    public GameObject gifPlayerObject;
    public gifPlayer gifPlayer;
    public GameObject dialogueBox;
    public GameObject instructionBox;

   




    private void Start()
    {
        if (DialogueManager.Instance != null)
        {
            StartCoroutine(DialogueManager.Instance.ShowDialogue(startDialogueLines, hasName));
        }
        if (hasDecision)
        {
            DialogueManager.Instance.OnDialogueEnded += ShowDecision;
        }
        else
        {
            Debug.LogError("DialogueManager instance not found. Ensure you have a DialogueManager in the scene.");
        }

        if (gifPlayerObject != null)
        {
            gifPlayerObject.SetActive(false);
        }
    }

    private void ShowDecision()
    {
        Debug.Log("SceneStartDialogue - ShowDecision has been called");
        DialogueManager.Instance.OnDialogueEnded -= ShowDecision;

        if (DecisionManager.Instance == null)
        {
            Debug.LogError("DecisionManager instance not found. Ensure you have a DecisionManager in the scene.");
            return;
        }

        Action[] actions = new Action[choiceContents.Length];
        for (int i = 0; i < choiceContents.Length; i++)
        {
            switch (i)
            {
                case 0:
                    actions[i] = OnOption1Selected;
                    break;
                case 1:
                    if (DecisionManager.Instance.HasKey(requiredKeyForOption2))
                    {
                        actions[i] = OnOption2Selected;
                    }
                    else
                    {
                        actions[i] = null;
                    }
                    break;
                case 2:
                    if (DecisionManager.Instance.HasKey(requiredKeyForOption3))
                    {
                        actions[i] = OnOption3Selected;
                    }
                    else
                    {
                        actions[i] = null;
                    }
                    break;
            }
        }

        DecisionManager.Instance.ShowDecision(choiceContents, actions);
    }

    private void OnOption1Selected()
    {
        Debug.Log("Option 1 selected");
        if (option1Dialogue != null && option1Dialogue.Length > 0)
        {
            StartCoroutine(HandleOption1Dialogue());
        }
    }

    private IEnumerator HandleOption1Dialogue()
    {
        Debug.Log("HandleOption1Dialogue being called");
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(option1Dialogue, false));
        Debug.Log("DialogueManager handled option1 dialogue");

        yield return StartCoroutine(WaitForDialogueToEnd());

        Debug.Log("Starting GIF playback");
        gifPlayerObject.SetActive(true);
        gifPlayer.enabled = true;
        yield return StartCoroutine(PlayGIF());
        Debug.Log("PlayGIF is called");

        // After the GIF is played, mark option 1 as completed and show the decision UI again
        DecisionManager.Instance.CompleteOption1();
        ShowDecision();
    }

    private IEnumerator WaitForDialogueToEnd()
    {
        Debug.Log("Waiting for dialogue to end...");
        while (dialogueBox.activeInHierarchy)
        {
            yield return null;
        }
        Debug.Log("Dialogue ended");
    }

    private IEnumerator PlayGIF()
    {
        Debug.Log("Playing GIF...");
        instructionBox.SetActive(false);

        // Wait until the GIF has finished playing
        while (!gifPlayer.IsAnimationFinished())
        {
            yield return null;
        }

        gifPlayerObject.SetActive(false); // Disable the GIF player
        Debug.Log("GIF finished.");
        instructionBox.SetActive(true);
    }

    private void OnOption2Selected()
    {
        Debug.Log("Option 2 selected");
        if (DecisionManager.Instance.HasKey(requiredKeyForOption2))
        {
            if (option2Dialogue != null && option2Dialogue.Length > 0)
            {
                StartCoroutine(HandleOption2Dialogue());
            }
        }
        else
        {
            Debug.Log("Option 2 is not available yet.");
        }
    }

    private IEnumerator HandleOption2Dialogue()
    {
        Debug.Log("HandleOption2Dialogue being called");
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(option2Dialogue, false));
        Debug.Log("DialogueManager handled option2 dialogue");

        // Transition to the next scene in sequence
        SceneTransitionManager.Instance.TransitionToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnOption3Selected()
    {
        Debug.Log("Option 3 selected");
        if (DecisionManager.Instance.HasKey(requiredKeyForOption3))
        {
            if (option3Dialogue != null && option3Dialogue.Length > 0)
            {
                StartCoroutine(HandleOption3Dialogue());
            }
        }
        else
        {
            Debug.Log("Option 3 is not available yet.");
        }
    }

    private IEnumerator HandleOption3Dialogue()
    {
        Debug.Log("HandleOption3Dialogue being called");
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(option3Dialogue, false));
        Debug.Log("DialogueManager handled option3 dialogue");

        // No further actions specified for option 3, add logic here if needed
    }
}
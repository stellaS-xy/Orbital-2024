using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter4SceneController : MonoBehaviour
{
    public string[] startDialogue;
    public string[] option1Dialogue;
    public string[] option2Dialogue;
    public string[] option3Dialogue;

    public GameObject gifPlayerObject;
    public GameObject gifImage;
    private gifPlayer gifPlayer;

    public string[] choiceContents;
    public GameObject choiceButtonGroup;

   
    public GameObject optionDialogue;

    private bool option1Selected = false;
    private bool option2Selected = false;

    private void Start()
    {
        gifPlayer = gifPlayerObject.GetComponent<gifPlayer>();
        gifPlayerObject.SetActive(false);
        gifImage.SetActive(false);
        choiceButtonGroup.SetActive(false);
        optionDialogue.SetActive(false);

        StartCoroutine(StartSceneSequence());
    }

    private IEnumerator StartSceneSequence()
    {
        // Show the starting dialogue
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(startDialogue, true));

        // Wait for the dialogue to end
        yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());

        // Play the GIF
        gifPlayerObject.SetActive(true);
        gifImage.SetActive(true);
        yield return new WaitUntil(() => gifPlayer.IsAnimationFinished());
        gifPlayerObject.SetActive(false);
        gifImage.SetActive(false);

        Debug.Log("gif has been played");

        // Show the choices
        ShowChoices();
    }

    private void ShowChoices()
    {
        Debug.Log("Show Choices is called");
        Action[] actions = new Action[choiceContents.Length];
        actions[0] = OnOption1Selected;
        actions[1] = OnOption2Selected;
        actions[2] = option1Selected && option2Selected ? (Action)OnOption3Selected : null;

        DecisionManager.Instance.ShowDecision(choiceContents, actions);
        Debug.Log("Decision Manager is called from chapter 4 scene controller");
    }

    private void OnOption1Selected()
    {
        choiceButtonGroup.SetActive(false);
        Debug.Log("Option 1 selected");
        StartCoroutine(HandleOption1Dialogue());
    }


    private void OnOption2Selected()
    {
        choiceButtonGroup.SetActive(false);
        Debug.Log("Option 2 selected");
        StartCoroutine(HandleOption2Dialogue());
    }


    private void OnOption3Selected()
    {
        choiceButtonGroup.SetActive(false);
        Debug.Log("Option 3 selected");
        StartCoroutine(HandleOption3Dialogue());
    }


    private IEnumerator HandleOption1Dialogue()
    {
        optionDialogue.SetActive(true);

        Debug.Log("HandleOption1Dialogue being called");
        yield return StartCoroutine(OptionDialogueManager.Instance.ShowDialogue(option1Dialogue));
        yield return new WaitUntil(() => !OptionDialogueManager.Instance.IsDialogueBoxActive());

        // Load scene with index 11
        SceneManager.LoadScene(11);
    }

    private IEnumerator HandleOption2Dialogue()
    {
        optionDialogue.SetActive(true);
        Debug.Log("HandleOption1Dialogue being called");
        yield return StartCoroutine(OptionDialogueManager.Instance.ShowDialogue(option2Dialogue));
        yield return new WaitUntil(() => !OptionDialogueManager.Instance.IsDialogueBoxActive());

        // Load scene with index 12
        SceneManager.LoadScene(12);
    }

    private IEnumerator HandleOption3Dialogue()
    {
        optionDialogue.SetActive(true);
        Debug.Log("HandleOption1Dialogue being called");
        yield return StartCoroutine(OptionDialogueManager.Instance.ShowDialogue(option3Dialogue));
        yield return new WaitUntil(() => !OptionDialogueManager.Instance.IsDialogueBoxActive());

        // Load scene with index 14
        SceneManager.LoadScene(14);
    }

    
}

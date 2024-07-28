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

    private void Awake()
    {
        // Load player choices from PlayerPrefs
        option1Selected = PlayerPrefs.GetInt("Option1Selected", 0) == 1;
        option2Selected = PlayerPrefs.GetInt("Option2Selected", 0) == 1;
    }

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

        Debug.Log("GIF has been played");
        choiceButtonGroup.SetActive(true);

        // Show the choices
        ShowChoices();
    }

    private void ShowChoices()
    {
        Debug.Log("Show Choices is called");
        if (DecisionManager.Instance != null)
        {
            Action[] actions = new Action[choiceContents.Length];
            actions[0] = OnOption1Selected;
            actions[1] = OnOption2Selected;
            actions[2] = option1Selected && option2Selected ? (Action)OnOption3Selected : null;

            DecisionManager.Instance.ShowDecision(choiceContents, actions);
            Debug.Log("Decision Manager is called from Chapter 4 Scene Controller");
        }
        else
        {
            Debug.LogError("DecisionManager instance not found.");
        }
    }

    private void OnOption1Selected()
    {
        if (choiceButtonGroup != null)
        {
            choiceButtonGroup.SetActive(false);
        }
        Debug.Log("Option 1 selected");
        StartCoroutine(HandleOption1Dialogue());
    }

    private void OnOption2Selected()
    {
        if (choiceButtonGroup != null)
        {
            choiceButtonGroup.SetActive(false);
        }
        Debug.Log("Option 2 selected");
        StartCoroutine(HandleOption2Dialogue());
    }

    private void OnOption3Selected()
    {
        if (choiceButtonGroup != null)
        {
            choiceButtonGroup.SetActive(false);
        }
        Debug.Log("Option 3 selected");
        StartCoroutine(HandleOption3Dialogue());
    }

    private IEnumerator HandleOption1Dialogue()
    {
        option1Selected = true;
        PlayerPrefs.SetInt("Option1Selected", 1); // Save the choice
        PlayerPrefs.Save();

        if (optionDialogue != null)
        {
            optionDialogue.SetActive(true);
        }
        Debug.Log("HandleOption1Dialogue being called");
        yield return StartCoroutine(OptionDialogueManager.Instance.ShowDialogue(option1Dialogue));
        yield return new WaitUntil(() => !OptionDialogueManager.Instance.IsDialogueBoxActive());

        // Load scene with index 11
        SceneManager.LoadScene(11);
    }

    private IEnumerator HandleOption2Dialogue()
    {
        option2Selected = true;
        PlayerPrefs.SetInt("Option2Selected", 1); // Save the choice
        PlayerPrefs.Save();

        if (optionDialogue != null)
        {
            optionDialogue.SetActive(true);
        }
        Debug.Log("HandleOption2Dialogue being called");
        yield return StartCoroutine(OptionDialogueManager.Instance.ShowDialogue(option2Dialogue));
        yield return new WaitUntil(() => !OptionDialogueManager.Instance.IsDialogueBoxActive());

        // Load scene with index 12
        SceneManager.LoadScene(12);
    }

    private IEnumerator HandleOption3Dialogue()
    {
        if (optionDialogue != null)
        {
            optionDialogue.SetActive(true);
        }
        Debug.Log("HandleOption3Dialogue being called");
        yield return StartCoroutine(OptionDialogueManager.Instance.ShowDialogue(option3Dialogue));
        yield return new WaitUntil(() => !OptionDialogueManager.Instance.IsDialogueBoxActive());

        // Load scene with index 14
        SceneManager.LoadScene(14);
    }
}

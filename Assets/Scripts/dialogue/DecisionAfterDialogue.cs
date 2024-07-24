using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    public GameObject choiceButtonGroup; //parentObject
    public string[] choiceContents; // Contents for choices
    public string[] option1Dialogue;
    public string[] option2Dialogue;
    public string[] option3Dialogue;
    public string requiredKeyForOption2;
    public string requiredKeyForOption3;


    public string[] rabbitAfterRexaDialogue; // Dialogue with rabbit after Rexa walks away
    public GameObject gifPlayerObject; // GameObject for GIF Player

    public GameObject gifPlayerImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DialogueManager.Instance.OnDialogueEnded += OnDialogueEnded;
        choiceButtonGroup.SetActive(false);
        gifPlayerObject.SetActive(false);
        gifPlayerImage.SetActive(false);
        Debug.Log("DecisionBox has been set inactive");
    }

    private void OnDestroy()
    {
        DialogueManager.Instance.OnDialogueEnded -= OnDialogueEnded;
    }

    private void OnDialogueEnded()
    {
        
        ShowDecision();
        
    }

    private void ShowDecision()
    {
        Debug.Log("SceneController - ShowDecision has been called");

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
                    actions[i] = OnOption2Selected;
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

        // Transition to the next scene in sequence
        LoadNextSceneInSequence();
    }

    private void OnOption2Selected()
    {
        Debug.Log("Option 2 selected");
        if (option2Dialogue != null && option2Dialogue.Length > 0)
        {
            StartCoroutine(HandleOption2Dialogue());
        }
    }

    private IEnumerator HandleOption2Dialogue()
    {
        Debug.Log("HandleOption2Dialogue being called");
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(option2Dialogue, false));
        Debug.Log("DialogueManager handled option2 dialogue");

        // Play Rexa's animation
        yield return StartCoroutine(PlayRexaAnimation());

        // Start conversation with rabbit
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(rabbitAfterRexaDialogue, false));

        // Show GIF Player for losing signal effect
        yield return StartCoroutine(ShowLosingSignalGIF());
        Debug.Log("GIFPLayer is called as option 2 has been selected");

        // After GIF, show decision box again
        DecisionManager.Instance.ShowDecision(choiceContents, new Action[] { OnOption1Selected, OnOption2Selected });
    }

    private IEnumerator PlayRexaAnimation()
    {
        RexaAnimationTrigger rexaAnimTrigger = FindObjectOfType<RexaAnimationTrigger>();
        if (rexaAnimTrigger != null)
        {
            yield return rexaAnimTrigger.PlayRexaAnimation();
        }
        else
        {
            Debug.LogError("RexaAnimationTrigger not found in the scene.");
        }
    }

    private IEnumerator ShowLosingSignalGIF()
    {
        gifPlayerObject.SetActive(true);
        gifPlayerImage.SetActive(true);
        gifPlayer gifPlayer = gifPlayerObject.GetComponent<gifPlayer>();
        
        while (!gifPlayer.IsAnimationFinished())
        {
            yield return null;
        }
        gifPlayerObject.SetActive(false);
    }


    private void LoadNextSceneInSequence()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Ensure the next scene index is within the valid range
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogError("No more scenes in build settings to load.");
        }
    }

   
}

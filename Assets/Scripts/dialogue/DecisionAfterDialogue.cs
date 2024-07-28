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

    public string[] initialDialogue; // Initial dialogue at the start

    public string requiredKeyForOption2;
    public string requiredKeyForOption3;


    public string[] rabbitAfterRexaDialogue; // Dialogue with rabbit after Rexa walks away
    public string[] option1SecondDialogue;

    public GameObject gifPlayerObject; // GameObject for GIF Player
    public GameObject gifPlayerImage;
    private gifPlayer gifPlayer;


    public GameObject rexa; // Reference to the Rexa GameObject
    private Animator rexaAnimator;
    public GameObject rexaBase;
    public GameObject rabbit; // Reference to the rabbit GameObject

    public GameObject Canvas;

    private bool rabbitAfterDialogueDone;
    

    public int nextSceneIndex;

   


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
        
        choiceButtonGroup.SetActive(false);
        gifPlayerImage.SetActive(false);
        gifPlayerObject.SetActive(false);
        rexaBase.SetActive(false);
        rabbitAfterDialogueDone = false;
   

        Debug.Log("DecisionBox has been set inactive");

        if(rexa != null)
        {
            rexaAnimator = rexa.GetComponent<Animator>();
            if (rexaAnimator == null)
            {
                Debug.LogError("Animator component not found on Rexa GameObject.");
            }
        }

        if (gifPlayerObject != null)
        {
            gifPlayer = gifPlayerObject.GetComponent<gifPlayer>();
            gifPlayerObject.SetActive(false); // Initially hide the gif player
        }

        StartCoroutine(SceneCoroutine());

    }

    

    private IEnumerator SceneCoroutine()
    {
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(initialDialogue, false));
        yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());

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
        rexaBase.SetActive(true);

        rexa.SetActive(true);
        Debug.Log("Rexa set to active");

        rexaAnimator.enabled = false;
        rexa.transform.position = new Vector3(4.89f, -0.88f, 0f);
        Debug.Log("Rexa's position set to (4.89, -0.88, 0)");

        // Log the new position of Rexa after setting it
        Debug.Log("Rexa's new position: " + rexa.transform.position);

        rexaAnimator.enabled = true;
        Debug.Log("Rexa's Animator re-enabled");


        Debug.Log("HandleOption1Dialogue being called");
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(option1Dialogue, false));
        Debug.Log("DialogueManager handled option1 dialogue");

        // Wait for the dialogue to end
        yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());
        Debug.Log("Option 1 first dialogue finished");

       
        choiceButtonGroup.SetActive(false);

        rexaAnimator.Play("Idle State");
        Debug.Log("Rexa's animation state set to Idle");

        // Trigger Rexa's walkout animation
        rexaAnimator.SetBool("WalkOutAndDisappear", true);
        Debug.Log("RexaWalkOutandDisappear set to true");

        // Log the current state and parameter value
        var currentState = rexaAnimator.GetCurrentAnimatorStateInfo(0);
        bool walkOutAndDisappearValue = rexaAnimator.GetBool("WalkOutAndDisappear");
        Debug.Log($"Current Animation State: {currentState.fullPathHash}");
        Debug.Log($"WalkOutAndDisappear Parameter Value: {walkOutAndDisappearValue}");


        // Wait for the animation to start
        rexaBase.SetActive(false);
        yield return new WaitUntil(() => rexaAnimator.GetCurrentAnimatorStateInfo(0).IsName("RexaWalkOut"));
        Debug.Log("RexaWalkOut animation started");

        // Wait for the animation to finish
        yield return new WaitUntil(() => rexaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle State"));
        Debug.Log("RexaWalkOut animation finished");
        rexa.SetActive(false);

        // Reset the boolean parameter to prevent replaying the animation
        rexaAnimator.SetBool("WalkOutAndDisappear", false);

        // Start the conversation with Rabbit
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(option1SecondDialogue, false));
        Debug.Log("Rabbit after dialogue starts to display");

        // Wait for rabbitAfterRexaDialogue to finish
        yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());
        Debug.Log("Rabbit after dialogue is done");


        // Transition to the next scene in sequence
        SceneTransitionManager.Instance.TransitionToScene(SceneManager.GetActiveScene().buildIndex + 1);
           
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

        rabbit.SetActive(false);
        Debug.Log("Rabbit is set to inactive");

        Debug.Log("HandleOption2Sequence being called");
        choiceButtonGroup.SetActive(false);

        // Play the option 2 dialogue first
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(option2Dialogue, false));

        // Wait for the dialogue to end
        yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());
        Debug.Log("Option 2 dialogue finished");
        choiceButtonGroup.SetActive(false);

        // Trigger Rexa's walkout animation
        rexaAnimator.SetBool("WalkOutAndDisappear", true);

        // Wait for the animation to start
        yield return new WaitUntil(() => rexaAnimator.GetCurrentAnimatorStateInfo(0).IsName("RexaWalkOut"));
        Debug.Log("RexaWalkOut animation started");

        // Wait for the animation to finish
        yield return new WaitUntil(() => rexaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle State"));
        Debug.Log("RexaWalkOut animation finished");
        rexa.SetActive(false);

        // Reset the boolean parameter to prevent replaying the animation
        rexaAnimator.SetBool("WalkOutAndDisappear", false);

        // Start the conversation with Rabbit
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(rabbitAfterRexaDialogue, false));
        Debug.Log("Rabbit after dialogue starts to display");

        // Wait for rabbitAfterRexaDialogue to finish
        yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());
        Debug.Log("Rabbit after dialogue is done");

        

        // Play the losing signal GIF
        gifPlayerObject.SetActive(true);
        gifPlayerImage.SetActive(true);
        Debug.Log("GIF player set to active");

        choiceButtonGroup.SetActive(false);
        // Wait for the GIF animation to finish
        yield return new WaitUntil(() => gifPlayer.IsAnimationFinished());
        gifPlayerObject.SetActive(false);
        gifPlayerImage.SetActive(false);
        Debug.Log("GIF player finished and hidden");

        rexa.SetActive(true);

        // Log the current position of Rexa before setting the new position
        Debug.Log("Rexa's current position: " + rexa.transform.position);

        

        // Set Rexa's position
        rexa.transform.position = new Vector3(4.89f, -0.88f, 0f);
        Debug.Log("Rexa's position set to (4.89, -1.29, 0)");

        // Log the new position of Rexa after setting it
        Debug.Log("Rexa's new position: " + rexa.transform.position);

        

        rabbitAfterDialogueDone = true;


        if (rabbitAfterDialogueDone)
        {
            DecisionManager.Instance.ShowDecision(choiceContents, new Action[] { OnOption1Selected, OnOption2Selected });
            Debug.Log("Decision-making process resumed");

        }

        // Return to the decision-making process
        
    }





    /*
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
    */


}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending3SceneController : MonoBehaviour
{
    public string[] initialDialogue;
    public string[] secondDialogue;
    public string[] endingDialogue;


    public GameObject gifPlayerObject;
    public gifPlayer gifPlayer;
    public GameObject gifPlayerImage;

    public GameObject dialogueBox;
    public GameObject optionDialogueBox;
    


    public GameObject backgroundImage1;
    

    /*
    public GameObject cgImageObject; // The GameObject for the CG image
    public Image cgImage;
    public float cgDuration = 5f; // Duration for the CG image to be displayed
    */

    public int nextSceneIndex; // Index of the next scene to load

    private void Start()
    {
        // Ensure the necessary components are initially inactive
        gifPlayerObject.SetActive(false);

        gifPlayerImage.SetActive(false);
        dialogueBox.SetActive(false);
        optionDialogueBox.SetActive(false);
       
        backgroundImage1.SetActive(false);
       

        // Start the sequence of events
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        // Show initial dialogue
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(initialDialogue, true));

        // Wait for the dialogue to end
        yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());

        // Play the GIF

        gifPlayerImage.SetActive(true);
        gifPlayerObject.SetActive(true);
        yield return new WaitUntil(() => gifPlayer.IsAnimationFinished());
        Debug.Log("gif done playing");
        gifPlayerObject.SetActive(false);
        gifPlayerImage.SetActive(false);


        backgroundImage1.SetActive(true);
       

        // Show final dialogue
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(secondDialogue, true));

        // Wait for the dialogue to end
        yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());

        

        // Show final dialogue
        yield return StartCoroutine(OptionDialogueManager.Instance.ShowDialogue(endingDialogue));

        // Wait for the dialogue to end
        yield return new WaitUntil(() => !OptionDialogueManager.Instance.IsDialogueBoxActive());


        // Load the next scene
        SceneTransitionManager.Instance.TransitionToScene(16);
    }
}
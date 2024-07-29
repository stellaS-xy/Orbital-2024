using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending2SceneController : MonoBehaviour
{
    public string[] initialDialogue;
    public string[] EndingDialogue;


    public GameObject gifPlayerObject;
    public gifPlayer gifPlayer;
    public GameObject gifPlayerImage;

    public GameObject dialogueBoxToHide;
    public GameObject instructionBoxToHide;
    public GameObject nameBoxToHide;


    public GameObject backgroundImage1;
    public GameObject backgroundImage2;

    /*
    public GameObject cgImageObject; // The GameObject for the CG image
    public Image cgImage;
    public float cgDuration = 5f; // Duration for the CG image to be displayed
    */

  

    private void Start()
    {
        // Ensure the necessary components are initially inactive
        gifPlayerObject.SetActive(false);
        
        gifPlayerImage.SetActive(false);
        dialogueBoxToHide.SetActive(false);
        instructionBoxToHide.SetActive(false);
        nameBoxToHide.SetActive(false);
        backgroundImage1.SetActive(true);
        backgroundImage2.SetActive(false);

        // Start the sequence of events
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        // Show initial dialogue
        yield return StartCoroutine(OptionDialogueManager.Instance.ShowDialogue(initialDialogue));

        // Wait for the dialogue to end
        yield return new WaitUntil(() => !OptionDialogueManager.Instance.IsDialogueBoxActive());

        // Play the GIF

        gifPlayerImage.SetActive(true);
        gifPlayerObject.SetActive(true);
        yield return new WaitUntil(() => gifPlayer.IsAnimationFinished());
        Debug.Log("gif done playing");
        gifPlayerObject.SetActive(false);
        gifPlayerImage.SetActive(false);


        backgroundImage1.SetActive(false);
        backgroundImage2.SetActive(true);

        // Show final dialogue
        yield return StartCoroutine(OptionDialogueManager.Instance.ShowDialogue(EndingDialogue));

        // Wait for the dialogue to end
        yield return new WaitUntil(() => !OptionDialogueManager.Instance.IsDialogueBoxActive());


        // Load the next scene
        SceneTransitionManager.Instance.TransitionToScene(0);
    }

    /*
    private IEnumerator FadeIn(Image image)
    {
        Debug.Log("Fade In CG");
        float duration = 1f; // Fade in duration
        Color color = image.color;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            color.a = t / duration;
            image.color = color;
            yield return null;
        }
        color.a = 1f;
        image.color = color;
    }

    private IEnumerator FadeOut(Image image)
    {
        Debug.Log("Fade Out CG");
        float duration = 1f; // Fade out duration
        Color color = image.color;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            color.a = 1 - (t / duration);
            image.color = color;
            yield return null;
        }
        color.a = 0f;
        image.color = color;
    }
    */

}

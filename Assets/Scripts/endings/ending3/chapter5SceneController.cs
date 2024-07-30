using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class chapter5SceneController : MonoBehaviour
{
    public string[] initialDialogue;
    public string[] monitorDialogue;
  
    public string[] endingDialogue;

    public GameObject gifPlayerObject1;
    private gifPlayer gifPlayer1;
    public GameObject gifPlayerImage;

    public GameObject gifPlayerObject2;
    private gifPlayer gifPlayer2;
 

    public GameObject backgroundImage1;
    public GameObject blackbackground;
    public GameObject bgMonitor;
    public GameObject fileMonitor;
    public GameObject transitionBG;

    public GameObject dialogueBox;
    public GameObject optionDialogueBox;

    public int nextSceneIndex;


    private void Start()
    {
        gifPlayer1 = gifPlayerObject1.GetComponent<gifPlayer>();
        gifPlayer2 = gifPlayerObject2.GetComponent<gifPlayer>();

        blackbackground.SetActive(false);
        bgMonitor.SetActive(false);
        fileMonitor.SetActive(false);

        dialogueBox.SetActive(false);
        optionDialogueBox.SetActive(false);

        gifPlayerImage.SetActive(false);
        gifPlayerObject1.SetActive(false);
        gifPlayerObject2.SetActive(false);


        StartCoroutine(StartScene());
    }

    private IEnumerator StartScene()
    {
        yield return StartCoroutine(OptionDialogueManager.Instance.ShowDialogue(initialDialogue));
        yield return new WaitUntil(() => !OptionDialogueManager.Instance.IsDialogueBoxActive());

        yield return new WaitForSeconds(2f);

        bgMonitor.SetActive(true);
        transitionBG.SetActive(false);

        // Enable player control here to interact with monitors and files
    }

    public void InteractWithMonitor()
    {
        StartCoroutine(MonitorInteraction());
    }

    private IEnumerator MonitorInteraction()
    {
        bgMonitor.SetActive(false);
        backgroundImage1.SetActive(true);
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(monitorDialogue, true));
        yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());
        fileMonitor.SetActive(true);
        // Enable player control to interact with files
    }

    public void InteractWithFiles()
    {
        StartCoroutine(FileInteraction());
    }

    private IEnumerator FileInteraction()
    {
        fileMonitor.SetActive(false);
        gifPlayerObject1.SetActive(true);
        gifPlayerImage.SetActive(true);
        yield return new WaitUntil(() => gifPlayer1.IsAnimationFinished());
        gifPlayerObject1.SetActive(false);
        gifPlayerImage.SetActive(false);


        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(endingDialogue, true));
        yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());


        gifPlayerImage.SetActive(true);
        gifPlayerObject2.SetActive(true);
        yield return new WaitUntil(() => gifPlayer2.IsAnimationFinished());
        gifPlayerObject2.SetActive(false);
        gifPlayerImage.SetActive(false);

        SceneTransitionManager.Instance.TransitionToScene(0);
    }
}

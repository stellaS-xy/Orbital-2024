using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingSceneController : MonoBehaviour
{
    public string[] initialDialogueLines;
    public string[] finalDialogueLines;

    public GameObject dialogueBox;
    public Image cgImage;

    public float fadeDuration = 1.0f;

    private void Start()
    {
        cgImage.gameObject.SetActive(false);
        StartCoroutine(StartSceneSequence());
    }

    private IEnumerator StartSceneSequence()
    {
        // Show initial dialogue
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(initialDialogueLines, false));

        // Wait for the dialogue to end
        yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());

        // Fade in CG
        yield return StartCoroutine(FadeInCG());

        // Show final dialogue with CG as background
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(finalDialogueLines, false));

        // Wait for the dialogue to end
        yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());

        // Transition to the next scene
        LoadNextSceneInSequence();
    }

    private IEnumerator FadeInCG()
    {
        cgImage.gameObject.SetActive(true);
        Color color = cgImage.color;
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, timer / fadeDuration);
            cgImage.color = color;
            yield return null;
        }

        color.a = 1;
        cgImage.color = color;
    }

    private void LoadNextSceneInSequence()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

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

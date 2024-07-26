using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OptionDialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox; // Display or Hide
    [SerializeField] Text dialogueText;
    [TextArea(1, 3)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;

    public static OptionDialogueManager Instance { get; private set; }

    public bool isScrolling;
    [SerializeField] private float textSpeed;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dialogueText.text = ""; // Ensure the dialogue text is empty initially
        dialogueBox.SetActive(false); // Ensure the dialogue box is hidden initially
    }

    private void Update()
    {
        // Ensure HandleUpdate is called every frame
        HandleUpdate();
    }

    public void HandleUpdate()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E is pressed down");
                if (isScrolling)
                {
                    Debug.Log("Skip scrolling effect");
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[currentLine];
                    isScrolling = false;
                }
                else
                {
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        Debug.Log("To the next line");
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        Debug.Log("No more dialogue");
                        EndDialogue();
                    }
                }
            }
        }
    }

    public IEnumerator ShowDialogue(string[] _newLines)
    {
        Debug.Log("Option Dialogue Manager show dialogue being called");
        yield return new WaitForEndOfFrame();

        dialogueLines = _newLines;
        currentLine = 0;

        StartCoroutine(ScrollingText());

        dialogueBox.SetActive(true);
    }

    public void EndDialogue()
    {
        Debug.Log("EndDialogue is called and dialogueBox set to inactive");
        dialogueBox.SetActive(false); // Hide the Box when the dialogue is done
    }

    public bool IsDialogueBoxActive()
    {
        return dialogueBox.activeInHierarchy;
    }

    private IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogueText.text = "";

        foreach (char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isScrolling = false;
    }
}

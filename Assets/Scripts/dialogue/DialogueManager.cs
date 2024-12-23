using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager: MonoBehaviour
{ 
    [SerializeField] GameObject dialogueBox, nameBox, instructionBox, decisionBox; //Display or Hide
    [SerializeField] Text dialogueText, nameText, instructionText;
    [SerializeField] Image faceImage;
    //[SerializeField] Button option1Button, option2Button; // Buttons for options

    [Header("Face Image")]
    public Sprite rabbitFace, bearFace, foxFace, lionFace, narratorFace, elephantFace;

    [TextArea(1,3)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;

    public static DialogueManager Instance { get; private set; }

    public bool isScrolling;

    [SerializeField] private float textSpeed;

    public event Action OnDialogueEnded;

    


    private void Awake()
    {
        Instance = this;
    }

    private void Start() 
    {
        dialogueText.text = ""; // Ensure the dialogue text is empty initially
        dialogueBox.SetActive(false); // Ensure the dialogue box is hidden initially
        nameBox.SetActive(false); // Ensure the name box is hidden initially
        instructionBox.SetActive(true);
        decisionBox.SetActive(false);


    }

    public void HandleUpdate()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isScrolling)
                {
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[currentLine];
                    isScrolling = false;
                }
                else
                {
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        CheckName();
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        EndDialogue();
                        
                    }
                }
            }
        }
    }

    public IEnumerator ShowDialogue(string[] _newLines, bool hasName) 
    {
        Debug.Log("ShowDialogue has been actived");

       

        yield return new WaitForEndOfFrame();

 
        dialogueLines = _newLines;
        currentLine = 0;

        CheckName();

        //dialogueText.text = dialogueLines[currentLine];
        StartCoroutine(ScrollingText());

        dialogueBox.SetActive(true);
        nameBox.SetActive(true);
        instructionBox.SetActive(false);
        
        Debug.Log("ShowDialogue is done");

        // StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false); // Hide the Box when the dialogue is done
        nameBox.SetActive(false); //Hide the Name Box when the dialogue is done
        instructionBox.SetActive(true);
        OnDialogueEnded?.Invoke();

    }

    
    public bool IsDialogueBoxActive()
    {
        return dialogueBox.activeInHierarchy;
    }

    private void CheckName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", "").Trim();

            if (dialogueLines[currentLine].Contains("Sammy"))
            {
                faceImage.sprite = rabbitFace;
                Debug.Log("Setting face to Sammy (Rabbit)");
            }
            else if (dialogueLines[currentLine].Contains("Truth"))
            {
                faceImage.sprite = bearFace;
                Debug.Log("Setting face to Truth (Bear)");
            }
            else if (dialogueLines[currentLine].Contains("Rexa"))
            {
                faceImage.sprite = lionFace;
                Debug.Log("Setting face to Rexa (Lion)");
            }
            else if (dialogueLines[currentLine].Contains("Finn"))
            {
                faceImage.sprite = foxFace;
                Debug.Log("Setting face to Finn (Fox)");
            }
            else if (dialogueLines[currentLine].Contains("Hugo"))
            {
                faceImage.sprite = elephantFace;
                Debug.Log("Setting face to Hugo (Elephant)");
            }
            else
            {
                faceImage.sprite = narratorFace;
                Debug.Log("Setting face to default (Narrator)");

            }


            currentLine++;
        }
    }


    /*
    public void ShowDecision(string option1, string option2, System.Action onOption1Selected, System.Action onOption2Selected)
    {
        decisionBox.SetActive(true);
        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();

        option1Button.GetComponentInChildren<Text>().text = option1;
        option2Button.GetComponentInChildren<Text>().text = option2;

        option1Button.onClick.AddListener(() => {
            decisionBox.SetActive(false);
            onOption1Selected.Invoke();
        });

        option2Button.onClick.AddListener(() => {
            decisionBox.SetActive(false);
            onOption2Selected.Invoke();
        });
    }
    */


    private IEnumerator ScrollingText() 
    {
        isScrolling = true;
        dialogueText.text = "";

        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isScrolling = false;
    }
}

    

    
    /*
    [SerializeField] int lettersPerSecond; 
    public event Action OnShowDialogue;
    public event Action OnHideDialogue;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    Dialogue dialogue;
    int currentLine = 0;
    bool isTyping;

    public void HandleUpdate() 
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTyping )
        {
            ++currentLine;
            if (currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));

            } 
            else 
            {
                dialogueBox.SetActive(false);
                OnHideDialogue?.Invoke(); 
            }

        }

    }


    

    public IEnumerator ShowDialogue(Dialogue dialogue)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialogue?.Invoke();

        this.dialogue = dialogue;
        currentLine = 0;

        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
    }

    public IEnumerator TypeDialogue(string line) 
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
    */
    

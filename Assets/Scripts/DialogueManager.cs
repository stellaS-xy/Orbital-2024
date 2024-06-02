using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager: MonoBehaviour
{ 
    [SerializeField] GameObject dialogueBox, nameBox, instructionBox; //Display or Hide
    [SerializeField] Text dialogueText, nameText, instructionText;
    [SerializeField] Image faceImage;

    [Header("Face Image")]
    public Sprite face01, face02;

    [TextArea(1,3)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;

    public static DialogueManager Instance { get; private set; }

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
        nameBox.SetActive(false); // Ensure the name box is hidden initially
        
    }

    public void HandleUpdate()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (isScrolling ==false)
                {
                    ++currentLine;
                if(currentLine < dialogueLines.Length)
                {
                    CheckName();
                    //dialogueText.text = dialogueLines[currentLine];
                    StartCoroutine(ScrollingText());

                     
                }
                else
                {
                    dialogueBox.SetActive(false); // Hide the Box when the dialogue is done
                    nameBox.SetActive(false); //Hide the Name Box when the dialogue is done
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
        nameBox.SetActive(hasName);
        instructionBox.SetActive(false);
        
        Debug.Log("ShowDialogue is done");

        // StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
    }
    public bool IsDialogueBoxActive()
    {
        return dialogueBox.activeInHierarchy;
    }

    private void CheckName()
    {
        if(dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-","");
            
            if (dialogueLines[currentLine] == "n-Sammy")
            {
                faceImage.sprite = face01;
            }
            else
            {
                faceImage.sprite = face02;
            }

            currentLine++;



        }
    }

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
    

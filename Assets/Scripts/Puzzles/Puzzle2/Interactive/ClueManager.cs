using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;


public class ClueManager : MonoBehaviour
{
    public static ClueManager Instance { get; private set; }

    private int cluesCollected = 0;
    private const int totalClues = 3; // Adjust this if you have more or fewer clues
    private List<string> cluesList;

    public GameObject rabbitObject;
    public NPCController rabbit;

    public GameObject directionCanvas;
   
    public GameObject arrow;

    public GameObject toNextScene;

    public string[] rabbitDialogue;

    private void Start()
    {
        arrow.SetActive(false);
        rabbitObject.SetActive(false);
        toNextScene.SetActive(false);

    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            cluesList = new List<string>(); // Initialize the list
        }
        else
        {
            Destroy(gameObject);
        }
    }

  

    public void CollectClue(string clueName)
    {
        if (!cluesList.Contains(clueName)) // Check if clue is not already collected
        {
            cluesList.Add(clueName); // Add the clue to the list
            cluesCollected++;
            Debug.Log("Clue collected: " + clueName + ". Total clues collected: " + cluesCollected);

            if (cluesCollected >= totalClues)
            {
                OnAllCluesCollected();
            }
        }
        else
        {
            Debug.Log("Clue already collected: " + clueName);
        }
    }

    private void OnAllCluesCollected()
    {
        Debug.Log("All clues collected!");
        // Trigger the next sequence of events

        
        directionCanvas.SetActive(false);
        

        PuzzleManager.Instance.SetPuzzleCompleted("Puzzle2");
        SceneTransitionManager.Instance.TransitionToScene(7);

        /*
        rabbitObject.SetActive(true);
        rabbit.showButton();
        arrow.SetActive(true);
        // Start the coroutine to handle the rabbit dialogue and scene transition
        StartCoroutine(HandleRabbitDialogueAndTransition());
        */
    }

    private IEnumerator HandleRabbitDialogueAndTransition()
    {
        if (rabbitDialogue != null && rabbitDialogue.Length > 0)
        {
            // Show the rabbit dialogue
            yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(rabbitDialogue, false));

            // Wait for the dialogue to finish
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogueBoxActive());
        }
        else
        {
            Debug.LogWarning("Rabbit dialogue is null or empty!");
        }
        // Transition to the next scene
        SceneTransitionManager.Instance.TransitionToScene(7);
    }




}

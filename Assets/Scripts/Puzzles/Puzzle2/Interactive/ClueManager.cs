using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnDialogueEnded += OnRabbitDialogueEnded;
        }
    }

    private void OnDisable()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnDialogueEnded -= OnRabbitDialogueEnded;
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

        rabbitObject.SetActive(true);
        rabbit.showButton();
        directionCanvas.SetActive(false);
        arrow.SetActive(true);
        PuzzleManager.Instance.SetPuzzleCompleted("Puzzle2");
        
        
    }


    private void OnRabbitDialogueEnded()
    {
        toNextScene.SetActive(true);

    }

}
